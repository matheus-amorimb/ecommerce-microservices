namespace Ordering.Application.Order.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(order => order.Order.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(order => order.Order.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(order => order.Order.CustomerId).NotNull().WithMessage("CustomerId is required.");
    }
}