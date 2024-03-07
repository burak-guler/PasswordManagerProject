using PasswordManager.WebApp.Services.Abstract;
using PasswordManager.WebApp.Services.Concrete;

namespace PasswordManager.WebApp.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services.AddHttpClient<IUserService, UserService>();
            services.AddHttpClient<IPasswordService, PasswordService>();
            services.AddHttpClient<ICategoryService, CategoryService>();          
            return services;
        }

        
    }
}
