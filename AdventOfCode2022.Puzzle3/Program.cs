int Solution1(string[] input)
{
    return input.Select(l => GetItemPriority(GetCommonItem(l))).Sum();
    
    char GetCommonItem(string tempLine)
    {
        var compartment1 = tempLine.Take(tempLine.Length / 2).ToArray();
        var compartment2 = tempLine.Skip(tempLine.Length / 2).ToArray();
        var intersection = compartment1.Intersect(compartment2);
        return intersection.First();
    }
}

int Solution2(string[] input)
{
    return input.Chunk(3).Select(l => GetItemPriority(GetCommonItem(l))).Sum();
    
    char GetCommonItem(ICollection<string> groups)
    {
        SortedSet<char>? remainingItems = null;
        foreach (var itemsInBackpack in groups.Select(x => new SortedSet<char>(x)))
        {
            remainingItems = remainingItems == null 
                ? itemsInBackpack
                : new SortedSet<char>(remainingItems.Intersect(itemsInBackpack));
        }

        return remainingItems?.First() ?? '?';
    }
}

int GetItemPriority(char item)
{
    return item switch
    {
        >= 'a' and <= 'z' => 1 + item - 'a',
        >= 'A' and <= 'Z' => 27 + item - 'A',
        _ => -1,
    };
}

var sampleInput = File.ReadAllLines("sample.txt");
Console.WriteLine("Solution sample:");
Console.WriteLine(Solution1(sampleInput));
Console.WriteLine(Solution2(sampleInput));

var input = File.ReadAllLines("input.txt");
Console.WriteLine("Solution:");
Console.WriteLine(Solution1(input));
Console.WriteLine(Solution2(input));

Console.ReadKey();