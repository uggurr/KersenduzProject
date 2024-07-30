using kersenduz.WebApp.Service.Implement;
using kersenduz.WebApp.Service.Interface;

namespace kersenduz.WebApp.Service.IoC;

public static class AppServiceContainer
{
    public static void AddAppScopedService(this IServiceCollection services)
    {
        services.AddScoped<ILoginRegisterService, LoginRegisterService>();
    }
}
