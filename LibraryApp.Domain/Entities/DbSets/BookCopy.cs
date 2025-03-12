using LibraryApp.Domain.Entities.Enums;

namespace LibraryApp.Domain.Entities.DbSets;

public class BookCopy:AuditableEntity
{
    public string CopyNumber { get; set; } = null!;
    public BookStatus BookStatus { get; set; }

    // Nav Props
    public Guid BookId { get; set; }
    public virtual Book Book { get; set; } = null!;

    public virtual ICollection<BookLoan>? BookLoans { get; set; }
}
