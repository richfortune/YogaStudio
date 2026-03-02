namespace YogaStudio.Core.Entities;

public class InstructorProfile
{
    public Guid UserId { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string Specialties { get; set; } = string.Empty; // e.g., CSV or JSON format for now

    // Navigation property
    public User? User { get; set; }
}
