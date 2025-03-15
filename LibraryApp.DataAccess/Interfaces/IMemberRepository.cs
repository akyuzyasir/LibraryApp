using LibraryApp.Domain.Core.DataAccess.Interfaces;
using LibraryApp.Domain.Entities.DbSets;

namespace LibraryApp.DataAccess.Interfaces;

public interface IMemberRepository : IAsyncRepository, 
                                    IAsyncFindableRepository<Member>, 
                                    IAsyncInsertableRepository<Member>, 
                                    IAsyncDeletableRepository<Member>, 
                                    IAsyncUpdatableRepository<Member>, 
                                    IAsyncTransactionRepository
{
    Task<Member?> GetByIdentityIdAsync(string identityId);
}
