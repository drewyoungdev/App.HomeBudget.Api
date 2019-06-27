using System.Threading.Tasks;
using HouseBudgetApi.Models.Database;
using HouseBudgetApi.Models.Listings;

namespace HouseBudgetApi.BusinessLogic.Interfaces
{
    public interface IListingsEvaluator
    {
        EvaluatedListing Evaluate(decimal availableBudget, decimal percentageForDownPayment, int termInMonths, decimal rate, Listing listing);
    }
}
