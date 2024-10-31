
namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string CategoryName) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    
internal class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().Where(product => product.Category.Contains(query.CategoryName)).ToListAsync(cancellationToken);
        if (products.Count == 0)
        {
            throw new NotFoundException("Product not found");
        }
        return new GetProductsByCategoryResult(products);
    }
}