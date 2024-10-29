namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageUrl, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to create a product
        
        //CREATE PRODUCT ENTITY FROM COMMAND OBJECT
        Product product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageUrl = command.ImageUrl,
            Price = command.Price
        };
        
        //SAVE TO DATABASE
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        //RETURN CreateProductResult result
        return new CreateProductResult(product.Id);
    }
}