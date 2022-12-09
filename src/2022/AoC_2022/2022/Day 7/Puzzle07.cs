[Puzzle(2022, 7)]
public sealed class Puzzle07 : PuzzleBase<long>
{
    public override long Solution1(IEnumerable<string> input)
    {
        var root = ParseStructure(input.ToArray());
        return root.DirectoriesWithSizeOf(100000, SizeOperation.Minimum).Sum(x => x.Size());
    }

    public override long Solution2(IEnumerable<string> input)
    {
        var diskSize = 70000000;
        var requiredSize = 30000000;
        var root = ParseStructure(input.ToArray());
        var unused = diskSize - root.Size();
        return root.DirectoriesWithSizeOf(requiredSize - unused, SizeOperation.Maximum).Min(x => x.Size());
    }

    private Dir ParseStructure(string[] input)
    {
        Dir root = new Dir(null, "/");
        Dir? current = null;
        foreach (var commandOutput in string.Join('\n', input).Split('$', StringSplitOptions.RemoveEmptyEntries))
        {
            var output = commandOutput.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToArray();
            var command = output.First().Split(" ", StringSplitOptions.TrimEntries);
            if (command[0] == "cd")
            { 
                ChangeDirectory(command);   
            }
            else if (command[0] == "ls")
            {
                List(output.Skip(1));
            }
        }

        return root;

        void ChangeDirectory(string[] command)
        {
            if (command[1] == "/")
            {
                current = root;
                return;
            }
            
            if (command[1] == "..")
            {
                current = current!.Parent;
                return;
            }

            current = current!.Directories[command[1]];
        }

        void List(IEnumerable<string> output)
        {
            foreach (var entry in output)
            {
                var entryParts = entry.Split(" ", StringSplitOptions.TrimEntries);
                if (entryParts[0] == "dir")
                {
                    var newDir = new Dir(current, entryParts[1]);
                    current!.Directories[newDir.Name] = newDir;
                }
                else
                {
                    var newFile = new File(entryParts[1], Convert.ToInt32(entryParts[0]));
                    current!.Files[newFile.Name] = newFile;
                }
            }
        }
    }

    private record Dir(Dir? Parent, string Name)
    {
        private long? _size;
        public Dictionary<string, Dir> Directories { get; } = new ();
        public Dictionary<string, File> Files { get; } = new ();

        public long Size() => _size ??= Directories.Values.Sum(x => x.Size()) + Files.Sum(x => x.Value.Size);

        public IEnumerable<Dir> DirectoriesWithSizeOf(long size, SizeOperation operation)
        {
            var actualSize = Size();
            var isMatch = operation switch
            {
                SizeOperation.Minimum => actualSize <= size,
                SizeOperation.Maximum => actualSize >= size,
                _ => false,
            };

            if (isMatch)
            {
                yield return this;
            }

            foreach (var dir in Directories.SelectMany(x => x.Value.DirectoriesWithSizeOf(size, operation)))
            {
                yield return dir;
            }
        }

        public override string ToString() => Name;
    }

    private enum SizeOperation
    {
        Unknown = 0,
        Minimum,
        Maximum,
    }

    private record File(string Name, long Size);
}