using ClickHouse.Client;
using ClickHouse.Client.ADO;
using ClickHouse.Client.Copy;
using ClickHouse.Client.Utility;
using Kos811.Spydee3.Domain;

namespace Kos811.Spydee3.DataAccess.Repositories;

public class PageRepository
{
    private readonly IClickHouseConnection _clickHouseConnection;

    public PageRepository(IClickHouseConnection clickHouseConnection) => _clickHouseConnection = clickHouseConnection;

    public async Task AddAsync(Page page, CancellationToken cancellationToken)
    {
        _clickHouseConnection.Open();
        await using var command = _clickHouseConnection.CreateCommand();
        command.CommandText = @"
INSERT INTO pages (page_uri, page_version, page_content, page_created_at)
VALUES ({page_uri:text}, {page_version:Int32}, {page_content:String}, now());
";
        command.AddParameter("page_uri", page.Uri.AbsoluteUri);
        command.AddParameter("page_version", page.Version);
        command.AddParameter("page_content", page.ResponseContent!);

        await command.ExecuteNonQueryAsync(cancellationToken);

        _clickHouseConnection.Close();
    }

    public async Task AddBatchAsync(Page page, CancellationToken cancellationToken)
    {
        using var bulkCopyInterface = new ClickHouseBulkCopy((ClickHouseConnection)_clickHouseConnection)
        {
            DestinationTableName = "default.pages",
            BatchSize = 100000
        };

        var rows = new[] { new object[]
            { page.Uri, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), page.ResponseContent!, DateTimeOffset.UtcNow }
        };
        await bulkCopyInterface.WriteToServerAsync(rows, cancellationToken);
    }
}
