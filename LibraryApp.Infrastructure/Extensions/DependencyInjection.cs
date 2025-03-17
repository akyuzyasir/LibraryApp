using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Infrastructure.Contexts;
using LibraryApp.Infrastructure.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<LibraryAppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString(LibraryAppDbContext.DevConnectionString),
                options => options.EnableRetryOnFailure(
                    10,
                    TimeSpan.FromSeconds(10),
                    null));
            options.UseLazyLoadingProxies();
        });

        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
        services.AddScoped<IBookCopyRepository, BookCopyRepository>();
        services.AddScoped<IBookLoanRepository, BookLoanRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();

        return services;
    }
}
