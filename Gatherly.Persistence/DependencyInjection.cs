using Gatherly.Domain.Repositories;
using Gatherly.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Gatherly.Persistence.Repositories;

namespace Gatherly.Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistense(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IGatheringRepository, GatheringRepository>();

        return services;
    }
}
