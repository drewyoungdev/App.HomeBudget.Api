using System.Threading.Tasks;
using house_budget_api.Models.Listings;

namespace house_budget_api.BusinessLogic.Interfaces
{
    public interface IHousingBudgetService
    {
        Task<HousingBudget> Run();
    }
}
