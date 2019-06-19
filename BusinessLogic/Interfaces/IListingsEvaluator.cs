using System.Threading.Tasks;
using house_budget_api.Models.Listings;

namespace house_budget_api.BusinessLogic.Interfaces
{
    public interface IListingsEvaluator
    {
        EvaluatedListing Evaluate(decimal availableBudget, decimal percentageForDownPayment, int termInMonths, decimal rate, Listing listing);
    }
}
