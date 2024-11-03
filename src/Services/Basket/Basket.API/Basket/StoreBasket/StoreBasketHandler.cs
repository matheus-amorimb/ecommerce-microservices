namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(command => command.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(command => command.Cart.Username).NotEmpty().WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler (IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(basket.Username);
        //TODO: update cache
    }
}