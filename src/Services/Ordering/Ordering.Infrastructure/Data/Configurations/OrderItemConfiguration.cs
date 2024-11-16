using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(item => item.Id);
        builder.Property(item => item.Id)
            .HasConversion(
                orderItemId => orderItemId.Value, 
                dbId => OrderItemId.Of(dbId)
            );

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(item => item.ProductId);

        builder.Property(orderItem => orderItem.Quantity).IsRequired();
        builder.Property(orderItem => orderItem.Price).IsRequired();
    }
}