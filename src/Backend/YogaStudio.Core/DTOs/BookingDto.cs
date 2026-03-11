using YogaStudio.Core.Enums;

namespace YogaStudio.Core.DTOs;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Guid StudentUserId { get; set; }
    public BookingStatus Status { get; set; }
}
