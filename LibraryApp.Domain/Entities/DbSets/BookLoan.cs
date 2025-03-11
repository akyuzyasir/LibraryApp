namespace LibraryApp.Domain.Entities.DbSets;

public class BookLoan : AuditableEntity
{
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    // Nav props
    public Guid BookId { get; set; }
    public virtual Book? Book { get; set; }
    public Guid MemberId { get; set; }
    public virtual Member? Member { get; set; }
}
