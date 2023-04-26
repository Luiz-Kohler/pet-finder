using Domain.Entities;
using Infra.MSSQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.MSSQL.Mappings
{
    public class ImageMapping : BaseMapping<Image>
    {
        public override string TableName => "Images";

        protected override void MapearEntidade(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Url).HasColumnName("Url").HasColumnType("VARCHAR(600)").IsRequired();

            builder.HasOne(x => x.Pet)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.PetId);
        }

        protected override void MapearIndices(EntityTypeBuilder<Image> builder)
        {
            builder.HasIndex(c => new { c.PetId, c.IsActive }, "ix_id_active");
        }
    }
}
