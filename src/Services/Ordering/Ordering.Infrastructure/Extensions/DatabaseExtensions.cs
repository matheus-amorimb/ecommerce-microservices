using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static Task InitializeDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();
        return Task.FromResult(Task.CompletedTask);
    }
}