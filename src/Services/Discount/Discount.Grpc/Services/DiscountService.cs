namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(coupon => coupon.ProductName == request.ProductName) ?? new Coupon {ProductName = "No discount", Amount = 0, Description = "No discount"};
        logger.LogInformation($"Discount is retrieved for Product: {coupon.ProductName}");
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Discount is successfully created. Product: {coupon.ProductName}");
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var updatedCoupon = request.Coupon.Adapt<Coupon>();
        var existingCoupon=
            await dbContext.Coupons.FirstOrDefaultAsync(coupon => coupon.ProductName == request.Coupon.ProductName);
        if (existingCoupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
        }
        existingCoupon.UpdatePropsFromRequest(updatedCoupon, "id");
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Discount is successfully updated. Product: {updatedCoupon.ProductName}");
        var couponModel = existingCoupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(coupon => coupon.ProductName == request.ProductName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product {request.ProductName} not found"));
        }
        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", request.ProductName);
        return new DeleteDiscountResponse { Success = true };
    }
}