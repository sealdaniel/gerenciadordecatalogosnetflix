using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

public static class ListMoviesFunction
{
    [FunctionName("ListMoviesFunction")]
    public static async Task<IActionResult> Run(
        [CosmosDB(
            databaseName: "NetflixCatalog",
            collectionName: "Movies",
            ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
        ILogger log)
    {
        var movies = client.CreateDocumentQuery<Movie>(
            UriFactory.CreateDocumentCollectionUri("NetflixCatalog", "Movies")).AsEnumerable();
        return new OkObjectResult(movies);
    }
}
