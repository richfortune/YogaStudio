using YogaStudio.Core.Enums;

namespace YogaStudio.Core.Entities;

public class YogaClass : SoftDeletableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; }
}
