using System;
using System.Collections.Generic;

namespace house_budget_api.Models.Amortization
{
    public class AmortizationSchedule
    {
        public decimal InterestPaid { get; set; }
        public decimal ExtraPrincipalPaid { get; set; }
        public decimal MonthlyPayment { get; set; }
        public DateTime MaturityDate { get; set; }
        public List<AmortizationPayment> Schedule { get; set; }
    }
}
