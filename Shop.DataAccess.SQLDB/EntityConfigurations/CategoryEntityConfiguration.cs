using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Core;

namespace Shop.DataAccess.SQLDB.EntityConfigurations
{
    internal class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("dbo_category");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
        }
    }
}
