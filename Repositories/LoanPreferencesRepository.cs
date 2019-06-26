using System.Threading.Tasks;
using HouseBudgetApi.DatabaseContext.Interfaces;
using HouseBudgetApi.Models.Database;
using HouseBudgetApi.Repositories.Interfaces;
using MongoDB.Driver;

namespace HouseBudgetApi.Repositories
{
    public class LoanPreferencesRepository : ILoanPreferencesRepository
    {
        private IHouseBudgetContext context;

        public LoanPreferencesRepository(IHouseBudgetContext context)
        {
            this.context = context;
        }

        public Task<LoanPreferences> Get(string clientId)
        {
            FilterDefinition<LoanPreferences> filter = Builders<LoanPreferences>.Filter.Eq(m => m.ClientId, clientId);
            return context
                    .LoanPreferences
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(LoanPreferences loanPreferences)
        {
            await context.LoanPreferences.InsertOneAsync(loanPreferences);
        }

        public async Task<bool> Update(LoanPreferences loanPreferences)
        {
            ReplaceOneResult updateResult =
                await context
                        .LoanPreferences
                        .ReplaceOneAsync(
                            filter: g => g.Id == loanPreferences.Id,
                            replacement: loanPreferences);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
