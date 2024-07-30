using kersenduz.WebApi.Service.Implement;
using kersenduz.WebApi.Service.Interface;

namespace kersenduz.WebApi.Service.IoC
{
    public static class ServiceContainer
    {
        public static void AddScopedService(this IServiceCollection services)
        {
            services.AddScoped<ILoginRegisterService, LoginRegisterService>();
        }
    }
}
