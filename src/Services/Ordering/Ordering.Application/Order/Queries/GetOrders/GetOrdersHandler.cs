using BuildingBlocks.Pagination;
using Ordering.Application.Extensions;

namespace Ordering.Application.Order.Queries.GetOrders;

public class GetOrdersHandler (IAppDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        int pageIndex = query.PaginationRequest.PageIndex;
        int pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
        
        var orders = await dbContext.Orders
            .Include(order => order.OrderItems)
            .OrderBy(order => order.OrderName.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList())
        );
    }
}