using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Net.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;

namespace ServerlessPersistence.Blob.Output
{
    public static class StorePlayerWithStreamBlobOutput
    {
        [FunctionName(nameof(StorePlayerWithStreamBlobOutput))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] HttpRequestMessage message,
            [Blob(
                "players/out/stream-{rand-guid}.json",
                FileAccess.Write)] Stream playerStream
        )
        {
            var player = await message.Content.ReadAsAsync<Player>();

            IActionResult result;
            if (player == null)
            {
                result = new BadRequestObjectResult("No player data in request.");
            }
            else
            {
                result = new AcceptedResult();
            }

            using var writer = new StreamWriter(playerStream);
            var jsonData = JsonConvert.SerializeObject(player);
            await writer.WriteLineAsync(jsonData);

            return result;
        }
    }
}
