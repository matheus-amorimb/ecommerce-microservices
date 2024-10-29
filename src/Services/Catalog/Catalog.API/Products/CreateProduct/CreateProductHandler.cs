using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageUrl, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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
        
        
        //RETURN CreateProductResult result
        return new CreateProductResult(Guid.NewGuid());
    }
}