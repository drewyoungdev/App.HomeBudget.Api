using System.Collections.Generic;
using HouseBudgetApi.BusinessLogic.Interfaces;

namespace HouseBudgetApi.BusinessLogic
{
    public class BudgetEvaluator : IBudgetEvaluator
    {
        // Pull from Budget Preferences DB
        private decimal grossAnnualSalary          = 70155.12m;
        private decimal estimatedMonthlyTaxes      = 1242.06m;
        private decimal estimatedMonthlyDeductions = 186m;
        private decimal percentageTo401K           = 15;
        private decimal additionalMonthlyIncome    = 500m;
        private Dictionary<string, decimal> variableCosts = new Dictionary<string, decimal>()
        {
            { "Vehicle Payment", 348 },
            { "Vehicle Insurance", 154 },
            { "Vehicle Fuel", 250 },
            { "Internet and Cable", 193 },
            { "Utilites", 200 },
            { "Groceries", 400 },
            { "Restaurants", 200 },
            { "Shopping", 440 },
            { "Amusement", 60 },
            { "Gym Membership", 36.92m },
            { "Haircut", 55 },
        };

        public decimal CalculateAvailableMonthlyBudget()
        {
            var availableMonthlyBudget = CalculateEstimatedMonthlyIncome();

            foreach(var variableCost in variableCosts)
            {
                availableMonthlyBudget -= variableCost.Value;
            }

            return availableMonthlyBudget;
        }

        private decimal CalculateEstimatedMonthlyIncome()
        {
            var monthlyIncome = grossAnnualSalary / 12;

            monthlyIncome -= estimatedMonthlyTaxes;
            monthlyIncome -= estimatedMonthlyDeductions;
            monthlyIncome -= (percentageTo401K / 100) * grossAnnualSalary / 12;
            monthlyIncome += additionalMonthlyIncome;

            return monthlyIncome;
        }
    }
}
