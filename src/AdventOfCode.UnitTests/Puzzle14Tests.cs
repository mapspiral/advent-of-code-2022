public sealed class Puzzle14Tests : TestBase<Puzzle14>
{
    [Fact]
    public void HaveCorrectSolution1_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("sample.txt"));
        
        solution.Should().Be(24);
    }
    
    [Fact]
    public void HaveCorrectSolution1_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("input.txt"));
        
        solution.Should().Be(793);
    }
    
    [Fact]
    public void HaveCorrectSolution2_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("sample.txt"));
        solution.Should().Be(24);
    }
    
    [Fact]
    public void HaveCorrectSolution2_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("input.txt"));
        solution.Should().Be(24166);
    }
}