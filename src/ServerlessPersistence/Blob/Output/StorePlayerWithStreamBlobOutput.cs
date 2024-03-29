using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ServerlessPersistence.Blob.Output
{
    public static class StorePlayerWithStreamBlobOutput
    {
        [FunctionName(nameof(StorePlayerWithStreamBlobOutput))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] Player player,
            [Blob(
                "players/out/stream-{id}.json",
                FileAccess.Write)] Stream playerStream
        )
        {
            IActionResult result;
            if (player == null)
            {
                result = new BadRequestObjectResult("No player data in request.");
            }
            else
            {
                using var writer = new StreamWriter(playerStream);
                var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };
                var jsonData = JsonSerializer.Serialize(player, serializerOptions);
                await writer.WriteLineAsync(jsonData);

                result = new AcceptedResult();
            }

            return result;
        }
    }
}
