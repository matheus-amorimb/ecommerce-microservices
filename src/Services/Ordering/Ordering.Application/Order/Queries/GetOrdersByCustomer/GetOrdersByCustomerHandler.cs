using Ordering.Application.Extensions;

namespace Ordering.Application.Order.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler (IAppDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(order => order.CustomerId.Value.Equals(request.CustomerId))
            .OrderBy(order => order.OrderName.Value)
            .ToListAsync(cancellationToken);
        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}