using System.Collections.Generic;
using System.Threading.Tasks;
using HouseBudgetApi.BusinessLogic.Interfaces;
using HouseBudgetApi.Models.Database;
using HouseBudgetApi.Repositories.Interfaces;

namespace HouseBudgetApi.BusinessLogic
{
    public class BudgetEvaluator : IBudgetEvaluator
    {
        private IBudgetPreferencesRepository budgetPreferencesRepository;

        public BudgetEvaluator(IBudgetPreferencesRepository budgetPreferencesRepository)
        {
            this.budgetPreferencesRepository = budgetPreferencesRepository;
        }

        public async Task<decimal> CalculateAvailableMonthlyBudget(string clientId)
        {
            var budgetPreferences = await budgetPreferencesRepository.Get(clientId);

            var availableMonthlyBudget = CalculateEstimatedMonthlyIncome(budgetPreferences.Income);

            foreach(var variableCost in budgetPreferences.Costs)
            {
                availableMonthlyBudget -= variableCost.Value;
            }

            return availableMonthlyBudget;
        }

        private decimal CalculateEstimatedMonthlyIncome(Income income)
        {
            var monthlyIncome = income.GrossAnnualSalary / 12;

            monthlyIncome -= income.EstimatedMonthlyTaxes;
            monthlyIncome -= income.EstimatedMonthlyDeductions;

            var monthly401KDeposit = (income.PercentageTo401K / 100) * income.GrossAnnualSalary / 12;

            monthlyIncome -= monthly401KDeposit;
            monthlyIncome += income.AdditionalMonthlyIncome;

            return monthlyIncome;
        }
    }
}
