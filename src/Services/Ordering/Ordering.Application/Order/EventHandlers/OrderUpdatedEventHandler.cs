using Microsoft.Extensions.Logging;

namespace Ordering.Application.Order.EventHandlers;

public class OrderUpdatedEventHandler (ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderUpdateEvent>
{
    public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}