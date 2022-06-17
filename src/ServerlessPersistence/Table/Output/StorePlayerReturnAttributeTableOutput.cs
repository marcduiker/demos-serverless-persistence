using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using ServerlessPersistence.Models;
using Microsoft.AspNetCore.Http;

namespace ServerlessPersistence.Table.Output
{
    public static class StorePlayerReturnAttributeTableOutput
    {
        [FunctionName(nameof(StorePlayerReturnAttributeTableOutput))]
        [return: Table("players")]
        public static PlayerEntity Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] PlayerEntity playerEntity)
        {
            playerEntity.SetKeys();

            return playerEntity;
        }
    }
}
