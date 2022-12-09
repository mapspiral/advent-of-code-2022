using System.Reflection;
using AdventOfCode.Shared;

public sealed class Runner
{
    private readonly Assembly _target;

    public Runner(Assembly target)
    {
        _target = target;
    }

    public void Print()
    {
        var puzzleTypes = _target
            .GetTypes()
            .Where(x => x.GetCustomAttribute<PuzzleAttribute>() != null)
            .Select<Type, (Type Type, PuzzleAttribute Metadata)>(t => (t, t.GetCustomAttributes<PuzzleAttribute>().First()));

        foreach (var puzzles in puzzleTypes.OrderByDescending(x => x.Metadata.Year).ThenByDescending(x => x.Metadata.Day))
        {
            var puzzle =  Activator.CreateInstance(puzzles.Type) as PuzzleBase ?? throw new InvalidCastException("Puzzle attribute applied to unsupported type");
            var contentRoot = RunnerHelpers.FindContentDirectory(_target) ?? throw new Exception("Cannot find content directory");
            var puzzleData = contentRoot
                .GetDirectories(puzzles.Metadata.Year.ToString())
                .FirstOrDefault()?
                .GetDirectories(puzzles.Metadata.Day.ToString())
                .FirstOrDefault() ?? throw new Exception("Cannot find puzzle data directory");
            
            var header = $"Puzzle {puzzles.Metadata.Year}.{puzzles.Metadata.Day}";
            Console.WriteLine(header);
            Console.WriteLine(new String('=', header.Length));
            foreach (var input in puzzleData.GetFiles("*.txt"))
            {
                var solutions = puzzle.Solve(input);
                Console.WriteLine(input.Name);
                Console.WriteLine($"\tSolution 1: {solutions.Solution1} ");
                Console.WriteLine($"\tSolution 2: {solutions.Solution2} ");
            }

            Console.WriteLine();
        }
    }
    
    
}