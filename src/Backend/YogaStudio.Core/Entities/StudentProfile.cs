namespace YogaStudio.Core.Entities;

public class StudentProfile
{
    public Guid UserId { get; set; }
    public string EmergencyContact { get; set; } = string.Empty;
    public string? Notes { get; set; }

    // Navigation property
    public User? User { get; set; }
}
