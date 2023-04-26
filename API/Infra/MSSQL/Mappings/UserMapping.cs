using Domain.Entities;
using Infra.MSSQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.MSSQL.Mappings
{
    public class UserMapping : BaseMapping<User>
    {
        public override string TableName => "Users";

        protected override void MapearEntidade(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("VARCHAR(600)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("VARCHAR(600)").IsRequired();
            builder.Property(x => x.Password).HasColumnName("Password").HasColumnType("VARCHAR(70)").IsRequired();

            builder.HasMany(x => x.PetsToAdopt)
                .WithOne(x => x.OldOwner)
                .HasForeignKey(x => x.OldOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(x => x.PetsAdopted)
                .WithOne(x => x.NewOwner)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Address)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.AddressId);
        }
    }
}
