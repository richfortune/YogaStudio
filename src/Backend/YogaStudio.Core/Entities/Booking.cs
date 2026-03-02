using YogaStudio.Core.Enums;

namespace YogaStudio.Core.Entities;

public class Booking
{
     public Guid Id { get; set; }

     public Guid SessionId { get; set; }
     public ClassSession? Session { get; set; }

     public Guid StudentUserId { get; set; }
     public User? StudentUser { get; set; }

     public BookingStatus Status { get; set; }
     public DateTimeOffset BookedAt { get; set; } = DateTimeOffset.UtcNow;
     
     public DateTimeOffset? CancelledAt { get; set; }
     public DateTimeOffset? CheckedInAt { get; set; }

     // Note: Composite Unique constraint (SessionId, StudentUserId) 
     // will be configured in EF Core mapping (Infrastructure layer).
}
