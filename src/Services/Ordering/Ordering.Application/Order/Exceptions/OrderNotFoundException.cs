namespace Ordering.Application.Order.Exceptions;

public class OrderNotFoundException(Guid id) : NotFoundException($"Order with Id: {id} Not Found");