using Discount.Grpc;

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

public class StoreBasketCommandHandler (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await ApplyDiscount(command.Cart, cancellationToken);
        var basket = await basketRepository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(basket.Username);
    }
    
    private async Task ApplyDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var discountRequest = new GetDiscountRequest { ProductName = item.ProductName };
            var coupon = await discountProtoServiceClient.GetDiscountAsync(discountRequest, cancellationToken: cancellationToken);
            Console.WriteLine($"coupon.Amount {coupon.Amount}");
            item.Price -= coupon.Amount;
        }
    }
    
}