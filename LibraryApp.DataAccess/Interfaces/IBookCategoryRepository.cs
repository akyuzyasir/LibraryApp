using LibraryApp.Domain.Core.DataAccess.Interfaces;
using LibraryApp.Domain.Entities.DbSets;

namespace LibraryApp.DataAccess.Interfaces;

public interface IBookCategoryRepository :        IAsyncRepository,
                                        IAsyncInsertableRepository<BookCategory>,
                                        IAsyncUpdatableRepository<BookCategory>,
                                        IAsyncDeletableRepository<BookCategory>,
                                        IAsyncFindableRepository<BookCategory>,
                                        IAsyncQueryableRepository<BookCategory>,
                                        IAsyncOrderableRepository<BookCategory>
{
}
