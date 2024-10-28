namespace Catalog.API.Models;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public List<string> Category { get; private set; } = [];
    public string Description { get; private set; } = default!;
    public string ImageUrl { get; private set; } = default!;
    public decimal Price { get; private set; }
}