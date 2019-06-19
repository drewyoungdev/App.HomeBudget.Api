using System.Collections.Generic;

namespace HouseBudgetApi.Models.Listings
{
    public class HousingBudget
    {
        public decimal AvailableMonthlyBudgetForMortgage { get; set; }
        public decimal BreakEvenLoanAmount { get; set; }
        public int LoanTermInMonths { get; set; }
        public decimal LoanRate { get; set; }
        public List<EvaluatedListing> EvaluatedListings { get; set; }
    }
}
