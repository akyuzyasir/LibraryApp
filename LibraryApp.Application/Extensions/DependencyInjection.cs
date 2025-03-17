using LibraryApp.Application.Concretes.Services;
using LibraryApp.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraryApp.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookCategoryService, BookCategoryService>();
        services.AddScoped<IBookCopyService, BookCopyService>();
        services.AddScoped<IBookLoanService, BookLoanService>();
        services.AddScoped<IMemberService, MemberService>();

        return services;
    }
}
