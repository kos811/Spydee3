using Kos811.Spydee3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kos811.Spydee3.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IndexerController : ControllerBase
{
    private readonly ILogger<IndexerController> _logger;
    private readonly PageDownloader _pageDownloader;

    public IndexerController(ILogger<IndexerController> logger, PageDownloader pageDownloader)
    {
        _logger = logger;
        _pageDownloader = pageDownloader;
    }

    [HttpGet("ProcessPage")]
    public async Task<IActionResult> ProcessPage(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return BadRequest($"Cant parse uri: '{url}'");

        _logger.LogInformation("Uri '{uri}' parsed successfully.", uri);

        var result = await _pageDownloader.Download(uri);

        return Ok(result);
    }
}
