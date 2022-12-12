public sealed class Puzzle06Tests : TestBase<Puzzle06>
{
    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void HaveCorrectSolution1_Sample(string input, int expectedResult)
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(new[] { input });
        
        solution.Should().Be(expectedResult);
    }
    
    [Fact]
    public void HaveCorrectSolution1_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution1(GetInput("input.txt"));
        
        solution.Should().Be(1804);
    }
    
    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void HaveCorrectSolution2_Sample(string input, int expectedResult)
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(new[] { input });
        
        solution.Should().Be(expectedResult);
    }
    
    [Fact]
    public void HaveCorrectSolution2_Input()
    {
        var sut = CreatePuzzle();
        var solution = sut.Solution2(GetInput("input.txt"));
        
        solution.Should().Be(2508);
    }
}