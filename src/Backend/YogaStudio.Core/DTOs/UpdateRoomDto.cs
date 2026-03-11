using System.ComponentModel.DataAnnotations;

namespace YogaStudio.Core.DTOs;

public class UpdateRoomDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1, 1000)]
    public int? Capacity { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }
}
