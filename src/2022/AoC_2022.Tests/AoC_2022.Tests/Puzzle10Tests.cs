public sealed class Puzzle10Tests : TestBase<Puzzle10>
{
    [Fact]
    public void HaveCorrectSolution1_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("sample.txt"));
        
        solution.Should().Be("13140");
    }
    
    [Fact]
    public void HaveCorrectSolution1_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("input.txt"));
        
        solution.Should().Be("15140");
    }
    
    [Fact]
    public void HaveCorrectSolution2_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("sample.txt"));
        solution.Should().Be(
            "##..##..##..##..##..##..##..##..##..##.." + Environment.NewLine +
            "###...###...###...###...###...###...###." + Environment.NewLine +
            "####....####....####....####....####...." + Environment.NewLine +
            "#####.....#####.....#####.....#####....." + Environment.NewLine +
            "######......######......######......####" + Environment.NewLine +
            "#######.......#######.......#######.....");
    }
    
    [Fact]
    public void HaveCorrectSolution2_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("input.txt"));
        solution.Should().Be(
            "###..###....##..##..####..##...##..###.." + Environment.NewLine +
            "#..#.#..#....#.#..#....#.#..#.#..#.#..#." + Environment.NewLine +
            "###..#..#....#.#..#...#..#....#..#.#..#." + Environment.NewLine +
            "#..#.###.....#.####..#...#.##.####.###.." + Environment.NewLine +
            "#..#.#....#..#.#..#.#....#..#.#..#.#...." + Environment.NewLine +
            "###..#.....##..#..#.####..###.#..#.#....");
    }
}