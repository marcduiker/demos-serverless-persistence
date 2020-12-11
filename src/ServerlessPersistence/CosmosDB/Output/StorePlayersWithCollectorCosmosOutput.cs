using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using ServerlessPersistence.Models;
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
                Route = null)] Player[] players,
            [CosmosDB(CosmosDBConfig.Database, CosmosDBConfig.Collection, ConnectionStringSetting = CosmosDBConfig.ConnectionStringSetting)] IAsyncCollector<Player> collector)
        {
            foreach (var player in players)
            {
                await collector.AddAsync(player);
            }

            return new AcceptedResult();
        }
    }
}
