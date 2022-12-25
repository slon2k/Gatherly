using Gatherly.Domain.Repositories;
using Gatherly.Persistense.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Gatherly.Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistense(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MockMemberRepository>();
        services.AddScoped<IGatheringRepository, MockGatheringRepository>();

        return services;
    }
}
