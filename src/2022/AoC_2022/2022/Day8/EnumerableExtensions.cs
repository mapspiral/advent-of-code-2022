public static class EnumerableExtensions
{
    public static IEnumerable<T> While<T>(this IEnumerable<T> values, Func<T, bool> fail)
    {
        foreach (var value in values)
        {
            if (fail(value))
            {
                yield return value;
                break;
            }

            yield return value;
        }
    }
}