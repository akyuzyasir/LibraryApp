using AspNetCoreHero.ToastNotification;
using LibraryApp.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Presentation.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<LibraryAppDbContext>()
                .AddDefaultTokenProviders();
            services.AddNotyf(config =>
            {
                config.HasRippleEffect = true;
                config.DurationInSeconds = 5;
                config.Position = NotyfPosition.BottomRight;

            });
            return services;
        }
    }
}
