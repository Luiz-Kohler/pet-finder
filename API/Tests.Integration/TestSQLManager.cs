using Infra.MSSQL.Common;
using Infra.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using Infra.MSSQL.Mappings;

namespace Tests.Integration
{
    public static class TestSQLManager
    {
        public static void RebuildDatabase(IServiceProvider serviceProvider)
        {
            var databaseContext = serviceProvider.GetService<DatabaseContext>();
            databaseContext!.Database.EnsureDeleted();
            databaseContext.Database.Migrate();
        }

        public static void TruncateAllTables(IServiceProvider serviceProvider)
        {
            var databaseContext = serviceProvider.GetService<DatabaseContext>();

            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Images DROP CONSTRAINT FK_Images_Pets_PetId");
            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Pets DROP CONSTRAINT FK_Pets_Users_NewOwnerId");
            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Pets DROP CONSTRAINT FK_Pets_Users_OldOwnerId");
            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Users DROP CONSTRAINT FK_Users_Addresses_AddressId");

            var tableNames = GetTableNames();
            foreach (var tableName in tableNames)
            {
                databaseContext!.Database.ExecuteSqlRaw($"TRUNCATE TABLE {tableName};");
            }

            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Images ADD CONSTRAINT FK_Images_Pets_PetId FOREIGN KEY(Id) REFERENCES Pets (Id)");
            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Pets ADD CONSTRAINT FK_Pets_Users_NewOwnerId FOREIGN KEY(Id) REFERENCES Users (Id)");
            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Pets ADD CONSTRAINT FK_Pets_Users_OldOwnerId FOREIGN KEY(Id) REFERENCES Users (Id)");
            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Users ADD CONSTRAINT FK_Users_Addresses_AddressId FOREIGN KEY(Id) REFERENCES Addresses (Id)");
        }

        private static IEnumerable GetTableNames()
        {
            var tableNames = typeof(PetMapping).Assembly
                .GetTypes()
                .Where(x => x.IsSubclassOfRawGeneric(typeof(BaseMapping<>)))
                .Where(x => x.IsAbstract is false)
                .Select(x => Activator.CreateInstance(x))
                .Select(x => x.GetType().GetProperty(nameof(PetMapping.TableName)).GetValue(x))
                .ToList();
            return tableNames;
        }
    }

    public static class TypeExtensions
    {
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                    return true;

                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}