using System;
using HouseBudgetApi.Models.Amortization;

namespace HouseBudgetApi.Services.Interfaces
{
    public interface IAmortizationCalculator
    {
        AmortizationSchedule Calculate(decimal loanAmount, decimal interestRate, int termInMonths, DateTime startDate, decimal? monthlyPayment = null, ExtraPayment extraPayment = null);
    }
}
