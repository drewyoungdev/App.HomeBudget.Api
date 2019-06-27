using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace HouseBudgetApi.Models.Database
{
    public class SavedListings
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId Id { get; set; }

        public string ClientId { get; set; }
        public List<Listing> Listings { get; set; } = new List<Listing>();
    }

    public class Listing
    {
        public string Address { get; set; }
        public decimal Price { get; set; }
    }
}
