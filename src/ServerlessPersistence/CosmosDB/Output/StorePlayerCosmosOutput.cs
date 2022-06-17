using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;

namespace ServerlessPersistence.CosmosDB.Output
{
    public static class StorePlayerCosmosOutput
    {
        [FunctionName(nameof(StorePlayerCosmosOutput))]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                "post", 
                Route = null)] Player playerInput,
            [CosmosDB(
                "gamedb", 
                "players", 
                ConnectionStringSetting = "CosmosDBConnectionGameDB")] out Player playerOutput)
        {
            playerOutput = playerInput;

            return new AcceptedResult();
        }
    }
}
