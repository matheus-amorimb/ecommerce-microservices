using Ordering.Infrastructure.Extensions;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static async Task<WebApplication> UseApiServices(this WebApplication app)
    {
        app.UseSwaggerUI();
        app.UseSwagger();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            await app.InitializeDbAsync();
        }
        
        return app;
    }
}