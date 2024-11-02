namespace Basket.API.Models;

public class ShoppingCart
{
    public string Username { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

    public ShoppingCart(string username)
    {
        Username = username;
    }
    public ShoppingCart(){}
}