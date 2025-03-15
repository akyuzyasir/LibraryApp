using LibraryApp.Domain.Core.DataAccess.Interfaces;
using LibraryApp.Domain.Entities.DbSets;

namespace LibraryApp.DataAccess.Interfaces;

public interface IAdminRepository : IAsyncRepository,
                                    IAsyncFindableRepository<Admin>,
                                    IAsyncInsertableRepository<Admin>,
                                    IAsyncDeletableRepository<Admin>,
                                    IAsyncUpdatableRepository<Admin>,
                                    IAsyncTransactionRepository
{
    Task<Admin?> GetByIdentityIdAsync(string identityId);
}
