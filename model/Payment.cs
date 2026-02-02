using System;

namespace LibraryManagementSystem.model
{
    /// <summary>
    /// Tracks student penalty payments and calculates financial metrics
    /// </summary>
    public class Payment
    {
        public string StudentName { get; set; }
        public decimal TotalPenalty { get; set; }
        public decimal AmountPaid { get; set; }

        /// <summary>
        /// Calculates unpaid balance
        /// Formula: UnpaidBalance = TotalPenalty - AmountPaid
        /// </summary>
        public decimal UnpaidBalance
        {
            get
            {
                decimal balance = TotalPenalty - AmountPaid;
                return balance < 0 ? 0 : balance;
            }
        }

        /// <summary>
        /// Calculates paid percentage for progress bar
        /// Critical: If TotalPenalty == 0, returns 0 (not 100%)
        /// Formula: PaidPercentage = (AmountPaid / TotalPenalty) × 100
        /// </summary>
        public int PaidPercentage
        {
            get
            {
                // Zero-penalty case: both bars should be empty
                if (TotalPenalty == 0)
                    return 0;

                decimal percentage = (AmountPaid / TotalPenalty) * 100;
                // Clamp between 0 and 100
                return Math.Max(0, Math.Min(100, (int)Math.Round(percentage)));
            }
        }

        /// <summary>
        /// Calculates unpaid percentage for progress bar
        /// Critical: If TotalPenalty == 0, returns 0 (prevents "no data = fully unpaid" bug)
        /// Formula: UnpaidPercentage = 100 - PaidPercentage (when TotalPenalty > 0)
        /// </summary>
        public int UnpaidPercentage
        {
            get
            {
                // Zero-penalty case: both bars should be empty
                if (TotalPenalty == 0)
                    return 0;

                return 100 - PaidPercentage;
            }
        }

        public Payment()
        {
            StudentName = "";
            TotalPenalty = 0;
            AmountPaid = 0;
        }

        public Payment(string studentName, decimal totalPenalty, decimal amountPaid)
        {
            StudentName = studentName;
            TotalPenalty = totalPenalty;
            AmountPaid = amountPaid;
        }
    }
}
