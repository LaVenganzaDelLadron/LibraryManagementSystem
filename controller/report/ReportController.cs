using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.controller;
using LibraryManagementSystem.controller.book;
using LibraryManagementSystem.enumerator;

namespace LibraryManagementSystem.controller.report
{
    internal class ReportController
    {
        /// <summary>
        /// Loads all report metrics (circulation rate, active borrowers, books, penalties)
        /// </summary>
        public class ReportMetrics
        {
            public double CirculationRate { get; set; }
            public int ActiveBorrowers { get; set; }
            public int TotalBooks { get; set; }
            public decimal TotalUnpaidPenalties { get; set; }
            public int CollectionProgress { get; set; }
        }

        /// <summary>
        /// Calculates circulation rate as percentage of monthly borrow transactions to total book copies
        /// Circulation Rate = (Borrow Transactions This Month ÷ Total Book Copies) × 100
        /// Resets at the start of each month
        /// Measures how actively the collection is being used in the current month
        /// Single source of truth using direct data access
        /// </summary>
        public double GetCirculationRateMetric()
        {
            try
            {
                BorrowController borrowController = new BorrowController();
                BookController bookController = new BookController();

                var borrows = borrowController.GetAllBorrows();
                var books = bookController.GetAllBooks();

                // Get current month and year
                DateTime now = DateTime.Now;
                int currentMonth = now.Month;
                int currentYear = now.Year;

                // Count borrow transactions from the current month only
                int monthlyBorrows = borrows.Count(b =>
                    b.RequestDate.Month == currentMonth &&
                    b.RequestDate.Year == currentYear
                );

                // Total available book copies in the library
                int totalBookCopies = books.Sum(b => b.Copies);

                if (totalBookCopies == 0)
                    return 0;

                double circulationRate = (double)monthlyBorrows / totalBookCopies * 100;
                return Math.Round(circulationRate, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating circulation rate: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Calculates active borrowers as count of distinct students with active borrows
        /// Active = students with Status == Borrowed or Overdue
        /// Single source of truth using direct data access
        /// </summary>
        public int GetActiveBorrowersMetric()
        {
            try
            {
                BorrowController borrowController = new BorrowController();
                var borrows = borrowController.GetAllBorrows();

                // Count distinct students who have active (Borrowed or Overdue) books
                int activeBorrowers = borrows
                    .Where(b => b.Status == BorrowStatus.Borrowed || b.Status == BorrowStatus.Overdue)
                    .Select(b => b.StudentName)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Count();

                return activeBorrowers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating active borrowers: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Gets all report metrics
        /// </summary>
        public ReportMetrics GetReportMetrics()
        {
            try
            {
                return new ReportMetrics
                {
                    CirculationRate = HomeContoller.GetCirculationRate(),
                    ActiveBorrowers = HomeContoller.GetActiveBorrowers(),
                    TotalBooks = HomeContoller.GetTotalBooks(),
                    TotalUnpaidPenalties = HomeContoller.GetTotalUnpaidPenalties(),
                    CollectionProgress = HomeContoller.GetPenaltyCollectionProgress()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading report metrics: {ex.Message}");
                return new ReportMetrics();
            }
        }

        /// <summary>
        /// Gets list of overdue books with penalty details
        /// </summary>
        public List<dynamic> GetOverdueBooksList()
        {
            try
            {
                return HomeContoller.GetOverdueBooks();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading overdue books: {ex.Message}");
                return new List<dynamic>();
            }
        }

        /// <summary>
        /// Receives a returned book and marks it as received
        /// </summary>
        public bool ReceiveReturnedBook(Guid borrowId)
        {
            try
            {
                return HomeContoller.ReceiveReturnedBook(borrowId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving returned book: {ex.Message}");
                return false;
            }
        }
    }
}
