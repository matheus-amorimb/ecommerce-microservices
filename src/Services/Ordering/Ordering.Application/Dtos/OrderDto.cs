using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid Id, 
    Guid CustomerId, 
    string Name, 
    AddressDto ShippingAddress, 
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems);