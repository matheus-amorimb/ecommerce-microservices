using BuildingBlocksMessaging.MassTransit;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// ======================================
// Configure Services
// ======================================

// Add Carter for routing and lightweight endpoint handling
builder.Services.AddCarter();

// Add MediatR with custom behaviors for logging and validation
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(assembly);
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// Add Marten for database interaction with lightweight sessions
builder.Services
    .AddMarten(options =>
    {
        options.Connection(builder.Configuration.GetConnectionString("Database") ?? string.Empty);
        options.Schema.For<ShoppingCart>().Identity(cart => cart.Username);
    })
    .UseLightweightSessions();

// Register repository services with a decorator pattern
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

// Configure Redis for caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Configure gRPC client for the Discount service
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
    {
        options.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl") ?? string.Empty);
    })
    .ConfigureChannel(options => options.Credentials = ChannelCredentials.Insecure);

// Add exception handler middleware
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Configure health checks for database and Redis
builder.Services
    .AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database") ?? string.Empty)
    .AddRedis(builder.Configuration.GetConnectionString("Redis") ?? string.Empty);

// Add essential ASP.NET Core services
builder.Services.AddControllers(); // MVC controller support
builder.Services.AddEndpointsApiExplorer(); // For API endpoint discovery
builder.Services.AddSwaggerGen(); // Swagger/OpenAPI documentation generation

// Configure message broker integration
builder.Services.AddMessageBroker(builder.Configuration);

// ======================================
// Build Application Pipeline
// ======================================
var app = builder.Build();

// Enable Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure routing and middleware
app.MapCarter(); // Map Carter endpoints
// app.UseHttpsRedirection(); // Redirect HTTP to HTTPS (currently disabled)

// Configure custom exception handling
app.UseExceptionHandler(options => { });

// Configure health check endpoint
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

// Enable authorization
app.UseAuthorization();

// Map controllers for API endpoints
app.MapControllers();

// Start the application
app.Run();