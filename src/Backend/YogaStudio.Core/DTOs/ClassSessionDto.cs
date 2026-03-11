using YogaStudio.Core.Enums;

namespace YogaStudio.Core.DTOs;

public class ClassSessionDto
{
    public Guid Id { get; set; }
    public Guid YogaClassId { get; set; }
    public Guid? RoomId { get; set; }
    public Guid InstructorUserId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public int MaxCapacity { get; set; }
    public DeliveryMode DeliveryMode { get; set; }
    public string? MeetingUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
