using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaStudio.Core.Entities;

namespace YogaStudio.Infrastructure.Configurations;

public class YogaClassConfiguration : IEntityTypeConfiguration<YogaClass>
{
    public void Configure(EntityTypeBuilder<YogaClass> builder)
    {
        builder.HasKey(yc => yc.Id);

        builder.Property(yc => yc.Name).IsRequired().HasMaxLength(150);
        builder.Property(yc => yc.Description).HasMaxLength(1000);
        builder.Property(yc => yc.DifficultyLevel).IsRequired();

        // Soft Delete global query filter
        builder.HasQueryFilter(yc => yc.DeletedAt == null);
    }
}
