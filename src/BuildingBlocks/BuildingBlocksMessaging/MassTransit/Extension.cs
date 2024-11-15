using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocksMessaging.MassTransit;

public static class Extension
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.SetKebabCaseEndpointNameFormatter();
            
            if (assembly is not null) configurator.AddConsumers(assembly);
            
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                factoryConfigurator.Host(new Uri(configuration["MessageBroker:Host"]!), hostConfigurator =>
                {
                    hostConfigurator.Username(configuration["MessageBroker:UserName"]!);
                    hostConfigurator.Password(configuration["MessageBroker:Password"]!);
                });
                factoryConfigurator.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}