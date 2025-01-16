using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

public static class FilterMoviesFunction
{
    [FunctionName("FilterMoviesFunction")]
    public static async Task<IActionResult> Run(
        [CosmosDB(
            databaseName: "NetflixCatalog",
            collectionName: "Movies",
            ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
        string genre, ILogger log)
    {
        var query = client.CreateDocumentQuery<Movie>(
            UriFactory.CreateDocumentCollectionUri("NetflixCatalog", "Movies"),
            $"SELECT * FROM c WHERE c.Genre = '{genre}'").AsEnumerable();
        return new OkObjectResult(query);
    }
}
