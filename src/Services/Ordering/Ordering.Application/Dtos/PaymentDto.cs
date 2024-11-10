namespace Ordering.Application.Dtos;

public record PaymentDto(
    string CardNUmber, 
    string Expiration, 
    string Cvv, 
    int PaymentMethod);