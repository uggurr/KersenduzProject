using kersenduz.Core.Repository.Implement;
using kersenduz.Core.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kersenduz.Core.Repository.IoC;

public static class RepositoryContainer
{
    public static void AddScopedRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
