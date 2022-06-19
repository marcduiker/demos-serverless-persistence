using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ServerlessPersistence.Blob.Output
{
    public static class StorePlayerWithStringBlobOutput
    {
        [FunctionName(nameof(StorePlayerWithStringBlobOutput))]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] Player player,
            [Blob(
                "players/out/string-{id}.json",
                FileAccess.Write)] out string playerBlob
        )
        {
            playerBlob = default;
            IActionResult result;

            if (player == null)
            {
                result = new BadRequestObjectResult("No player data in request.");
            }
            else
            {
                var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };
                playerBlob = JsonSerializer.Serialize(player, serializerOptions);
                result = new AcceptedResult();
            }

            return result;
        }
    }
}
