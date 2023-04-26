using Domain.Entities;
using Infra.MSSQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.MSSQL.Mappings
{
    public class PetMapping : BaseMapping<Pet>
    {
        public override string TableName => "Pets";

        protected override void MapearEntidade(EntityTypeBuilder<Pet> builder)
        {
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("VARCHAR(600)").IsRequired();
            builder.Property(x => x.AdoptDate).HasColumnName("AdoptDate").IsRequired(false);
            builder.Property(x => x.Age).HasColumnName("Age").HasColumnType("BIGINT").IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("VARCHAR(600)").IsRequired();
            builder.Property(x => x.Type).HasColumnName("Type").IsRequired();
            builder.Property(x => x.Size).HasColumnName("Size").IsRequired();

            builder.HasOne(x => x.OldOwner)
                .WithMany(x => x.PetsToAdopt)
                .HasForeignKey(x => x.OldOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.NewOwner)
                .WithMany(x => x.PetsAdopted)
                .HasForeignKey(x => x.NewOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

            builder.HasMany(x => x.Images)
                .WithOne(x => x.Pet)
                .HasForeignKey(x => x.PetId);
        }
    }
}
