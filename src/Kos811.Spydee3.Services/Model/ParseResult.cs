namespace Kos811.Spydee3.Services.Model;

public readonly struct ParseResult
{
    public ParseResult()
    {
    }

    public HashSet<Uri> Links { get; } = new();
}
