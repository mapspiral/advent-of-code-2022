public sealed class Puzzle05Tests : TestBase<Puzzle05>
{
    [Fact]
    public void HaveCorrectSolution1_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("sample.txt"));
        
        solution.Should().Be("CMZ");
    }
    
    [Fact]
    public void HaveCorrectSolution1_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("input.txt"));
        
        solution.Should().Be("LJSVLTWQM");
    }
    
    [Fact]
    public void HaveCorrectSolution2_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("sample.txt"));
        
        solution.Should().Be("MCD");
    }
    
    [Fact]
    public void HaveCorrectSolution2_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("input.txt"));
        
        solution.Should().Be("BRQWDBBJM");
    }
}