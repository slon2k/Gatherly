using Gatherly.Domain.Repositories;
using Gatherly.Persistence;
using Gatherly.Persistense.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistense(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IMemberRepository, MockMemberRepository>();
        services.AddScoped<IGatheringRepository, MockGatheringRepository>();

        return services;
    }
}
