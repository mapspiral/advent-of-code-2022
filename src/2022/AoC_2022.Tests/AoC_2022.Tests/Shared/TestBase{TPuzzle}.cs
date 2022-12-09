using System.Reflection;
using AdventOfCode.Shared;

public abstract class TestBase<TPuzzle>
    where TPuzzle : PuzzleBase
{
    protected TPuzzle CreatePuzzle() => Activator.CreateInstance<TPuzzle>();

    protected IEnumerable<string> GetInput(string fileName) => File.ReadLines(ContentDirectory.GetFiles(fileName).First().FullName);

    private DirectoryInfo ContentDirectory
    {
        get
        {
            var metadata = typeof(TPuzzle).GetCustomAttributes().OfType<PuzzleAttribute>().First();
            return RunnerHelpers.FindContentDirectory(GetType().Assembly)?
                .GetDirectories($"{metadata.Year}")
                .First()
                .GetDirectories($"{metadata.Day}")
                .First()!;
        }
    }
}