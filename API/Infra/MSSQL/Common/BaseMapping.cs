using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.MSSQL.Common
{
    public abstract class BaseMapping<TEntity> : IBaseMapping
                where TEntity : BaseEntity
    {
        public abstract string TableName { get; }

        public void MapearEntidade(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<TEntity>();
            MapearBase(entityBuilder);
            MapearEntidade(entityBuilder);
            MapearIndices(entityBuilder);
        }

        private void MapearBase(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("DATETIME").IsRequired();
            builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired();
        }

        protected abstract void MapearEntidade(EntityTypeBuilder<TEntity> builder);
        protected virtual void MapearIndices(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasIndex(e => e.Id);
        }
    }
}
