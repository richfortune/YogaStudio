using YogaStudio.Core.Enums;

namespace YogaStudio.Core.Entities;

public class ClassSession : SoftDeletableEntity
{
    public Guid Id { get; set; }
    
    public Guid YogaClassId { get; set; }
    public YogaClass? YogaClass { get; set; }

    public Guid InstructorUserId { get; set; }
    public User? InstructorUser { get; set; }

    public DateTimeOffset StartTime { get; set; }
    public int DurationMinutes { get; set; }
    
    // Derived property, not persisted in DB
    public DateTimeOffset EndTime => StartTime.AddMinutes(DurationMinutes);

    public int MaxCapacity { get; set; }

    public Guid? RoomId { get; set; }
    public Room? Room { get; set; }

    public DeliveryMode DeliveryMode { get; set; }
    public string? MeetingUrl { get; set; }
}
