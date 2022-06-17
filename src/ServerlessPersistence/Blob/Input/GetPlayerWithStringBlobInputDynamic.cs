using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace ServerlessPersistence.Blob.Output
{
    public static class GetPlayerWithStringBlobInputDynamic
    {
        [FunctionName(nameof(GetPlayerWithStringBlobInputDynamic))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                nameof(HttpMethods.Get),
                Route = null)] HttpRequest request,
            IBinder binder
        )
        {
            string name = request.Query["name"];

            IActionResult result;
            if (string.IsNullOrEmpty(name))
            {
                result = new BadRequestObjectResult("No player name in request.");
            }
            else
            {
                string content;
                var blobAttribute = new BlobAttribute($"players/in/dynamic-{name}.json");
                using (var input = await binder.BindAsync<TextReader>(blobAttribute))
                {
                    content = await input.ReadToEndAsync();
                }

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
