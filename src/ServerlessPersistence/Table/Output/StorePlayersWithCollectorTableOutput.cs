using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;

namespace ServerlessPersistence.Table.Output
{
    public static class StorePlayersWithCollectorTableOutput
    {
        [FunctionName(nameof(StorePlayersWithCollectorTableOutput))] 
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] PlayerEntity[] playerEntities,
            [Table("players")] IAsyncCollector<PlayerEntity> collector)
        {
            foreach (var playerEntity in playerEntities)
            {
                playerEntity.SetKeys();
                await collector.AddAsync(playerEntity);
            }

            return new AcceptedResult();
        }
    }
}
