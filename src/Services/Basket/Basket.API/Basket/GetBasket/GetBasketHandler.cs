namespace Basket.API.Basket.GetBasket;

public record GetBasketCommand(string UserName): IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketCommandHandler (IBasketRepository basketRepository) : IQueryHandler<GetBasketCommand, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(command.UserName, cancellationToken);
        Console.WriteLine(basket.TotalPrice);
        return new GetBasketResult(basket);
    }
}