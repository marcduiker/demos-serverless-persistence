using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;
using ServerlessPersistence.Models;

namespace ServerlessPersistence.Table.Output
{
    public static class StorePlayerReturnAttributeTableOutput
    {
        [FunctionName(nameof(StorePlayerReturnAttributeTableOutput))]
        [return: Table("players")] 
        public static async Task<PlayerEntity> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage message)
        {
            var playerEntity = await message.Content.ReadAsAsync<PlayerEntity>();

            return playerEntity;
        }
    }
}
