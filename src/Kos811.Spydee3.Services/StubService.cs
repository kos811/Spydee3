namespace Kos811.Spydee3.Services;

public class StubService
{
    public int GetAnswer(string question)
    {
        return question switch
        {
            string str when string.IsNullOrWhiteSpace(str) => throw new ArgumentOutOfRangeException(),
            _ => 42
        };
    }
}
