using System.Collections.Generic;
using System.Threading.Tasks;
using house_budget_api.BusinessLogic.Interfaces;
using house_budget_api.Models.Listings;
using System.Linq;
using System;

namespace house_budget_api.BusinessLogic
{
    public class HousingBudgetService : IHousingBudgetService
    {
        // Pull from Loan Settings DB
        private int termInMonths                 = 360;
        private decimal rate                     = 4.5m;
        private decimal percentageForDownPayment = 20;

        // Pull from Listings DB
        private List<Listing> listings = new List<Listing>()
        {
            new Listing() { Address = "131 S Dorchester Ave, Royal Oak, MI 48067", Price = 259000 },
            new Listing() { Address = "364 E Maplehurst St, Ferndale, MI 48220", Price = 289890 },
            new Listing() { Address = "1530 E Lincoln Ave, Royal Oak, MI 48067", Price = 250000 }
        };

        private IBudgetEvaluator budgetEvaluator;
        private IListingsEvaluator listingsEvaluator;

        public HousingBudgetService(IBudgetEvaluator budgetEvaluator, IListingsEvaluator listingsEvaluator)
        {
            this.budgetEvaluator = budgetEvaluator;
            this.listingsEvaluator = listingsEvaluator;
        }

        public async Task<HousingBudget> Run()
        {
            var availableBudget = budgetEvaluator.CalculateAvailableMonthlyBudget();

            var evaluatedListingsTasks = listings
                .Select(listing => Task.Run(() => listingsEvaluator.Evaluate(availableBudget, percentageForDownPayment, termInMonths, rate, listing)))
                .ToList();

            var evaluatedListings = await Task.WhenAll(evaluatedListingsTasks);

            var housingBudget                               = new HousingBudget();
            housingBudget.AvailableMonthlyBudgetForMortgage = Decimal.Round(availableBudget, 2);
            housingBudget.LoanTermInMonths                  = termInMonths;
            housingBudget.LoanRate                          = rate;
            housingBudget.EvaluatedListings                 = evaluatedListings.ToList();

            return housingBudget;
        }
    }
}
