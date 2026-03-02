using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaStudio.Core.Entities;

namespace YogaStudio.Infrastructure.Configurations;

public class ClassSessionConfiguration : IEntityTypeConfiguration<ClassSession>
{
    public void Configure(EntityTypeBuilder<ClassSession> builder)
    {
        builder.HasKey(cs => cs.Id);

        // Ignore derived property
        builder.Ignore(cs => cs.EndTime);

        // Foreign Keys
        builder.HasOne(cs => cs.YogaClass)
               .WithMany()
               .HasForeignKey(cs => cs.YogaClassId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cs => cs.InstructorUser)
               .WithMany()
               .HasForeignKey(cs => cs.InstructorUserId)
               .OnDelete(DeleteBehavior.Restrict);
               
        builder.HasOne(cs => cs.Room)
               .WithMany()
               .HasForeignKey(cs => cs.RoomId)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired(false);

        // Properties
        builder.Property(cs => cs.MeetingUrl).HasMaxLength(500);

        // Soft Delete global query filter
        builder.HasQueryFilter(cs => cs.DeletedAt == null);
    }
}
