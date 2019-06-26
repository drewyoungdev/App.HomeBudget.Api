using System.Collections.Generic;
using System.Threading.Tasks;
using HouseBudgetApi.BusinessLogic.Interfaces;
using HouseBudgetApi.Models.Listings;
using System.Linq;
using System;

namespace HouseBudgetApi.BusinessLogic
{
    public class HousingBudgetService : IHousingBudgetService
    {
        // Pull from Loan Settings DB
        private int termInMonths = 360;
        private decimal rate = 4.5m;
        private decimal percentageForDownPayment = 20;

        // Pull from SavedListings DB
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

        public async Task<HousingBudget> Run(string clientId)
        {
            var availableBudget = await budgetEvaluator.CalculateAvailableMonthlyBudget(clientId);

            var evaluatedListingsTasks = listings
                .Select(listing => Task.Run(() => listingsEvaluator.Evaluate(availableBudget, percentageForDownPayment, termInMonths, rate, listing)))
                .ToList();

            var evaluatedListings = await Task.WhenAll(evaluatedListingsTasks);

            var housingBudget = new HousingBudget();
            housingBudget.AvailableMonthlyBudgetForMortgage = Decimal.Round(availableBudget, 2);
            housingBudget.LoanTermInMonths = termInMonths;
            housingBudget.LoanRate = rate;
            housingBudget.EvaluatedListings = evaluatedListings.ToList();

            return housingBudget;
        }
    }
}
