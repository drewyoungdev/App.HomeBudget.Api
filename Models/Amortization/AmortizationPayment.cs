using System;

namespace HouseBudgetApi.Models.Amortization
{
    public class AmortizationPayment
    {
        public DateTime PaymentDate { get; set; }
        public decimal PaymentPrincipal { get; set; }
        public decimal PaymentInterest { get; set; }
        public decimal AdditionalPrincipal { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal InterestPaidToDate { get; set; }
        public decimal BalanceToDate { get; set; }
    }
}
