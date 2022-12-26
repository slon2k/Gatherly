using Gatherly.Application.Abstractions;
using Gatherly.Infrastructure.BackgroundJobs;
using Gatherly.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gatherly.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, MockEmailService>();
        services.AddBackgroundJobs();

        return services;
    }
}
