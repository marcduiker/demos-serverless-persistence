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
            [CosmosDB(
                CosmosDBConfig.Database, 
                CosmosDBConfig.Collection,
                ConnectionStringSetting = CosmosDBConfig.ConnectionStringSetting)] DocumentClient documentClient)
        {
            string region = request.Query["region"];
            var collectionUri = UriFactory.CreateDocumentCollectionUri(CosmosDBConfig.Database, CosmosDBConfig.Collection);
            var query = documentClient.CreateDocumentQuery<Player>(
                collectionUri,
                new FeedOptions() { 
                    PartitionKey = new PartitionKey(region) })
                .Where(player => player.NickName != "")
                .AsDocumentQuery();

            var playerNames = new List<string>();
            while (query.HasMoreResults)
            {
                foreach (var player in await query.ExecuteNextAsync<Player>())
                {
                    playerNames.Add(player.NickName);
                }
            }

            return new OkObjectResult(playerNames);
        }
    }
}
