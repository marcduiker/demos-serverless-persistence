using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;

namespace ServerlessPersistence
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PlayerScore : IPlayerScore
    {
        [JsonProperty("value")]
        public int HighScore { get; set; }

        public void Add(int points) => this.HighScore += points;

        public void Reset() => this.HighScore = 0;

        [FunctionName(nameof(PlayerScore))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx)
            => ctx.DispatchAsync<PlayerScore>();
    }
}
