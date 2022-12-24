using Gatherly.Application.Abstractions;
using Gatherly.Domain.Repositories;
using Gatherly.Infrastructure.Repositories;
using Gatherly.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gatherly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MockMemberRepository>();
        services.AddScoped<IGatheringRepository, MockGatheringRepository>();
        services.AddScoped<IEmailService, MockEmailService>();

        return services;
    }
}
