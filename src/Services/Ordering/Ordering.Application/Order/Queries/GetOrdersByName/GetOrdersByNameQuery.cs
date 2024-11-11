namespace Ordering.Application.Order.Queries.GetOrderByName;

public record GetOrdersByNameQuery(string Name) : IQuery<GetOrderByNameResult>;
public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);