namespace LibraryApp.Domain.Entities.DbSets;

public class Member : BaseUser
{
    public string MembershipNumber { get; set; } = null!;

    // Nav Props

    public virtual ICollection<BookLoan>? BookLoans { get; set; }

}
