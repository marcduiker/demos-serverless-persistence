using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System.Linq;
using Microsoft.Azure.Documents.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ServerlessPersistence.CosmosDB.Input
{
    public static class GetPlayersByRegionDocumentClientCosmosInput
    {
        [FunctionName(nameof(GetPlayersByRegionDocumentClientCosmosInput))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Get),
                Route = null)] HttpRequest request,
            [CosmosDB(ConnectionStringSetting = "CosmosDBConnectionGameDB")] DocumentClient documentClient)
        {
            string region = request.Query["region"];
            var collectionUri = UriFactory.CreateDocumentCollectionUri("gamedb", "players");
            var query = documentClient.CreateDocumentQuery<Player>(
                collectionUri,
                new FeedOptions() { 
                    PartitionKey = new PartitionKey(region) })
                .Where(player => player.Email != "")
                .Select(player => player.Name)
                .AsDocumentQuery();

            var names = new List<string>();
            while (query.HasMoreResults)
            {
                foreach (var name in await query.ExecuteNextAsync<string>())
                {
                    names.Add(name);
                }
            }

            return new OkObjectResult(names);
        }
    }
}
