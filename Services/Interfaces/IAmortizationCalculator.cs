using System;
using house_budget_api.Models.Amortization;

namespace house_budget_api.Services.Interfaces
{
    public interface IAmortizationCalculator
    {
        AmortizationSchedule Calculate(decimal loanAmount, decimal interestRate, int termInMonths, DateTime startDate, decimal? monthlyPayment = null, ExtraPayment extraPayment = null);
    }
}
