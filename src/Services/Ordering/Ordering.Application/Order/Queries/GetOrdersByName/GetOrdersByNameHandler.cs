using Ordering.Application.Extensions;
using Ordering.Application.Order.Queries.GetOrderByName;

namespace Ordering.Application.Order.Queries.GetOrdersByName;

public class GetOrdersByNameHandler (IAppDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(order => order.OrderName.Value.Contains(query.Name))
            .OrderBy(order => order.OrderName)
            .ToListAsync(cancellationToken);
        return new GetOrderByNameResult(orders.ToOrderDtoList());
    }
}