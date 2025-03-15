using LibraryApp.Domain.Core.DataAccess.Interfaces;
using LibraryApp.Domain.Entities.DbSets;

namespace LibraryApp.DataAccess.Interfaces;

public interface IBookCopyRepository :        IAsyncRepository,
                                    IAsyncInsertableRepository<BookCopy>,
                                    IAsyncUpdatableRepository<BookCopy>,
                                    IAsyncDeletableRepository<BookCopy>,
                                    IAsyncFindableRepository<BookCopy>,
                                    IAsyncQueryableRepository<BookCopy>,
                                    IAsyncOrderableRepository<BookCopy>
{
}
