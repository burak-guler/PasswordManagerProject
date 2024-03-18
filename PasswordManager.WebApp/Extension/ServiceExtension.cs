using PasswordManager.WebApp.Services.Abstract;
using PasswordManager.WebApp.Services.Concrete;
using PasswordManager.WebApp.Services.Concrete.UrlStatic;

namespace PasswordManager.WebApp.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services,IConfiguration configuration)
        {
            string BasePath = configuration.GetValue<string>("Application:ApiEndpoint");
            services.AddHttpClient<IUserClientService, UserClientService>(o =>
            {
                o.BaseAddress = new Uri($"{BasePath}{UrlHelperClient.UserService}");
            });

            services.AddHttpClient<IPasswordClientService, PasswordClientService>(o =>
            {
                o.BaseAddress = new Uri($"{BasePath}{UrlHelperClient.PasswordService}");
            });

            services.AddHttpClient<ICategoryClientService, CategoryClientService>(o =>
            {
                o.BaseAddress = new Uri($"{BasePath}{UrlHelperClient.CategoryService}");
            });

            services.AddHttpClient<ICompanyClientService, CompanyClientService>(o =>
            {
                o.BaseAddress = new Uri($"{BasePath}{UrlHelperClient.CompanyService}");
            });

            services.AddHttpClient<IGroupClientService, GroupClientService>(o =>
            {
                o.BaseAddress = new Uri($"{BasePath}{UrlHelperClient.GroupService}");
            });

            services.AddHttpClient<ILogClientService, LogClientService>(o =>
            {
                o.BaseAddress = new Uri($"{BasePath}{UrlHelperClient.LogService}");
            });

            services.AddHttpClient<INotificationQueueClientService, NotificationQueueClientService>(o =>
            {
                o.BaseAddress = new Uri($"{BasePath}{UrlHelperClient.NotificationQueueService}");
            });

            services.AddHttpClient<IUserLevelClientService, UserLevelClientService>(o =>
            {
                o.BaseAddress = new Uri($"{BasePath}{UrlHelperClient.UserLevelService}");
            });
            
            return services;
        }

        
    }
}
