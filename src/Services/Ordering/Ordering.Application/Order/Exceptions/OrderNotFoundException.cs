namespace Ordering.Application.Order.Exceptions;

public class OrderNotFoundException(Guid id) : Exception($"Order with Id: {id} Not Found");