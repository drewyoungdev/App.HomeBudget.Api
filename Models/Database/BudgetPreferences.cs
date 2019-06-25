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
        public Income Income { get; set; } = new Income();
        public Dictionary<string, decimal> Costs { get; set; } = new Dictionary<string, decimal>();
    }

    public class Income
    {
        public decimal GrossAnnualSalary { get; set; }
        public decimal EstimatedMonthlyTaxes { get; set; }
        public decimal EstimatedMonthlyDeductions { get; set; }
        public decimal PercentageTo401K { get; set; }
    }
}
