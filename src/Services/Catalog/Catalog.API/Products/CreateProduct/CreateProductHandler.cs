using MediatR;

namespace Catalog.API.Products.CreateProduct;

public abstract record CreateProductCommand(string Name, List<string> Category, string Description, string ImageUrl, decimal Price)
    : IRequest<CreateProductResult>;
public abstract record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        //Business logic to create a product
        throw new NotImplementedException();
    }
}