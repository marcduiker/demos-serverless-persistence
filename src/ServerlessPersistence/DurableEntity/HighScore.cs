using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;

namespace ServerlessPersistence
{
    [JsonObject(MemberSerialization.OptIn)]
    public class HighScore : IHighScoreOperations
    {
        [JsonProperty("value")]
        public int CurrentValue { get; set; }

        public void Add(int points) => this.CurrentValue += points;

        public void Reset() => this.CurrentValue = 0;

        [FunctionName(nameof(HighScore))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx)
            => ctx.DispatchAsync<HighScore>();
    }
}
