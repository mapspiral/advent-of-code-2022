public sealed class Puzzle12Tests : TestBase<Puzzle12>
{
    [Fact]
    public void HaveCorrectSolution1_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("sample.txt"));
        
        solution.Should().Be(31);
    }
    
    [Fact]
    public void HaveCorrectSolution1_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("input.txt"));
        
        solution.Should().Be(412);
    }
    
    [Fact]
    public void HaveCorrectSolution2_Sample()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("sample.txt"));
        solution.Should().Be(29);
    }
    
    [Fact]
    public void HaveCorrectSolution2_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("input.txt"));
        solution.Should().Be(402);
    }
}