using Domain.Entities;
using Infra.MSSQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.MSSQL.Mappings
{
    public class AddressMapping : BaseMapping<Address>
    {
        public override string TableName => "Addresses";

        protected override void MapearEntidade(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.State).HasColumnName("State").HasColumnType("VARCHAR(600)").IsRequired();
            builder.Property(x => x.City).HasColumnName("City").HasColumnType("VARCHAR(600)").IsRequired();

            builder.HasOne(x => x.User)
                .WithOne(x => x.Address)
                .HasForeignKey<User>(x => x.AddressId);
        }

        protected override void MapearIndices(EntityTypeBuilder<Address> builder)
        {
            builder.HasIndex(c => new { c.Id, c.IsActive }, "ix_id_active");
        }
    }
}
