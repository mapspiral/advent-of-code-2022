using System.Numerics;

[Puzzle(2022, 11)]
public sealed class Puzzle11 : PuzzleBase<BigInteger>
{
    public override BigInteger Solution1(IEnumerable<string> input)
    {
        var monkeys = Parse(input);
        foreach (var _ in Enumerable.Range(0, 20))
        {
            foreach (var currentMonkey in Enumerable.Range(0, monkeys.Count))
            {
                monkeys[currentMonkey].Inspect(monkeys, l => l / 3);
            }
        }

        var mostActiveMonkeys = monkeys.Select(x => x.Value.InspectedItems).OrderDescending().Take(2).ToArray();
        return mostActiveMonkeys[0] * mostActiveMonkeys[1];
    }

    public override BigInteger Solution2(IEnumerable<string> input)
    {
        var monkeys = Parse(input);
        var reduce = monkeys.Values.Aggregate(1L, (v, i) => v * i.Test.Amount);
        foreach (var _ in Enumerable.Range(0, 10000))
        {
            foreach (var currentMonkey in Enumerable.Range(0, monkeys.Count))
            {
                monkeys[currentMonkey].Inspect(monkeys, l => l % reduce);
            }
        }

        var mostActiveMonkeys = monkeys.Select(x => x.Value.InspectedItems).OrderDescending().Take(2).ToArray();
        return mostActiveMonkeys[0] * mostActiveMonkeys[1];
    }

    private Dictionary<int, Monkey> Parse(IEnumerable<string> input)
    {
        var result = new Dictionary<int, Monkey>();
        var monkeySetups = input.Chunk(7);
        foreach (var setup in monkeySetups)
        {
            var startingItems = setup[1]
                .Substring(18)
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();
            var operationText = setup[2]
                .Substring(23)
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var operation = new Operation(Convert.ToChar(operationText[0]), operationText[1]);
            
            var testParsts = setup[3]
                .Substring(8)
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var test = new Test(testParsts[0], int.Parse(testParsts.Last()));
            var trueOp = int.Parse(setup[5]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Last());
            var falseOp = int.Parse(setup[4]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Last());
            
            result[result.Count] = new Monkey(startingItems, operation, test, trueOp, falseOp);
        }

        return result;
    }
    private record Monkey(List<long> Items, Operation Operation, Test Test, int TestFailedMonkey, int TestSucceededMonkey)
    {
        public long InspectedItems { get; private set; }

        public void Inspect(IReadOnlyDictionary<int, Monkey> otherMonkeys, Func<long, long> reducer)
        {
            for (var currentIndex = 0; currentIndex < Items.Count; currentIndex++)
            {
                var worryLevel = Items[currentIndex];
                InspectedItems++;
                
                var updated = reducer(Operation.Update(worryLevel));
                updated = (int)Math.Round((double)updated, MidpointRounding.ToZero);

                otherMonkeys[Test.Check(updated) ? TestSucceededMonkey : TestFailedMonkey].Catch(updated);
            }
            Items.Clear();
        }
        
        private void Catch(long worryLevel)
        {
            Items.Add(worryLevel);
        }
    }

    private record Operation(char Operator, string Amount)
    {
        public long Update(long currentLevel)
        {
            var operationIncrease = long.TryParse(Amount, out var intAmount)
                ? intAmount
                : currentLevel;
            return Operator switch
            {
                '+' => currentLevel + operationIncrease,
                '*' => currentLevel * operationIncrease,
                _ => currentLevel,
            };
        }
    }

    private record Test(string Operation, int Amount)
    {
        public bool Check(long worryLevel)
        {
            return Operation switch
            {
                "divisible" => worryLevel % Amount == 0,
                _ => false,
            };
        }
    }
}