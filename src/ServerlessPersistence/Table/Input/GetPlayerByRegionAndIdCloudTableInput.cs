using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace c.Table.Input
{
    public static class GetPlayerByRegionAndIdTableInput
    {
        [FunctionName(nameof(GetPlayerByRegionAndIdTableInput))]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Get),
                Route = "GetPlayerByRegionAndIdTableInput/{region}/{id}")] HttpRequest request,
            string region,
            string id,
            [Table(
                "players",
                "{region}",
                "{id}")] PlayerEntity playerEntity)
        {
            return new OkObjectResult(playerEntity);
        }
    }
}
