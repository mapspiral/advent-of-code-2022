using System.Drawing;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

[Puzzle(2022, 15)]
public sealed class Puzzle15 : PuzzleBase<long>
{
    //private static readonly long? Size = 20;// 4000000;
    public override long Solution1(IEnumerable<string> input)
    {
        var map = CreateMap(input);
        var positions = GetRanges(map, 2000000);
        //var positions = GetRanges(map, 10);
        var segment = Simplify(positions);
        return segment.End - segment.Start;
    }

    public override long Solution2(IEnumerable<string> input)
    {
        var map = CreateMap(input);
        var size = 4000000L;

        for (var y = 0; y < size; y++)
        {
            var segment = Simplify(GetRanges(map, y, size));
            if (segment.End != size)
            {
                return TuningFrequency((segment.End + 1, y));
            }
        }

        return -1;
    }

    private (long Start, long End) Simplify((long Start, long End)[] positions)
    {
        var map = positions.GroupBy(x => x.Start).ToDictionary(k => k.Key, v => v.Max(x => x.End));
        var start = map.MinBy(x => x.Key);
        var max = map.MaxBy(x => x.Value);
        var currentEnd = start.Value;
        do
        {
            var bla = positions.Where(x => x.Start <= currentEnd && x.End > currentEnd).ToArray();
            if (!bla.Any())
            {
                return (start.Key, currentEnd);
            }

            var next = bla.MaxBy(x => x.End);
            if (next.End == max.Value)
            {
                return (start.Key, next.End);
            }

            currentEnd = next.End;
            
        } 
        while (true);
    }

    private Dictionary<(long, long), (long, long)> CreateMap(IEnumerable<string> input)
    {
        var map = new Dictionary<(long, long), (long, long)>();
        foreach (var line in input)
        {
            var matches = Regex.Matches(line, @"-?(\d+)");
            var sensor = (long.Parse(matches[0].Value), long.Parse(matches[1].Value));
            var beacon = (long.Parse(matches[2].Value), long.Parse(matches[3].Value));
            map[sensor] = beacon;
;        }
        return map;
    }

    private (long Start, long End)[] GetRanges(Dictionary<(long X, long Y), (long X, long Y)> sensorMap, long targetY, long? dimension = null)
    {
        var result = new List<(long Start, long End)>();
        result.AddRange(sensorMap.Keys.Where(k => k.X >= 0 && k.X <= dimension && k.Y == targetY).Select(x => (x.X, x.X)));
        result.AddRange(sensorMap.Values.Where(v => v.X >= 0 && v.X <= dimension && v.Y == targetY).Select(x => (x.X, x.X)));
        foreach (var item in sensorMap)
        {
            var sensor = item.Key;
            var beacon = item.Value;
            var distance = Math.Abs(sensor.X - beacon.X) + Math.Abs(sensor.Y - beacon.Y);
            var deltaY = Math.Abs(sensor.Y - targetY);
            var deltaX = distance - deltaY;
            if (deltaY <= distance)
            {
                var lowerBound = sensor.X - deltaX;
                var upperBound = sensor.X + deltaX;
                result.Add((dimension.HasValue ? Math.Max(0,lowerBound) : lowerBound, dimension.HasValue ? Math.Min(dimension.Value, upperBound) : upperBound));
            }
        }

        return result.ToArray();
        //
        // var temp = result.GroupBy(x => x.Start).ToArray().ToDictionary(k => k.Key, v => v.Select(x => x.End).Max());
        // var a = temp.OrderBy(x => x.Key);
        // var current = 0L;
        //
        // var visited = new HashSet<long>();
        // do
        // {
        //     if (current == Size)
        //     {
        //         return true;
        //     }
        //
        //     if (visited.Contains(current))
        //     {
        //         Console.WriteLine($"{current + 1}, {targetY}");
        //         return false;
        //     }
        //
        //     visited.Add(current);
        //
        //     if (temp.TryGetValue(current, out var nextCurrent) && current != nextCurrent)
        //     {
        //         current = nextCurrent;
        //     }
        //     else if (temp.TryGetValue(current+1, out nextCurrent))
        //     {
        //         current = nextCurrent;
        //     }
        //     else
        //     {
        //         nextCurrent = temp.Where(x => x.Key < current && x.Value != current).Max(x => x.Key);
        //         current = nextCurrent;
        //     }
        //
        // } while (true);
        // return false;
    }


    private long TuningFrequency((long x, long y) location) => location.x * 4000000 + location.y;
}