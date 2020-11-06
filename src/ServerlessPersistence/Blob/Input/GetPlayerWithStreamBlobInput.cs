using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace ServerlessPersistence.Blob.Input
{
    public static class GetPlayerWithStreamBlobInput
    {
        [FunctionName(nameof(GetPlayerWithStreamBlobInput))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Get),
                Route = "GetPlayerWithStreamBlobInput/{id}")] HttpRequest request,
            string id,
            [Blob(
                "players/in/player-{id}.json",
                FileAccess.Read)] Stream playerStream
        )
        {
            IActionResult result;
            if (string.IsNullOrEmpty(id))
            {
                result = new BadRequestObjectResult("No player id route.");
            }
            else
            {
                using var reader = new StreamReader(playerStream);
                var content = await reader.ReadToEndAsync();
                result = new ContentResult 
                { 
                    Content = content,
                    ContentType = MediaTypeNames.Application.Json,
                    StatusCode = 200
                };
            }

            return result;
        }
    }
}
