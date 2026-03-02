using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaStudio.Core.Entities;

namespace YogaStudio.Infrastructure.Configurations;

public class InstructorProfileConfiguration : IEntityTypeConfiguration<InstructorProfile>
{
    public void Configure(EntityTypeBuilder<InstructorProfile> builder)
    {
        builder.HasKey(ip => ip.UserId); // PK maps directly to FK
        
        builder.Property(ip => ip.Bio).HasMaxLength(1000);
        builder.Property(ip => ip.Specialties).HasMaxLength(500);
    }
}
