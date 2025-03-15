using LibraryApp.Domain.Core.DataAccess.Interfaces;
using LibraryApp.Domain.Entities.DbSets;

namespace LibraryApp.DataAccess.Interfaces;

public interface IBookLoanRepository : IAsyncRepository,
                                    IAsyncInsertableRepository<BookLoan>,
                                    IAsyncUpdatableRepository<BookLoan>,
                                    IAsyncDeletableRepository<BookLoan>,
                                    IAsyncFindableRepository<BookLoan>,
                                    IAsyncQueryableRepository<BookLoan>,
                                    IAsyncOrderableRepository<BookLoan>
{
}
