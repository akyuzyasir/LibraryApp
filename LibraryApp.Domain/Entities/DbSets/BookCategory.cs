namespace LibraryApp.Domain.Entities.DbSets;

public class BookCategory : AuditableEntity
{
    public string Name { get; set; } = null!;

    // Nav Props
    public virtual ICollection<Book>? Books { get; set; }
}
