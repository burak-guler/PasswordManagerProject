using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Repositories;

namespace WebApi.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<INotificationQueueService, NotificationQueueService>();
            services.AddScoped<IUserLevelService, UserLevelService>();
            services.AddScoped<ITokenService, TokenService>();            
            return services;    
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<INotificationQueueRepository, NotificationQueueRepository>();
            services.AddScoped<IUserLevelRepository, UserLevelRepository>();
            return services;
        }
    }
}
