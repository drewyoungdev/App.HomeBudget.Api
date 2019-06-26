using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace HouseBudgetApi.Models.Database
{
    public class LoanPreferences
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId Id { get; set; }

        public string ClientId { get; set; }
        public decimal Rate { get; set; }
        public int TermInMonths { get; set; }
        public decimal PercentageForDownPayment { get; set; }
    }
}
