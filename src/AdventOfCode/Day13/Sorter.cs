public class Sorter : IComparer<ValueList>
{
    public int Compare(ValueList? x, ValueList? y)
    {
        return x!.HasCorrectOrder(y!) switch
        {
            true => -1,
            false => 1,
            _ => 0
        };
    }
}