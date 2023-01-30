using Kos811.Spydee3.DataAccess.Repositories;
using Kos811.Spydee3.Domain;
using Kos811.Spydee3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kos811.Spydee3.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IndexerController : ControllerBase
{
    private readonly ILogger<IndexerController> _logger;
    private readonly PageDownloader _pageDownloader;
    private readonly PageRepository _pageRepository;
    private readonly PageParser _pageParser;

    public IndexerController(ILogger<IndexerController> logger, PageDownloader pageDownloader, PageRepository pageRepository, PageParser pageParser)
    {
        _logger = logger;
        _pageDownloader = pageDownloader;
        _pageRepository = pageRepository;
        _pageParser = pageParser;
    }

    [HttpGet("ProcessPage")]
    public async Task<IActionResult> ProcessPage(string url, CancellationToken cancellationToken)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return BadRequest($"Cant parse uri: '{url}'");

        _logger.LogInformation("Uri '{uri}' parsed successfully.", uri);

        var result = await _pageDownloader.Download(uri);

#pragma warning disable CS0162
        var page = new Page(result.Uri, 1, result.ResponseReasonPhrase, result.ResponseContent);
#pragma warning restore CS0162

        //await _pageRepository.AddAsync(page, cancellationToken);
        await _pageRepository.AddBatchAsync(page, cancellationToken);
        var parseResult = _pageParser.Parse(page);

        return Ok(parseResult);
    }
}
