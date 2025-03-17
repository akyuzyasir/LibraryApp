using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Domain.Core.DataAccess.EntityFramework;
using LibraryApp.Domain.Entities.DbSets;
using LibraryApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories.Concretes;

public class MemberRepository : EFBaseRepository<Member>, IMemberRepository
{
    public MemberRepository(LibraryAppDbContext context) : base(context)
    {
    }

    public Task<Member?> GetByIdentityIdAsync(string identityId)
    {
        return _table.FirstOrDefaultAsync(a => a.IdentityId == identityId);

    }
}
