using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Domain.Core.DataAccess.EntityFramework;
using LibraryApp.Domain.Entities.DbSets;
using LibraryApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories.Concretes;

public class BookLoanRepository : EFBaseRepository<BookLoan>, IBookLoanRepository
{
    public BookLoanRepository(LibraryAppDbContext context) : base(context)
    {
    }
}
