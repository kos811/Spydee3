namespace Kos811.Spydee3.Domain;

public readonly struct Page
{
    public Page(
        Uri uri,
        int version,
        string? responseReasonPhrase,
        string? responseContent)
    {
        Uri = uri;
        Version = version;
        ResponseReasonPhrase = responseReasonPhrase;
        ResponseContent = responseContent;
    }

    public Uri Uri { get; }
    public int Version { get; }
    public string? ResponseReasonPhrase { get; }
    public string? ResponseContent { get; }
}
