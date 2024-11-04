using Discount.Grpc.Models;

namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1, ProductName = "iPhone X", Description = "iPhone X discount description", Amount = 800
            },
            new Coupon
            {
                Id = 2, ProductName = "Samsung 10", Description = "Samsung 10 discount description", Amount = 800
            }
        ); 
    }
}