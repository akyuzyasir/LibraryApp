using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Domain.Core.DataAccess.EntityFramework;
using LibraryApp.Domain.Entities.DbSets;
using LibraryApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories.Concretes;

public class BookCategoryRepository : EFBaseRepository<BookCategory>, IBookCategoryRepository
{
    public BookCategoryRepository(LibraryAppDbContext context) : base(context)
    {
    }
}
