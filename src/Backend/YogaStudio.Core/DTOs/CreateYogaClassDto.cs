using System.ComponentModel.DataAnnotations;
using YogaStudio.Core.Enums;

namespace YogaStudio.Core.DTOs;

public class CreateYogaClassDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public DifficultyLevel DifficultyLevel { get; set; }
}
