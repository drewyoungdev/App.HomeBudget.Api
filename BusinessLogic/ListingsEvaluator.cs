using System;
using HouseBudgetApi.BusinessLogic.Interfaces;
using HouseBudgetApi.Models.Listings;
using HouseBudgetApi.Services.Interfaces;

namespace HouseBudgetApi.BusinessLogic
{
    public class ListingsEvaluator : IListingsEvaluator
    {
        private decimal estimatedMonthlyTaxAmount = 340m;
        private decimal estimatedMonthlyInsuranceAmount = 100m;

        private IAmortizationCalculator amortizationCalculator;

        public ListingsEvaluator(IAmortizationCalculator amortizationCalculator)
        {
            this.amortizationCalculator = amortizationCalculator;
        }

        public EvaluatedListing Evaluate(decimal availableBudget, decimal percentageForDownPayment, int termInMonths, decimal rate, Listing listing)
        {
            var availableDownPayment = (percentageForDownPayment / 100) * listing.Price;
            var loanAmount           = listing.Price - availableDownPayment;

            var amortizationResults = amortizationCalculator
                .Calculate(loanAmount, rate/100, termInMonths, DateTime.Now);

            var evaluatedListing                             = new EvaluatedListing();
            evaluatedListing.Address                         = listing.Address;
            evaluatedListing.LoanAmount                      = Decimal.Round(loanAmount, 2);
            evaluatedListing.DownPayment                     = Decimal.Round(availableDownPayment, 2);
            evaluatedListing.MonthlyPI                       = Decimal.Round(amortizationResults.MonthlyPayment, 2);
            evaluatedListing.EstimatedMonthlyTaxes           = Decimal.Round(estimatedMonthlyTaxAmount, 2);
            evaluatedListing.EstimatedMonthlyInsurance       = Decimal.Round(estimatedMonthlyInsuranceAmount, 2);
            evaluatedListing.EstimatedMonthlyPITI            = Decimal.Round(evaluatedListing.MonthlyPI + evaluatedListing.EstimatedMonthlyTaxes + evaluatedListing.EstimatedMonthlyInsurance, 2);
            evaluatedListing.EstimatedRemainingMonthlyBudget = availableBudget - evaluatedListing.EstimatedMonthlyPITI;

            return evaluatedListing;
        }
    }
}
