namespace LibraryApp.Domain.Core.Entities.Base;

public abstract class AuditableEntity : BaseEntity, ISoftDeleteableEntity
{
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
}
