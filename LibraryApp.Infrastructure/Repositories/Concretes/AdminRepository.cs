using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Domain.Core.DataAccess.EntityFramework;
using LibraryApp.Domain.Entities.DbSets;
using LibraryApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories.Concretes;

public class AdminRepository : EFBaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(LibraryAppDbContext context) : base(context)
    {
    }
    public Task<Admin?> GetByIdentityIdAsync(string identityId)
    {
        return _table.FirstOrDefaultAsync(a => a.IdentityId == identityId);
    }
}
