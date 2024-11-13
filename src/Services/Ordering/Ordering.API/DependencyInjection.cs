using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("Database") ?? String.Empty);
        return services;
    }

    public static async Task<WebApplication> UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseSwaggerUI();
        app.UseSwagger();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health", 
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        
        if (app.Environment.IsDevelopment())
        {
            await app.InitializeDbAsync();
        }
        
        return app;
    }
}