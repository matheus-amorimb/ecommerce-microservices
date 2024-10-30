namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger): ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle called with {@command}", command);
        var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException();
        }
        session.Delete<Product>(command.ProductId);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}