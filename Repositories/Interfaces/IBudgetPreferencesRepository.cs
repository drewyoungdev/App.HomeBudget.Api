using System.Collections.Generic;
using System.Threading.Tasks;
using HouseBudgetApi.Models.Database;

namespace HouseBudgetApi.Repositories.Interfaces
{
    public interface IBudgetPreferencesRepository
    {
        Task<BudgetPreferences> Get(string clientId);
        Task Create(BudgetPreferences budgetPreferences);
        Task<bool> Update(BudgetPreferences budgetPreferences);
    }
}
