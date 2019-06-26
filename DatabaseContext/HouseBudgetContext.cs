using HouseBudgetApi.Configs;
using HouseBudgetApi.DatabaseContext.Interfaces;
using HouseBudgetApi.Models.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HouseBudgetApi.DatabaseContext
{
    public class HouseBudgetContext : IHouseBudgetContext
    {
        private readonly IMongoDatabase _db;

        public HouseBudgetContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<BudgetPreferences> BudgetPreferences => _db.GetCollection<BudgetPreferences>("BudgetPreferences");
        public IMongoCollection<LoanPreferences> LoanPreferences => _db.GetCollection<LoanPreferences>("LoanPreferences");
    }
}
