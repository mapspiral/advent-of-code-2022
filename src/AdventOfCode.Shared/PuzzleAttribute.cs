namespace AdventOfCode.Shared;

public sealed class PuzzleAttribute : Attribute
{
    public PuzzleAttribute(int year, int day)
    {
        Year = year;
        Day = day;
    }
    
    public int Year { get; }
    public int Day { get; }
}