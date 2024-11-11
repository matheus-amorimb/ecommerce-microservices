using Mapster;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Order.Commands.CreateOrder;

public class CreateOrderHandler(IAppDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id.Value);
    }

    private Domain.Models.Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName, 
            orderDto.ShippingAddress.LastName, 
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode);
        
        var billingAddress = Address.Of(
            orderDto.BillingAddress.FirstName, 
            orderDto.BillingAddress.LastName, 
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode);

        var newOrder = Domain.Models.Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.Name),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: Payment.Of(
                orderDto.Payment.CardName,
                orderDto.Payment.CardNUmber,
                orderDto.Payment.Expiration,
                orderDto.Payment.Cvv,
                orderDto.Payment.PaymentMethod)
        );

        orderDto.OrderItems.ForEach(orderItemDto => 
            newOrder.Add(
                ProductId.Of(orderItemDto.ProductId), 
                orderItemDto.Quantity,
                orderItemDto.Price)
        );
        
        return newOrder;
    }
}