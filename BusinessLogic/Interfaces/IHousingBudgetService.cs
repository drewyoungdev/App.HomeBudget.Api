using System.Threading.Tasks;
using HouseBudgetApi.Models.Listings;

namespace HouseBudgetApi.BusinessLogic.Interfaces
{
    public interface IHousingBudgetService
    {
        Task<HousingBudget> Run();
    }
}
