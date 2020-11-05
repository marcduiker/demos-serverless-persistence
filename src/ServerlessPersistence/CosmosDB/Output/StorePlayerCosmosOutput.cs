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
                CosmosDBConfig.Database, 
                CosmosDBConfig.Collection, 
                ConnectionStringSetting = CosmosDBConfig.ConnectionStringSetting)] out Player playerOutput)
        {
            playerOutput = playerInput;

            return new AcceptedResult();
        }
    }
}
