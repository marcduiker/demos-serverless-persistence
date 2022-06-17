using System;
using Azure;
using Azure.Data.Tables;

namespace ServerlessPersistence.Models
{
    public class PlayerEntity : ITableEntity
    {
        public PlayerEntity()
        {}
        public PlayerEntity(
            string region,
            string id,
            string name,
            string email) 
        {
            Region = region;
            Id = id;
            Name = name;
            Email = email;
            SetKeys();
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        
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