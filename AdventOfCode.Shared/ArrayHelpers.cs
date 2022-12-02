namespace AdventOfCode.Shared;

public static class ArrayHelpers
{
    public static TType[] ParseAs<TType>(this string[] source)
    {
        return source.Select(e =>
        {
            var actualType = Nullable.GetUnderlyingType(typeof(TType));
            return actualType != null && !string.IsNullOrEmpty(e)
                ? Convert.ChangeType(e, actualType)
                : default(TType);
        }).Cast<TType>().ToArray();
    }
}