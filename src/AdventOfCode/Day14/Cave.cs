using System.Runtime.CompilerServices;

internal sealed class Cave
{
    private readonly Dictionary<(int X, int Y), Point2D> _obstacles;
    private readonly int _bottomY;
    private readonly Func<Point2D, bool, bool> _shouldContinue;

    public Cave(HashSet<Point2D> rocks, int bottomY, Func<Point2D, bool, bool> shouldContinue)
    {
        _obstacles = rocks.ToDictionary(k => (k.X, k.Y), v => v);
        _bottomY = bottomY;
        _shouldContinue = shouldContinue;
    }

    public Point2D Add(Point2D unit)
    {
        bool hasMoved;
        do
        {
            hasMoved = Fall(unit);
        } 
        while (_shouldContinue(unit, hasMoved));
        return _obstacles[(unit.X, unit.Y)] = unit;
    }
    
    private bool Fall(Point2D current)
    {
        var down = current.Neighbour(Movement.Down);
        if(!_obstacles.ContainsKey(down))
        {
            if (down.Y == _bottomY)
            {
                return false;
            }

            current.MoveTo(down);
            return true;
        }
            
        var left = current.Neighbour(Movement.DownLeft);
        if(!_obstacles.ContainsKey(left))
        {
            current.MoveTo(left);
            return true;
        }

        var right = current.Neighbour(Movement.DownRight);
        if(!_obstacles.ContainsKey(right))
        {
            current.MoveTo(right);
            return true;
        }

        return false;
    }
}