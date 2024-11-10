namespace BuildingBlocks.Utils;

public static class UpdateUtil
{
    public static T UpdateProps<T>(this T objectToUpdate, T source)
    {
        var tType = objectToUpdate?.GetType();
        if (tType == null) throw new NullReferenceException();
        foreach (var prop in tType.GetProperties())
        {
            var newValue = prop.GetValue(source);
            if (newValue is null) continue;
            prop.SetValue(objectToUpdate, newValue);
        }
        return objectToUpdate;
    }
}
