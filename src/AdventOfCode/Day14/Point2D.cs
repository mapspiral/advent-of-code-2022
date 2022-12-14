using System.Collections;

public enum Movement
{
    Unknown = 0,
    Up,
    UpLeft,
    UpRight,
    Down,
    DownLeft,
    DownRight,
    Left,
    Right
}

public record Point2D
{
    public Point2D(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { get; private set; }
    public int Y { get; private set; }

    public Point2D MoveTo((int X, int Y) location)
    {
        X = location.X;
        Y = location.Y;
        return this;
    }
    
    public (int X, int Y) Neighbour(Movement direction)
    {
        if (direction == Movement.Up)
        {
            return (X, Y + 1);
        }

        if (direction == Movement.UpLeft)
        {
            return (X-1, Y+1);
        }

        if (direction == Movement.UpRight)
        {
            return (X+1, Y+1);
        }

        if (direction == Movement.Down)
        {
            return (X, Y-1);
        }

        if (direction == Movement.DownLeft)
        {
            return (X -1, Y - 1);
        }

        if (direction == Movement.DownRight)
        {
            return (X + 1, Y - 1);
        }

        if (direction == Movement.Left)
        {
            return (X--, Y);
        }

        if (direction == Movement.Right)
        {
            return (X+1, Y);
        }

        throw new Exception("Unsupported movement");
    }

    
}

public static class PointExtensions
{
    public static IEnumerable<Point2D> LineTo(Point2D from, Point2D to)
    {
        var result = new List<Point2D>();
        var deltaX = from.X - to.X;
        var deltaY = from.Y - to.Y;

        if (deltaY != 0)
        {
            if (deltaY < 0)
            {
                result.AddRange(Enumerable.Range(from.Y, deltaY * -1 + 1).Select(dy => new Point2D(from.X, dy * -1)));
            }
            else
            {
                result.AddRange(Enumerable.Range(to.Y, deltaY + 1).Select(dy => new Point2D(from.X, dy * -1)));
            }
        }
        
        if (deltaX != 0)
        {
            if (deltaX < 0)
            {
                result.AddRange(Enumerable.Range(from.X, deltaX * -1 + 1).Select(dx => new Point2D(dx, from.Y * -1)));
            }
            else
            {
                result.AddRange(Enumerable.Range(to.X, deltaX +1).Select(dx => new Point2D(dx, from.Y * -1)));
            }
        }
        return result;
    }
}