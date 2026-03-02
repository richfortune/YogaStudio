using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaStudio.Core.Entities;

namespace YogaStudio.Infrastructure.Configurations;

public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> builder)
    {
        builder.HasKey(sp => sp.UserId); // PK maps directly to FK
        
        builder.Property(sp => sp.EmergencyContact).HasMaxLength(255);
        builder.Property(sp => sp.Notes).HasMaxLength(1000);
    }
}
