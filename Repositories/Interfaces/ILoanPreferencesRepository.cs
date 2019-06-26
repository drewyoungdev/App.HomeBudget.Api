using System.Threading.Tasks;
using HouseBudgetApi.Models.Database;

namespace HouseBudgetApi.Repositories.Interfaces
{
    public interface ILoanPreferencesRepository
    {
        Task<LoanPreferences> Get(string clientId);
        Task Create(LoanPreferences loanPreferences);
        Task<bool> Update(LoanPreferences loanPreferences);
    }
}
