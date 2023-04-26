using API.Presentation;
using Infra.MSSQL;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Extensions
{
    public static class WebHostExtensions
    {
        public static void RunMigration(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = host.Services.GetService<ILogger<Program>>();
                logger.LogInformation("Aplicando Migration.");

                for (var i = 0; i < 5; i++)
                {
                    try
                    {
                        var db = scope.ServiceProvider.GetService<DatabaseContext>();
                        db.Database.Migrate();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.Write("ERROR AO RODAR MIGRATION: ");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.InnerException);

                        Thread.Sleep(20000);

                        continue;
                    }
                }

                logger.LogInformation("Migration aplicada com sucesso.");
            }
        }
    }
}
