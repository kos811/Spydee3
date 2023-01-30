using HtmlAgilityPack;
using Kos811.Spydee3.Domain;
using Kos811.Spydee3.Services.Model;

namespace Kos811.Spydee3.Services;

public class PageParser
{
    private readonly UriBuilder _uriBuilder = new();

    public ParseResult Parse(Page page)
    {
        var parseResult = new ParseResult();
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(page.ResponseContent);

        var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]")?.ToArray() ?? Array.Empty<HtmlNode>();

        foreach (var link in links)
        {
            var linkurl = link.Attributes["href"].Value;

            if (!Uri.TryCreate(linkurl, UriKind.RelativeOrAbsolute, out var uri))
                continue;

            var newUri = uri;

            if (!uri.IsAbsoluteUri)
            {
                _uriBuilder.Scheme = page.Uri.Scheme;
                _uriBuilder.Host = page.Uri.Host;
                _uriBuilder.Path = uri.OriginalString;
                newUri = _uriBuilder.Uri;
            }

            parseResult.Links.Add(newUri);
        }

        return parseResult;
    }
}
