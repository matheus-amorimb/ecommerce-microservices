using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Models;

public class Product
{
    public Guid Id { get;  set; }
    public string Name { get;  set; } = default!;
    public List<string> Category { get;  set; } = [];
    public string Description { get;  set; } = default!;
    public string ImageUrl { get;  set; } = default!;
    public decimal Price { get;  set; }
    
    //Domain logic
    public void UpdateFromCommand(UpdateProductCommand command)
    {
        var productType = typeof(Product);
        var commandType = command.GetType();
        
        foreach(var prop in commandType.GetProperties())
        {
            var newValue = prop.GetValue(command, null);
            if (newValue is not null)
            {
                productType.GetProperty(prop.Name)?.SetValue(this, newValue);
            }
        }
    }
}