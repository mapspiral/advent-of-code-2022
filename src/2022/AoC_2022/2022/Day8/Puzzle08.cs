[Puzzle(2022, 8)]
public sealed class Puzzle08 : PuzzleBase<int>
{
    public override int Solution1(IEnumerable<string> input)
    {
        var matrix = input.Select(l => l.Select(h => int.Parse(h.ToString())).ToArray()).ToArray();
        var visibleCount = 0;
        var rows = new Dictionary<int, Row>();
        var columns = matrix[0].Select(x => x).ToArray();
        for (var rowIndex = 1; rowIndex < matrix.Length - 1; rowIndex++)
        {
            rows[rowIndex] = new Row { Left = matrix[rowIndex][0], Right = matrix[rowIndex][1] };
            for (var columnIndex = 1; columnIndex < matrix[rowIndex].Length - 1; columnIndex++)
            {
                if (IsRowEdgeVisible(rowIndex, columnIndex) || IsColumnEdgeVisible(rowIndex, columnIndex))
                {
                    visibleCount++;
                }
                columns[columnIndex] = Math.Max(matrix[rowIndex][columnIndex], columns[columnIndex]);
            }
                        
        }
        var result = visibleCount + (matrix.Length * 2) + (matrix[0].Length-2)*2;
        return result;
        
        bool IsRowEdgeVisible(int rowIndex, int columnIndex)
        {
            var value = matrix[rowIndex][columnIndex];
            var toLeft = matrix[rowIndex].Take(columnIndex).Max();
            var toRight = matrix[rowIndex].Skip(columnIndex + 1).Max();
            return toLeft < value || toRight < value;
        }

        bool IsColumnEdgeVisible(int rowIndex, int columnIndex)
        {
            var value = matrix[rowIndex][columnIndex];
            var toTop = matrix.Take(rowIndex).Select(x => x[columnIndex]).Max();
            var toBottom = matrix.Skip(rowIndex + 1).Select(x => x[columnIndex]).Max();
            return toTop < value || toBottom < value;
        }
    }
    
    public override int Solution2(IEnumerable<string> input)
    {
        var matrix = input.Select(l => l.Select(h => int.Parse(h.ToString())).ToArray()).ToArray();
        var result = 0;
        for (var rowIndex = 1; rowIndex < matrix.Length - 1; rowIndex++)
        {
            for (var columnIndex = 1; columnIndex < matrix[rowIndex].Length -1; columnIndex++)
            {
                var value = matrix[rowIndex][columnIndex];
                var toLeft = matrix[rowIndex].Take(columnIndex).Reverse().While(NotBlocked).Count();
                var toRight = matrix[rowIndex].Skip(columnIndex + 1).While(NotBlocked).Count();
                var toTop = matrix.Take(rowIndex).Select(x => x[columnIndex]).Reverse().While(NotBlocked).Count();
                var toBottom = matrix.Skip(rowIndex + 1).Select(x => x[columnIndex]).While(NotBlocked).Count();
                result = Math.Max(result, toLeft * toRight * toTop * toBottom);

                bool NotBlocked(int x) => x >= value;
            }
        }
        return result;
    }

    record Row
    {
        public int Left { get; set; }
        public int Right { get; set; }
    }
}