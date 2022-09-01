using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ServerlessPersistence.CosmosDB.Input
{
    public static class GetPlayerByRegionAndIdCosmosInput
    {
        [FunctionName(nameof(GetPlayerByRegionAndIdCosmosInput))]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                nameof(HttpMethods.Get), 
                Route = "GetPlayerByRegionAndIdCosmosInput/{region}/{id}")] HttpRequest request,
            [CosmosDB(
                "gamedb", 
                "players",
                ConnectionStringSetting = "CosmosDBConnectionGameDB",
                PartitionKey = "{region}",
                Id =  "{id}")] Player player)
        {
            return new OkObjectResult(player);
        }
    }
}
