using System.Net;
using System.Net.Http.Headers;

namespace Kos811.Spydee3.Services.Model;

public readonly struct DownloadResult
{
    public DownloadResult(
        Uri uri,
        HttpStatusCode responseStatusCode,
        string? responseReasonPhrase,
        HttpResponseHeaders responseHeaders,
        string? responseContent)
    {
        Uri = uri;
        ResponseStatusCode = responseStatusCode;
        ResponseReasonPhrase = responseReasonPhrase;
        ResponseHeaders = responseHeaders;
        ResponseContent = responseContent;
    }

    public Uri Uri { get; }
    public HttpStatusCode ResponseStatusCode { get; }
    public string? ResponseReasonPhrase { get; }
    public HttpResponseHeaders ResponseHeaders { get; }
    public string? ResponseContent { get; }
}
