using Ordering.Application.Order.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderRequest(Guid Id);
public record DeleteOrderResponse(bool IsSuccessful);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteOrderCommand(id));
                var response = result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");
    }
}