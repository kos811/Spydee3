using Kos811.Spydee3.Services.Model;

namespace Kos811.Spydee3.Services;

public class PageDownloader
{
    private readonly HttpClient _httpClient;

    public PageDownloader(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<DownloadResult> Download(Uri uri)
    {
        _httpClient.BaseAddress = uri;
        var response = await _httpClient.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();
        var result = new DownloadResult(response?.RequestMessage?.RequestUri ?? uri, response!.StatusCode, response.ReasonPhrase, response.Headers, content);
        return result;
    }
}
