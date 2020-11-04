using Microsoft.Azure.Cosmos.Table;

namespace ServerlessPersistence.Models
{
    public class PlayerEntity : TableEntity
    {
        public PlayerEntity(
            string region,
            string id,
            string nickName,
            string email) 
            : base(partitionKey: region, rowKey: id)
        {
            Region = region;
            Id = id;
            NickName = nickName;
            Email = email; 
        }

        public string Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
    }
}