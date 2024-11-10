using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Data;

public interface IAppDbContext
{
    DbSet<Domain.Models.Order> Orders { get;}
    DbSet<OrderItem> OrderItems { get;}
    DbSet<Product> Products { get;}
    DbSet<Customer> Customers { get;}
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}