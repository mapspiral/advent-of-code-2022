[Puzzle(2022, 6)]
public sealed class Puzzle06 : PuzzleBase<int>
{    
    public override int Solution1(IEnumerable<string> input)
    {
        return input.Select(l => FindMarker(l, 4)).Sum();
    }

    public override int Solution2(IEnumerable<string> input)
    {
        return input.Select(l => FindMarker(l, 14)).Sum();
    }
    
    int FindMarker(string stream, int messageLength)
    {
        var window = new Dictionary<char, int>();
        for (var currentIndex = 0; currentIndex < stream.Length; currentIndex++)
        {
            window[stream[currentIndex]] = window.TryGetValue(stream[currentIndex], out var cv2)
                ? cv2+1
                : 1;
                
            if (currentIndex >= messageLength)
            {
                window[stream[currentIndex - messageLength]]--;
            }

            if (window.Count(x => x.Value == 1) == messageLength)
            {
                return currentIndex+1;
            }
                
                
        }

        return -1;
    }
}