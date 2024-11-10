namespace Ordering.Application.Dtos;

public record PaymentDto(
    string CardName,
    string CardNUmber, 
    string Expiration, 
    string Cvv, 
    int PaymentMethod);