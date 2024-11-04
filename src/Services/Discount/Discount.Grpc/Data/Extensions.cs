namespace Discount.Grpc.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var db = scope.ServiceProvider.GetService<DiscountContext>();
        db?.Database?.MigrateAsync();
        return app;
    }
}