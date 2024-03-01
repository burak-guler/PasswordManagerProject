using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Repositories;
using WebApi.Models.Abstract;
using WebApi.Models.Concrete;

namespace WebApi.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;    
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
