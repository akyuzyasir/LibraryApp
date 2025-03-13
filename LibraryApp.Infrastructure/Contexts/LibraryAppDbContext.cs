using LibraryApp.Domain.Core.Entities.Base;
using LibraryApp.Domain.Core.Enums;
using LibraryApp.Domain.Entities.Configurations;
using LibraryApp.Domain.Entities.DbSets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Infrastructure.Contexts
{
    public class LibraryAppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public const string DevConnectionString = "AppConnectionDev";
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public LibraryAppDbContext(DbContextOptions<LibraryAppDbContext> options, IHttpContextAccessor? httpContextAccessor = null) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<BookCopy> BookCopies { get; set; }
        public virtual DbSet<BookLoan> BookLoans { get; set; }
        public virtual DbSet<Member> Members { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            SetBaseProperties();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseProperties();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void SetBaseProperties()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            var userId = _httpContextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "UserNotFound";
            foreach (var entry in entries)
            {
                SetIfAdded(entry, userId);
                SetIfModified(entry, userId);
                SetIfDeleted(entry, userId);
            }
        }

        private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State != EntityState.Deleted)
                return;
            if (entry.Entity is not AuditableEntity entity)
                return;
            entry.Entity.Status = Status.Deleted;
            entry.State = EntityState.Modified;
            entity.DeletedDate = DateTime.UtcNow;
            entity.DeletedBy = userId;

        }

        private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State != EntityState.Modified)
                return;
            entry.Entity.Status = Status.Modified;
            entry.Entity.ModifiedDate = DateTime.UtcNow;
            entry.Entity.ModifiedBy = userId;
        }

        private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State != EntityState.Added)
                return;
            entry.Entity.Status = Status.Added;
            entry.Entity.ModifiedDate = DateTime.UtcNow;
            entry.Entity.ModifiedBy = userId;
        }
    }
}
