using Microsoft.Azure.Cosmos.Table;

namespace ServerlessPersistence.Models
{
    public class PlayerEntity : TableEntity
    {
        public PlayerEntity()
        {}
        public PlayerEntity(
            string region,
            string id,
            string name,
            string email) 
            : base(partitionKey: region, rowKey: id)
        {
            Region = region;
            Id = id;
            Name = name;
            Email = email; 
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }

        public void SetKeys()
        {
            PartitionKey = Region;
            RowKey = Id;
        }
    }
}