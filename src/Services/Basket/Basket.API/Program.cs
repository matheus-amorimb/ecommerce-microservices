using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(assembly);
    configuration.AddBehavior(typeof(LoggingBehavior<,>));
    configuration.AddBehavior(typeof(ValidationBehavior<,>));
});

var app = builder.Build();
if (app.Environment.IsDevelopment()) { }
app.UseSwagger();
app.UseSwaggerUI();
app.MapCarter();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();