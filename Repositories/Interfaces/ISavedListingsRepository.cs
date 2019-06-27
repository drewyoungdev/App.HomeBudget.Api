using System.Threading.Tasks;
using HouseBudgetApi.Models.Database;

namespace HouseBudgetApi.Repositories.Interfaces
{
    public interface ISavedListingsRepository
    {
        Task<SavedListings> Get(string clientId);
        Task Create(SavedListings savedListings);
        Task<bool> Update(SavedListings savedListings);
    }
}
