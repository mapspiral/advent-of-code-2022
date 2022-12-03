using AdventOfCode.Shared;

internal sealed class Puzzle2 : PuzzleBase<int>
{    
    protected override int Solution1(IEnumerable<string> input)
    {
        var result = input.Select(s => (GetResult(s), GetItem(s[2]))).Select(GetScore).Sum();
        return result;

        Result GetResult(string source)
        {
            return source switch
            {
                "A X" or "B Y" or "C Z" => Result.Draw,
                "A Z" or "B X" or "C Y" => Result.Lost,
                _ => Result.Win,
            };
        }
    }

    protected override int Solution2(IEnumerable<string> input)
    {
        var result = input.Select(s =>
        {
            var expectedResult = GetExpectedResult(s);
            var counterItem = GetCounterItem(GetItem(s[0]), expectedResult);
            return (expectedResult, counterItem);
        }).Select(s => GetScore((s.expectedResult, s.counterItem))).Sum();
        return result;

        Item GetCounterItem(Item item, Result expectedResult)
        {
            return expectedResult switch
            {
                Result.Draw => item,
                Result.Lost => item switch
                {
                    Item.Rock => Item.Scissor,
                    Item.Paper => Item.Rock,
                    Item.Scissor => Item.Paper,
                    _ => Item.Unknown,
                },
                _ => item switch
                {
                    Item.Rock => Item.Paper,
                    Item.Paper => Item.Scissor,
                    Item.Scissor => Item.Rock,
                    _ => Item.Unknown,
                }
            };
        }

        Result GetExpectedResult(string source)
        {
            return source[2] switch
            {
                'X' => Result.Lost,
                'Y' => Result.Draw,
                'Z' => Result.Win,
                _ => Result.Unknown,
            };
        }
    }

    private Item GetItem(char source)
    {
        return source switch
        {
            'A' or 'X' => Item.Rock,
            'B' or 'Y' => Item.Paper,
            'C' or 'Z' => Item.Scissor,
            _ => Item.Unknown,
        };
    }

    private int GetScore((Result Result, Item Item) input)
    {
        var score = input.Result switch
        {
            Result.Draw => 3,
            Result.Win => 6,
            Result.Lost => 0,
            _ => -1,
        };

        return score + (int)input.Item;
    }
    
    private enum Result
    {
        Unknown = -1,
        Lost = 0,
        Draw = 3,
        Win = 6
    }

    private enum Item
    {
        Unknown = -1,
        Rock = 1,
        Paper = 2,
        Scissor = 3
    }
}