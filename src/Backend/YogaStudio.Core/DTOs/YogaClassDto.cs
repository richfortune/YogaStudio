using YogaStudio.Core.Enums;

namespace YogaStudio.Core.DTOs;

public class YogaClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
