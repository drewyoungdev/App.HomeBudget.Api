using System;
using System.Collections.Generic;
using System.Linq;
using HouseBudgetApi.Models.Amortization;
using HouseBudgetApi.Services.Interfaces;

namespace HouseBudgetApi.Services
{
    public class AmortizationCalculator : IAmortizationCalculator
    {
        private const int MonthsInYear = 12;
        private const decimal MinimumBalanceLeft = 0.01m;

        public AmortizationSchedule Calculate(decimal loanAmount, decimal interestRate, int termInMonths, DateTime startDate, decimal? monthlyPayment = null, ExtraPayment extraPayment = null)
        {
            if (termInMonths <= 0)
            {
               throw new Exception($"Loan term in months must be greater than zero.  Term received was {termInMonths}.");
            }

            var monthlyInterestRate = interestRate / MonthsInYear;

            // calculate estimated monthly payment if calling code does not send an amount or valid amount
            if (monthlyPayment <= 0m || monthlyPayment == null)
            {
                monthlyPayment = loanAmount * (decimal)((double)monthlyInterestRate / (1 - Math.Pow(1 + (double)monthlyInterestRate, -termInMonths)));
            }

            var remainingBalance = loanAmount;
            var interestPaidToDate = 0m;
            DateTime maturityDate = startDate.AddMonths(termInMonths);
            var currentServicingDate = startDate;
            var amortizationPayments = new List<AmortizationPayment>();

            var monthCount = 0; // month count will result in remaining balance > 0 by end of term if we account for missed payments
            while (monthCount < termInMonths || Math.Round(remainingBalance, 2) > MinimumBalanceLeft)
            {
                if (monthCount > 0) // added if to prevent adding of month if it is the first month
                {
                    currentServicingDate = currentServicingDate.AddMonths(1); // assuming monthly intervals for payments
                }

                monthCount++;

                // while loop will always continue to end of term so that we can calculate relative total interest if actual balance is completed before end of term
                if (Math.Round(remainingBalance, 2) <= MinimumBalanceLeft) // can this remaining balance <= 0 be checked against 0.00 e.g. Math.Round(remainingBalance, 2) <= 0 currently there are scenarios where the remaining balance may be 0.00000001
                {
                    continue;
                }

                // set base interest and principal for month
                var interestForMonth = remainingBalance * monthlyInterestRate;
                var principalForMonth = monthlyPayment.Value - interestForMonth;

                // checks if current term will contain last payment
                if (remainingBalance < monthlyPayment.Value)
                {
                    // updates principal for month since that will be the only value affected by the last payment
                    principalForMonth = (monthlyInterestRate + 1) * remainingBalance - interestForMonth; // above is equal to principalForMonth = remainingBalance when you simplify.
                }

                // get any additional or historical payments
                var extraPaymentForMonth = GetExtraPayment(extraPayment, currentServicingDate);

                // calculate any left over extra payment. currently just stored in local variable
                if (remainingBalance < principalForMonth + extraPaymentForMonth) // moved remaining balance to the front for readibility
                {
                    var leftoverExtraPayment = (principalForMonth + extraPaymentForMonth) - remainingBalance; // this equation works for both when principal < remaining balance and principal > remaining balance
                    extraPaymentForMonth -= leftoverExtraPayment;
                }

                remainingBalance -= principalForMonth + extraPaymentForMonth;
                interestPaidToDate += interestForMonth;

                // apply payments saved and maturity date current iteration is last payment
                if (Math.Round(remainingBalance, 2) <= MinimumBalanceLeft)
                {
                    // moved this setting of data out of previous if blocks because there are multiple ways to end a loan
                    // remaining balance < monthly payment or remaining balance < principal for month + extra payment for month (if an extra payment is made on the last term)
                    maturityDate = currentServicingDate;
                }

                // create amort payment and add to schedule (no rounding)
                amortizationPayments.Add(new AmortizationPayment()
                {
                    PaymentDate = currentServicingDate,
                    PaymentInterest = interestForMonth,
                    PaymentPrincipal = principalForMonth,
                    PaymentAmount = principalForMonth + interestForMonth,
                    AdditionalPrincipal = extraPaymentForMonth,
                    InterestPaidToDate = interestPaidToDate,
                    BalanceToDate = remainingBalance
                });
            }

            var amortizationSchedule = new AmortizationSchedule()
            {
                InterestPaid = interestPaidToDate,
                ExtraPrincipalPaid = amortizationPayments.Sum(x => x.AdditionalPrincipal),
                MonthlyPayment = monthlyPayment.Value,
                MaturityDate = maturityDate,
                Schedule = amortizationPayments
            };

            return amortizationSchedule;
        }

        private decimal GetExtraPayment(ExtraPayment extraPayment, DateTime currentServicingDate)
        {
            if (extraPayment != null // do we need to return 0 if extraPayment.DateApplied < historicPayments.Last().PaymentDate ?
                && extraPayment.IsExtraPaymentApplicable(currentServicingDate))
            {
                // check if extra payment amount is valid relative to current date. if yes, return value. if not, return 0.
                return extraPayment.Amount;
            }

            return 0;
        }
    }
}
