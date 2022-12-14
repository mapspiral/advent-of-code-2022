using System.Data.Common;

[Puzzle(2022, 14)]
public sealed class Puzzle14 : PuzzleBase<long>
{
    public override long Solution1(IEnumerable<string> input)
    {
        var rocks = GetRocks(input);
        var bottom = rocks.Min(x => x.Y) - 2;
        var result = 0;
        var cave = new Cave(rocks, bottom, (p, m) => m && p.Y > bottom);
        do
        {
            var unit = new Point2D(500, 0);
            var location = cave.Add(unit);
            if (location.Y -1  == bottom)
            {
                return result;
            }
            result++;
        } while (true);
    }

    public override long Solution2(IEnumerable<string> input)
    {
        var rocks = GetRocks(input);
        var bottom = rocks.Min(x => x.Y) - 2;
        var result = 0;
        var cave = new Cave(rocks, bottom, (p, m) => m && (p.X, p.Y) != (500, 0));
        do
        {
            var unit = new Point2D(500, 0);
            var location = cave.Add(unit);
            if (location.Y - 1 == bottom)
            {
                return result;
            }
            result++;
        } while (true);
    }

    public void Render(HashSet<Point2D> points)
    {
        Console.Clear();
        for (var y = 0; y > -12; y--)
        {
            for(var x = 475; x < 525; x ++)
            {
                if (points.Contains(new Point2D(x, y)))
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    private HashSet<Point2D> GetRocks(IEnumerable<string> input)
    {
        var rocks = new HashSet<Point2D>();
        foreach (var line in input)
        {
            var coords = line.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToArray();
            var parsedCoords = coords.Select(x => x.Split(','))
                .Select(x => new Point2D(int.Parse(x[0]), int.Parse(x[1])))
                .ToArray();
            for (var i = 0; i < parsedCoords.Length - 1; i++)
            {
                var from = parsedCoords[i];
                var to = parsedCoords[i + 1];

                foreach (var point in PointExtensions.LineTo(from, to))
                {
                    rocks.Add(point);
                }
            }
        }

        return rocks;
    }
}