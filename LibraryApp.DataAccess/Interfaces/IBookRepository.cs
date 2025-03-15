using LibraryApp.Domain.Core.DataAccess.Interfaces;
using LibraryApp.Domain.Entities.DbSets;

namespace LibraryApp.DataAccess.Interfaces;

public interface IBookRepository:   IAsyncRepository, 
                                    IAsyncInsertableRepository<Book>,
                                    IAsyncUpdatableRepository<Book>,
                                    IAsyncDeletableRepository<Book>,
                                    IAsyncFindableRepository<Book>,
                                    IAsyncQueryableRepository<Book>,
                                    IAsyncOrderableRepository<Book>
{
}
