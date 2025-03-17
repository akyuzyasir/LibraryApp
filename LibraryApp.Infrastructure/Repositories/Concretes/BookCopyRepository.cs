using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Domain.Core.DataAccess.EntityFramework;
using LibraryApp.Domain.Entities.DbSets;
using LibraryApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories.Concretes;

public class BookCopyRepository : EFBaseRepository<BookCopy>, IBookCopyRepository
{
    public BookCopyRepository(LibraryAppDbContext context) : base(context)
    {
    }
}
