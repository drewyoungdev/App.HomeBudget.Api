using HouseBudgetApi.Models.Database;
using MongoDB.Driver;

namespace HouseBudgetApi.DatabaseContext.Interfaces
{
    public interface IHouseBudgetContext
    {
        IMongoCollection<BudgetPreferences> BudgetPreferences { get; }
        IMongoCollection<LoanPreferences> LoanPreferences { get; }
        IMongoCollection<SavedListings> SavedListings { get; }
    }
}
