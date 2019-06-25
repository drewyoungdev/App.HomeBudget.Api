using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace HouseBudgetApi.Models.Database
{
    public class BudgetPreferences
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId Id { get; set; }

        public string ClientId { get; set; }
        public Dictionary<string, decimal> Costs { get; set; }
    }
}
