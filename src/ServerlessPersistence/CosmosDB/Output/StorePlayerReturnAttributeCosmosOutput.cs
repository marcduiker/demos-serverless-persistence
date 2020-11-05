using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;

namespace ServerlessPersistence.CosmosDB.Output
{
    public static class StorePlayerReturnAttributeCosmosOutput
    {
        [FunctionName(nameof(StorePlayerReturnAttributeCosmosOutput))]
        [return: CosmosDB(
            CosmosDBConfig.Database, 
            CosmosDBConfig.Collection, 
            ConnectionStringSetting = CosmosDBConfig.ConnectionStringSetting)]
        public static async Task<Player> Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                nameof(HttpMethods.Post), 
                Route = null)] HttpRequestMessage message)
        {
            var player = await message.Content.ReadAsAsync<Player>();

            return player;
        }
    }
}
