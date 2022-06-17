using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ServerlessPersistence.Blob.Output
{
    public static class StorePlayerWithStringBlobOutputDynamic
    {
        [FunctionName(nameof(StorePlayerWithStringBlobOutputDynamic))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] Player player,
            IBinder binder
        )
        {
            IActionResult result;
            if (player == null)
            {
                result = new BadRequestObjectResult("No player data in request.");
            }
            else
            {
                var blobAttribute = new BlobAttribute($"players/out/dynamic-{player.Name}.json");
                using (var output = await binder.BindAsync<TextWriter>(blobAttribute))
                {
                    var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };
                    await output.WriteAsync(JsonSerializer.Serialize(player, serializerOptions));
                }

                result = new AcceptedResult();
            }

            return result;
        }
    }
}
