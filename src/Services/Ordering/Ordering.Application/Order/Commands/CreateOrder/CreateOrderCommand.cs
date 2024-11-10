namespace Ordering.Application.Order.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.Order.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(command => command.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(command => command.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}
