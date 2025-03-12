namespace LibraryApp.Domain.Entities.DbSets;

public class BookLoan : AuditableEntity
{
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    // Nav props
    public Guid BookCopyId { get; set; }
    public virtual BookCopy BookCopy { get; set; } = null!;
    public Guid MemberId { get; set; }
    public virtual Member? Member { get; set; }
}
