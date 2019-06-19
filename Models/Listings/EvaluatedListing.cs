namespace HouseBudgetApi.Models.Listings
{
    public class EvaluatedListing
    {
        public string Address { get;set; }
        public decimal DownPayment { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal MonthlyPI { get; set; }
        public decimal EstimatedMonthlyTaxes { get; set; }
        public decimal EstimatedMonthlyInsurance { get; set; }
        public decimal EstimatedMonthlyPITI { get; set; }
        public decimal EstimatedRemainingMonthlyBudget { get; set; }
    }
}
