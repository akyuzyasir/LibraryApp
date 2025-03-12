using LibraryApp.Domain.Entities.Enums;

namespace LibraryApp.Domain.Entities.DbSets;

public class Book : AuditableEntity
{
    public string ISBN { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Author { get; set; } = null!;
    public DateTime PublicationYear { get; set; }

    // Nav Props

    public Guid BookCategoryId { get; set; }
    public virtual BookCategory BookCategory { get; set; } = null!;
    public virtual ICollection<BookCopy>? BookCopies { get; set; }

}
