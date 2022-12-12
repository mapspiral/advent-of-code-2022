internal sealed class Area
{
    public Area(IEnumerable<string> input)
    {
        var data = CreateMap(input.ToArray());

        Map = data.Map;
        Start = data.Start;
        Target = data.Target;
    }

    public (int, int) Target { get; }

    public (int, int) Start { get; }

    public IReadOnlyDictionary<(int X, int Y), Position> Map { get; }

    private ((int, int) Start, (int, int) Target, Dictionary<(int X, int Y), Position> Map) CreateMap(string[] input)
    {
        var result = new Dictionary<(int, int), Position>();
        (int, int) initial = default;
        (int, int) target = default;
        
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                var marker = line[x];
                if (marker == 'S')
                {
                    initial = (x, y);
                }
                else if (marker == 'E')
                {
                    target = (x, y);
                }

                var position = new Position(
                    (x, y),
                    marker switch
                    {
                        'S' => 'a',
                        'E' => 'z',
                        _ => marker,
                    });
                result[position.Location] = position;
            }
        }
        return (initial, target, result);
    }
}