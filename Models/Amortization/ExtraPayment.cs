using System;

namespace house_budget_api.Models.Amortization
{
    public class ExtraPayment
    {
        public decimal Amount { get; set; }
        public ExtraPaymentType PaymentType { get; set; }
        public DateTime DateApplied { get; set; }

        public bool IsExtraPaymentApplicable(DateTime currentDate)
        {
            switch (PaymentType)
            {
                case ExtraPaymentType.OneTime:
                    if (currentDate.Month == DateApplied.Month && currentDate.Year == DateApplied.Year)
                    {
                        return true;
                    }
                    break;

                case ExtraPaymentType.Monthly:
                    if (currentDate.Year > DateApplied.Year || currentDate.Year == DateApplied.Year && currentDate.Month >= DateApplied.Month)
                    {
                        return true;
                    }
                    break;

                case ExtraPaymentType.Annual:
                    if (currentDate.Month == DateApplied.Month && currentDate.Year >= DateApplied.Year)
                    {
                        return true;
                    }
                    break;

                default:
                    return false;
            }

            return false;
        }
    }
}
