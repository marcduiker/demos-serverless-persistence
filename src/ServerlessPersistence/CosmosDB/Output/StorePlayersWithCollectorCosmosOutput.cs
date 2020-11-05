using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;
using ServerlessPersistence.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ServerlessPersistence.CosmosDB.Output
{
    public static class StorePlayersWithCollectorCosmosOutput
    {
        [FunctionName(nameof(StorePlayersWithCollectorCosmosOutput))] 
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                nameof(HttpMethods.Post), 
                Route = null)] HttpRequestMessage message,
            [CosmosDB(CosmosDBConfig.Database, CosmosDBConfig.Collection, ConnectionStringSetting = CosmosDBConfig.ConnectionStringSetting)] IAsyncCollector<Player> collector)
        {
            var players = await message.Content.ReadAsAsync<IEnumerable<Player>>();

            foreach (var player in players)
            {
                await collector.AddAsync(player);
            }

            return new AcceptedResult();
        }
    }
}
