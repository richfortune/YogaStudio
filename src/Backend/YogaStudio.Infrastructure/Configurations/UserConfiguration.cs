using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaStudio.Core.Entities;

namespace YogaStudio.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(u => u.Email).IsUnique(); // Ensure emails are unique

        // Soft Delete global query filter
        builder.HasQueryFilter(u => u.DeletedAt == null);

        // One-to-One relationships with profiles
        builder.HasOne(u => u.InstructorProfile)
               .WithOne(ip => ip.User)
               .HasForeignKey<InstructorProfile>(ip => ip.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.StudentProfile)
               .WithOne(sp => sp.User)
               .HasForeignKey<StudentProfile>(sp => sp.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
