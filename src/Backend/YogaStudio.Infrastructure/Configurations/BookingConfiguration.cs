using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YogaStudio.Core.Entities;

namespace YogaStudio.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        // Composite Unique constraint (SessionId, StudentUserId)
        builder.HasIndex(b => new { b.SessionId, b.StudentUserId }).IsUnique();

        // Relationships
        builder.HasOne(b => b.Session)
               .WithMany()
               .HasForeignKey(b => b.SessionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.StudentUser)
               .WithMany()
               .HasForeignKey(b => b.StudentUserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
