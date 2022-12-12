internal sealed record Position((int X, int Y) Location, char Height)
{
    private int _shortest = int.MaxValue;

    public int Visit(Area area, Position? previous, int currentSteps)
    {
        if (currentSteps >= _shortest)
        {
            return int.MaxValue;
        }
        
        _shortest = currentSteps;

        if (Location == area.Target)
        {
            return _shortest;
        }

        var neighbouringLocations = NeighbouringLocations(Location)
            .Select(l => l != previous?.Location && area.Map.TryGetValue(l, out var p) ? p : null)
            .OfType<Position>()
            .Where(x => HeightDifference(x) <= 1)
            .ToArray();

        return neighbouringLocations.Any() 
            ? neighbouringLocations.Min(x => x.Visit(area, this, currentSteps+1)) 
            : int.MaxValue;
    }
    
    private (int, int)[] NeighbouringLocations((int X, int Y) current) => new[]
    {
        (current.X, current.Y + 1),
        (current.X, current.Y - 1),
        (current.X - 1, current.Y),
        (current.X + 1, current.Y),
    };
        
    private int HeightDifference(Position next) => next.Height - Height;
}