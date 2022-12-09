public sealed class Puzzle07Tests : TestBase<Puzzle07>
{
    [Fact]
    public void HaveCorrectSolution1_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("sample.txt"));
        
        solution.Should().Be(95437);
    }
    
    [Fact]
    public void HaveCorrectSolution1_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("input.txt"));
        
        solution.Should().Be(2061777);
    }
    
    [Fact]
    public void HaveCorrectSolution2_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("sample.txt"));
        
        solution.Should().Be(24933642);
    }
    
    [Fact]
    public void HaveCorrectSolution2_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("input.txt"));
        
        solution.Should().Be(4473403);
    }
}