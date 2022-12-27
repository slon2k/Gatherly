using Gatherly.Domain.Repositories;
using Gatherly.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Gatherly.Persistence.Repositories;
using Gatherly.Persistence.Interceptors;

namespace Gatherly.Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistense(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddSingleton<ConvertDomainEventsToOutboxInterceptor>();
        
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxInterceptor>();

            options.UseSqlServer(connectionString)
                .AddInterceptors(interceptor);
        });
            
        services.AddScoped<IMemberRepository, MemberRepository>();
        
        services.AddScoped<IGatheringRepository, GatheringRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
