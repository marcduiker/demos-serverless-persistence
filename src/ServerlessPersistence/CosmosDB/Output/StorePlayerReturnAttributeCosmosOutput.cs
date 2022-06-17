using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;

namespace ServerlessPersistence.CosmosDB.Output
{
    public static class StorePlayerReturnAttributeCosmosOutput
    {
        [FunctionName(nameof(StorePlayerReturnAttributeCosmosOutput))]
        [return: CosmosDB(
            "gamedb", 
            "players", 
            ConnectionStringSetting = "CosmosDBConnectionGameDB")]
        public static Player Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                nameof(HttpMethods.Post), 
                Route = null)] Player player)
        {
            return player;
        }
    }
}
