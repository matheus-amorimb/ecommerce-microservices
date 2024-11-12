using Ordering.Application.Order.Commands.UpdateOrder;
namespace Ordering.API.Endpoints;
public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccessful);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Updated Order")
        .WithDescription("Updated Order");
    }
}