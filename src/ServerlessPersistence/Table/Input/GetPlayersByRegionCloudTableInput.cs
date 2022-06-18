using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Azure.Data.Tables;
using System.Linq;

namespace ServerlessPersistence.Table.Input
{
    public static class GetPlayersByRegionTableClient
    {
        [FunctionName(nameof(GetPlayersByRegionTableClient))]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Get),
                Route = null)] HttpRequest request,
            [Table("players")] TableClient tableClient)
        {
            string region = request.Query["region"];
            var playerEntities = tableClient.Query<PlayerEntity>(entity => entity.Region == region);

            return new OkObjectResult(playerEntities.ToList());
        }
    }
}
