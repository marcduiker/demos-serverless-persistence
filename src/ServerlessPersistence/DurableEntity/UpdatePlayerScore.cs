using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace ServerlessPersistence.DurableEntity
{
    public static class UpdatePlayerScore
    {
        [FunctionName(nameof(UpdatePlayerScore))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Get),
                nameof(HttpMethods.Post),
                Route = "UpdatePlayerScore/{playerName}/{points?}" )] HttpRequestMessage message,
                string playerName,
                string points,
                [DurableClient] IDurableClient durableClient
        )
        {
            var entityId = new EntityId(nameof(PlayerScore), playerName);
            string responseMessage = default;
            if (message.Method.Method == HttpMethods.Post)
            {
                int pointsValue = int.Parse(points);
                await durableClient.SignalEntityAsync<IPlayerScore>(
                    entityId,
                    playerScore => playerScore.Add(pointsValue));
                responseMessage = $"Added {pointsValue} points for player {playerName}.";
            }
            else if (message.Method.Method == HttpMethods.Get)
            {
                var entityResponse = await durableClient.ReadEntityStateAsync<PlayerScore>(entityId);
                if (entityResponse.EntityExists)
                {
                    responseMessage = $"{playerName} has a highscore of {entityResponse.EntityState.CurrentValue} points.";
                }
                else 
                {
                    responseMessage = $"No data available for {playerName}.";
                }
            }

            return new OkObjectResult(responseMessage);
        }
    }
}