using System.ComponentModel.DataAnnotations;
using YogaStudio.Core.Enums;

namespace YogaStudio.Core.DTOs;

public class UpdateBookingDto
{
    [Required]
    public Guid SessionId { get; set; }

    [Required]
    public Guid StudentUserId { get; set; }

    [Required]
    public BookingStatus Status { get; set; }
}
