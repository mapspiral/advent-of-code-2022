[Puzzle(2022, 9)]
public sealed class Puzzle09 : PuzzleBase<int>
{
    public override int Solution1(IEnumerable<string> input)
    {
        var head = new Position(0, 0);
        var tail = head;
        var positions = new HashSet<Position>();
        foreach (var movement in input.Select(ConvertToMovement))
        {
            foreach (var _ in Enumerable.Range(0, movement.Steps))
            {
                head = Move(head, movement.Direction);
                tail = Follow(head, tail);
                positions.Add(tail);
            }
        }
        return positions.Count;
    }
    
    public override int Solution2(IEnumerable<string> input)
    {
        var head = new Position(0, 0);
        var knots = Enumerable.Range(1, 9).ToDictionary(k => k, _ => new Position(0, 0));
        var positions = new HashSet<Position>();
        foreach (var movement in input.Select(ConvertToMovement))
        {
            foreach (var _ in Enumerable.Range(0, movement.Steps))
            {
                var previousKnot = head = Move(head, movement.Direction);
                for(var knotIndex = 1; knotIndex <= knots.Count; knotIndex++)
                {
                    previousKnot = knots[knotIndex] = Follow(previousKnot, knots[knotIndex]);
                }
                positions.Add(knots[9]);
            }
        }
        return positions.Count;
    }

    Movement ConvertToMovement(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return new Movement(Convert.ToChar(parts[0]), int.Parse(parts[1]));
    }

    Position Follow(Position head, Position tail)
    {
        if (head == tail)
        {
            return tail;
        }

        var deltaX = head.X - tail.X;
        var absDeltaX = Math.Abs(deltaX);
        var deltaY = head.Y - tail.Y;
        var absDeltaY = Math.Abs(deltaY);

        if (absDeltaX <= 1 && absDeltaY <= 1)
        {
            return tail;
        }

        if (deltaX == 0 || deltaY == 0)
        {
            return deltaY == 0
                ? tail with { X = tail.X + deltaX / 2 }
                : tail with { Y = tail.Y + deltaY / 2 };
        }

        return new Position(
            tail.X + (absDeltaX > 1 ? deltaX / 2 : deltaX), 
            tail.Y + (absDeltaY > 1 ? deltaY / 2 : deltaY));
    }

    Position Move(Position origin, char direction)
    {
        return direction switch
        {
            'U' => origin with { Y = origin.Y + 1 },
            'D' => origin with { Y = origin.Y - 1 },
            'R' => origin with { X = origin.X + 1 },
            'L' => origin with { X = origin.X - 1 },
            _ => origin
        };
    }

    private record Movement(char Direction, int Steps);

    private record Position(int X, int Y);
}