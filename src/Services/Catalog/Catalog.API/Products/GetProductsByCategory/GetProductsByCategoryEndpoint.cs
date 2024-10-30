namespace Catalog.API.Products.GetProductsByCategory;

// public record GetProductsByCategoryRequest(string CategoryName);
public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{categoryName}", async (string categoryName, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(categoryName));
            var response = result.Adapt<GetProductsByCategoryResponse>();
            return Results.Ok(response);
        }).WithName("GetProductsByCategory")
        .Produces<GetProductsByCategoryResponse>(statusCode: StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");;
    }
}