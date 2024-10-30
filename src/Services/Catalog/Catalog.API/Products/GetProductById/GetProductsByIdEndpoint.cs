namespace Catalog.API.Products.GetProductById;

// public record GetProductsByIdRequest(string Id);
public record GetProductsByIdResponse(Product Product); 

public class GetProductsByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductsByIdResponse>();
            return Results.Ok(response);
        }).WithName("GetProductsById")
        .Produces<GetProductsByIdResponse>(statusCode: StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}