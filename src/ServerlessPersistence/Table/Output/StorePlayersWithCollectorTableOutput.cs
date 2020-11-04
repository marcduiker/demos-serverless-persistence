using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;
using ServerlessPersistence.Models;
using System.Collections.Generic;

namespace ServerlessPersistence.Table.Output
{
    public static class StorePlayersWithCollectorTableOutput
    {
        [FunctionName(nameof(StorePlayersWithCollectorTableOutput))] 
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage message,
            [Table("players")] IAsyncCollector<PlayerEntity> collector)
        {
            var playerEntities = await message.Content.ReadAsAsync<IEnumerable<PlayerEntity>>();

            foreach (var playerEntity in playerEntities)
            {
                await collector.AddAsync(playerEntity);
            }

            return new AcceptedResult();
        }
    }
}
