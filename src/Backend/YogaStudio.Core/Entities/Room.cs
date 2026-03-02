namespace YogaStudio.Core.Entities;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public int? Capacity { get; set; }
}
