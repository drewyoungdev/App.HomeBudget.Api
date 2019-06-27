using System.Threading.Tasks;
using HouseBudgetApi.DatabaseContext.Interfaces;
using HouseBudgetApi.Models.Database;
using HouseBudgetApi.Repositories.Interfaces;
using MongoDB.Driver;

namespace HouseBudgetApi.Repositories
{
    public class SavedListingsRepository : ISavedListingsRepository
    {
        private IHouseBudgetContext context;

        public SavedListingsRepository(IHouseBudgetContext context)
        {
            this.context = context;
        }

        public Task<SavedListings> Get(string clientId)
        {
            FilterDefinition<SavedListings> filter = Builders<SavedListings>.Filter.Eq(m => m.ClientId, clientId);
            return context
                    .SavedListings
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(SavedListings savedListings)
        {
            await context.SavedListings.InsertOneAsync(savedListings);
        }

        public async Task<bool> Update(SavedListings savedListings)
        {
            ReplaceOneResult updateResult =
                await context
                        .SavedListings
                        .ReplaceOneAsync(
                            filter: g => g.Id == savedListings.Id,
                            replacement: savedListings);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
