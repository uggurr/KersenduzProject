using kersenduz.WebApi.Service.Implement;
using kersenduz.WebApi.Service.Interface;

namespace kersenduz.WebApi.Service.IoC
{
    public static class ServiceContainer
    {
        public static void AddScopedService(this IServiceCollection services)
        {
          services.AddScoped<ILoginRegisterService, LoginRegisterService>();
          services.AddScoped<IProductService, ProductService>();
          services.AddScoped<IUnitApiService, UnitApiService>();
          services.AddScoped<IRawMaterialService, RawMaterialService>();
        }
    }
}
