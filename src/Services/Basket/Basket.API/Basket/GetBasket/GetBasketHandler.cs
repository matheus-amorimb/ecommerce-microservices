namespace Basket.API.Basket.GetBasket;

public record GetBasketCommand(string UserName): IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBasketCommandHandler () : IQueryHandler<GetBasketCommand, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketCommand request, CancellationToken cancellationToken)
    {
        //TODO: get basket from database
        return new GetBasketResult(new ShoppingCart("cart"));
    }
}