using LibraryApp.Infrastructure.Contexts;
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
        return services;
    }
}
