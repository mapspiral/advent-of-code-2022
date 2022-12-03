internal sealed class Elf
{
    public int Calories { get; private set; }

    public void Add(int calories)
    {
        Calories += calories;
    }
}