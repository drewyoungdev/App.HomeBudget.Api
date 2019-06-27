using System.Collections.Generic;
using System.Threading.Tasks;
using HouseBudgetApi.BusinessLogic.Interfaces;
using HouseBudgetApi.Models.Listings;
using System.Linq;
using System;
using HouseBudgetApi.Repositories.Interfaces;

namespace HouseBudgetApi.BusinessLogic
{
    public class HousingBudgetService : IHousingBudgetService
    {
        private IBudgetEvaluator budgetEvaluator;
        private ILoanPreferencesRepository loanPreferencesRepository;
        private ISavedListingsRepository savedListingsRepository;
        private IListingsEvaluator listingsEvaluator;

        public HousingBudgetService(IBudgetEvaluator budgetEvaluator,
                                    ILoanPreferencesRepository loanPreferencesRepository,
                                    ISavedListingsRepository savedListingsRepository,
                                    IListingsEvaluator listingsEvaluator)
        {
            this.budgetEvaluator = budgetEvaluator;
            this.loanPreferencesRepository = loanPreferencesRepository;
            this.savedListingsRepository = savedListingsRepository;
            this.listingsEvaluator = listingsEvaluator;
        }

        public async Task<HousingBudget> Run(string clientId)
        {
            var availableBudgetTask = budgetEvaluator.CalculateAvailableMonthlyBudget(clientId);
            var loanPreferencesTask = loanPreferencesRepository.Get(clientId);
            var listingsTask = savedListingsRepository.Get(clientId);

            var availableBudget = await availableBudgetTask;
            var loanPreferences = await loanPreferencesTask;
            var listings = await listingsTask;

            var percentageForDownPayment = loanPreferences.PercentageForDownPayment;
            var termInMonths = loanPreferences.TermInMonths;
            var rate = loanPreferences.Rate;

            var evaluatedListingsTasks = listings
                .Listings
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
