using System.ComponentModel.DataAnnotations;
using YogaStudio.Core.Enums;

namespace YogaStudio.Core.DTOs;

public class CreateClassSessionDto
{
    [Required]
    public Guid YogaClassId { get; set; }

    [Required]
    public Guid? RoomId { get; set; }

    [Required]
    public Guid InstructorUserId { get; set; }

    [Required]
    public DateTimeOffset StartTime { get; set; }

    [Required]
    [Range(15, 300)]
    public int DurationMinutes { get; set; }

    [Required]
    [Range(1, 100)]
    public int MaxCapacity { get; set; }

    [Required]
    public DeliveryMode DeliveryMode { get; set; }

    [Url]
    [MaxLength(500)]
    public string? MeetingUrl { get; set; }
}
