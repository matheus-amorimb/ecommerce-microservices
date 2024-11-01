namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation)) return;

        session.Store<Product>(GetPreConfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>()
    {
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Smartphone",
            Category = new List<string> { "Electronics", "Mobile" },
            Description = "Latest model smartphone with high resolution camera and long battery life.",
            ImageUrl = "smartphone.jpg",
            Price = 699.99M
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Laptop",
            Category = new List<string> { "Electronics", "Computers" },
            Description = "Lightweight laptop with powerful processor and extended battery life.",
            ImageUrl = "laptop.jpg",
            Price = 1099.99M
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Wireless Earbuds",
            Category = new List<string> { "Electronics", "Audio" },
            Description = "Noise-cancelling wireless earbuds with immersive sound quality.",
            ImageUrl = "earbuds.jpg",
            Price = 149.99M
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Running Shoes",
            Category = new List<string> { "Sports", "Footwear" },
            Description = "Comfortable and durable running shoes for all terrains.",
            ImageUrl = "running-shoes.jpg",
            Price = 89.99M
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Electric Toothbrush",
            Category = new List<string> { "Health", "Personal Care" },
            Description = "Electric toothbrush with multiple brushing modes and long-lasting battery.",
            ImageUrl = "toothbrush.jpg",
            Price = 59.99M
        }
    };
}