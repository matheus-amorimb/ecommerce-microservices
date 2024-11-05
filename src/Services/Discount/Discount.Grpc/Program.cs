using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc.Data;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddDbContext<DiscountContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("Database") ?? string.Empty);
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.UseMigrations();
app.MapGrpcService<DiscountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MapGrpcReflectionService();
app.UseExceptionHandler(options => { });
app.Run();