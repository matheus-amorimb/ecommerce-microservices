namespace Discount.Grpc.Shared;

public static class UpdateProps
{
    public static T UpdatePropsFromRequest<T>(this T entity, T request, string? id = null)
    {
        var tType = typeof(T);
        foreach (var prop in tType.GetProperties())
        {
            if (string.Equals(prop.Name, id, StringComparison.OrdinalIgnoreCase)) continue;
            var newValue = prop.GetValue(request);
            if (newValue is null) continue;
            prop.SetValue(entity, newValue);
        }
        return entity;
    }
}