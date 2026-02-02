using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagementSystem.model;
using Newtonsoft.Json;

namespace LibraryManagementSystem.controller.payment
{
    /// <summary>
    /// Manages penalty payment tracking and financial calculations
    /// Provides single source of truth for payment data using payments.json
    /// </summary>
    public class PaymentController
    {
        private string paymentsFilePath;
        private List<Payment> payments;

        public PaymentController()
        {
            paymentsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "payments.json");
            LoadPayments();
        }

        /// <summary>
        /// Loads payment records from payments.json
        /// Creates default empty list if file doesn't exist
        /// </summary>
        private void LoadPayments()
        {
            try
            {
                if (File.Exists(paymentsFilePath))
                {
                    string json = File.ReadAllText(paymentsFilePath);
                    payments = JsonConvert.DeserializeObject<List<Payment>>(json) ?? new List<Payment>();
                }
                else
                {
                    payments = new List<Payment>();
                    EnsureDataDirectory();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading payments: {ex.Message}");
                payments = new List<Payment>();
            }
        }

        /// <summary>
        /// Saves payment records to payments.json
        /// </summary>
        private void SavePayments()
        {
            try
            {
                EnsureDataDirectory();
                string json = JsonConvert.SerializeObject(payments, Formatting.Indented);
                File.WriteAllText(paymentsFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving payments: {ex.Message}");
            }
        }

        /// <summary>
        /// Ensures Data directory exists
        /// </summary>
        private void EnsureDataDirectory()
        {
            string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
        }

        /// <summary>
        /// Gets payment record for a student, creates default if not found
        /// </summary>
        public Payment GetOrCreatePayment(string studentName)
        {
            var payment = payments.FirstOrDefault(p =>
                p.StudentName.Equals(studentName, StringComparison.OrdinalIgnoreCase));

            if (payment == null)
            {
                payment = new Payment(studentName, 0, 0);
                payments.Add(payment);
                SavePayments();
            }

            return payment;
        }

        /// <summary>
        /// Gets all payment records
        /// </summary>
        public List<Payment> GetAllPayments()
        {
            LoadPayments();
            return new List<Payment>(payments);
        }

        /// <summary>
        /// Gets payment record for a specific student
        /// </summary>
        public Payment GetStudentPayment(string studentName)
        {
            LoadPayments();
            return GetOrCreatePayment(studentName);
        }

        /// <summary>
        /// Updates total penalty for a student
        /// </summary>
        public void UpdateTotalPenalty(string studentName, decimal totalPenalty)
        {
            try
            {
                LoadPayments();
                var payment = GetOrCreatePayment(studentName);
                payment.TotalPenalty = totalPenalty < 0 ? 0 : totalPenalty;
                SavePayments();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating total penalty: {ex.Message}");
            }
        }

        /// <summary>
        /// Records a payment (supports partial or full payments)
        /// </summary>
        public void RecordPayment(string studentName, decimal amount)
        {
            try
            {
                LoadPayments();
                var payment = GetOrCreatePayment(studentName);
                payment.AmountPaid += amount;

                // Prevent overpayment
                if (payment.AmountPaid > payment.TotalPenalty)
                {
                    payment.AmountPaid = payment.TotalPenalty;
                }

                SavePayments();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error recording payment: {ex.Message}");
            }
        }

        /// <summary>
        /// Sets exact payment amount (replaces previous payment)
        /// </summary>
        public void SetPaymentAmount(string studentName, decimal amountPaid)
        {
            try
            {
                LoadPayments();
                var payment = GetOrCreatePayment(studentName);
                amountPaid = Math.Max(0, amountPaid);

                // Prevent overpayment
                if (amountPaid > payment.TotalPenalty)
                {
                    amountPaid = payment.TotalPenalty;
                }

                payment.AmountPaid = amountPaid;
                SavePayments();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting payment amount: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets total unpaid penalties across all students
        /// </summary>
        public decimal GetTotalUnpaidPenalties()
        {
            try
            {
                LoadPayments();
                return payments.Sum(p => p.UnpaidBalance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating total unpaid: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Gets collection progress percentage
        /// Calculation: (Total Paid / Total Penalty) × 100
        /// Returns 0 if no penalties exist
        /// </summary>
        public int GetCollectionProgress()
        {
            try
            {
                LoadPayments();
                decimal totalPenalty = payments.Sum(p => p.TotalPenalty);
                decimal totalPaid = payments.Sum(p => p.AmountPaid);

                if (totalPenalty == 0)
                    return 0;

                decimal percentage = (totalPaid / totalPenalty) * 100;
                return Math.Max(0, Math.Min(100, (int)Math.Round(percentage)));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating collection progress: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Clears all payment data (for reset/testing)
        /// </summary>
        public void ClearAllPayments()
        {
            try
            {
                payments.Clear();
                SavePayments();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing payments: {ex.Message}");
            }
        }
    }
}
