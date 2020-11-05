using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net.Http;
using System.Threading.Tasks;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;

namespace ServerlessPersistence.Table.Output
{
    public static class StorePlayerReturnAttributeTableOutput
    {
        [FunctionName(nameof(StorePlayerReturnAttributeTableOutput))]
        [return: Table(TableConfig.Table)]
        public static async Task<PlayerEntity> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] HttpRequestMessage message)
        {
            var playerEntity = await message.Content.ReadAsAsync<PlayerEntity>();

            return playerEntity;
        }
    }
}
