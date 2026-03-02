namespace YogaStudio.Core.Entities;

public abstract class AuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public abstract class SoftDeletableEntity : AuditableEntity
{
    public DateTimeOffset? DeletedAt { get; set; }
    
    public bool IsDeleted => DeletedAt.HasValue;

    public void Delete()
    {
        DeletedAt = DateTimeOffset.UtcNow;
    }

    public void Restore()
    {
        DeletedAt = null;
    }
}
