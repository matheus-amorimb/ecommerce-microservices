var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(assembly);
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services
    .AddMarten(options =>
        {
            options.Connection(builder.Configuration.GetConnectionString("Default") ?? string.Empty);
            options.Schema.For<ShoppingCart>().Identity(cart => cart.Username);
        }
    ).UseLightweightSessions();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
app.MapCarter();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();