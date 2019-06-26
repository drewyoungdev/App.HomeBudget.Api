using System.Threading.Tasks;

namespace HouseBudgetApi.BusinessLogic.Interfaces
{
    public interface IBudgetEvaluator
    {
        Task<decimal> CalculateAvailableMonthlyBudget(string clientId);
    }
}
