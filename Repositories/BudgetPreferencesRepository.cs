using System.Threading.Tasks;
using HouseBudgetApi.DatabaseContext.Interfaces;
using HouseBudgetApi.Models.Database;
using HouseBudgetApi.Repositories.Interfaces;
using MongoDB.Driver;

namespace HouseBudgetApi.Repositories
{
    public class BudgetPreferencesRepository : IBudgetPreferencesRepository
    {
        private IHouseBudgetContext context;

        public BudgetPreferencesRepository(IHouseBudgetContext context)
        {
            this.context = context;
        }

        public Task<BudgetPreferences> Get(string clientId)
        {
            FilterDefinition<BudgetPreferences> filter = Builders<BudgetPreferences>.Filter.Eq(m => m.ClientId, clientId);
            return context
                    .BudgetPreferences
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(BudgetPreferences budgetPreferences)
        {
            await context.BudgetPreferences.InsertOneAsync(budgetPreferences);
        }

        public async Task<bool> Update(BudgetPreferences budgetPreferences)
        {
            ReplaceOneResult updateResult =
                await context
                        .BudgetPreferences
                        .ReplaceOneAsync(
                            filter: g => g.Id == budgetPreferences.Id,
                            replacement: budgetPreferences);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
