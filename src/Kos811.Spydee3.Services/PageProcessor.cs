using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace Kos811.Spydee3.Services;

public class PageDownloader
{
    private readonly ILogger<PageDownloader> _logger;
    private readonly HttpClient _httpClient;

    public PageDownloader(ILogger<PageDownloader> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<DownloadResult> Download(Uri uri)
    {
        try
        {
            _httpClient.BaseAddress = uri;
            var response = await _httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();
            var result = new DownloadResult(response.StatusCode, response.ReasonPhrase, response.Headers, content);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("URL={2} {0} \r\n{1}", ex.Message, ex.StackTrace, uri);
            throw;
        }
    }
}

public readonly struct DownloadResult
{
    public DownloadResult(
        HttpStatusCode responseStatusCode,
        string? responseReasonPhrase,
        HttpResponseHeaders responseHeaders,
        string? responseContent)
    {
        ResponseStatusCode = responseStatusCode;
        ResponseReasonPhrase = responseReasonPhrase;
        ResponseHeaders = responseHeaders;
        ResponseContent = responseContent;
    }

    public HttpStatusCode ResponseStatusCode { get; }
    public string? ResponseReasonPhrase { get; }
    public HttpResponseHeaders ResponseHeaders { get; }
    public string? ResponseContent { get; }
}
