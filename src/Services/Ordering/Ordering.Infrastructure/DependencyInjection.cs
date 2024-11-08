using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetService<ISaveChangesInterceptor>() ?? throw new InvalidOperationException());
            // options.AddInterceptors(new AuditableEntityInterceptor(), new DispatchDomainEventsInterceptor());
            options.UseSqlServer(connectionString)
                .UseSnakeCaseNamingConvention();
        });
        return services;
    }
}