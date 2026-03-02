namespace YogaStudio.Core.Entities;

public class User : SoftDeletableEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties for relationships
    public InstructorProfile? InstructorProfile { get; set; }
    public StudentProfile? StudentProfile { get; set; }
}
