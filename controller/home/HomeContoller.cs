using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibraryManagementSystem.inheritance;


namespace LibraryManagementSystem.controller
{
    internal class HomeContoller : HomeInherit
    {
        private const int BorrowDurationDays = 1;
        private const decimal FinePerHour = 3m;

        // Static instance for backward compatibility
        private static HomeContoller _instance;
        public static HomeContoller Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HomeContoller();
                return _instance;
            }
        }

        // Data file paths are now centralized through DataPathHelper
        private static string GetStudentsPath() => LibraryManagementSystem.core.DataPathHelper.GetDataFilePath("students.json");
        private static string GetBooksPath() => LibraryManagementSystem.core.DataPathHelper.GetDataFilePath("books.json");
        private static string GetBorrowPath() => LibraryManagementSystem.core.DataPathHelper.GetDataFilePath("borrow.json");

        public override int GetTotalStudents()
        {
            try
            {
                string filePath = GetStudentsPath();
                if (!File.Exists(filePath))
                    return 0;

                var json = File.ReadAllText(filePath);
                var students = JsonConvert.DeserializeObject<List<Users>>(json);
                return students?.Count ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dashboard error: {ex.Message}");
                return 0;
            }
        }

        public override List<string> GetAvailableBookTitles()
        {
            try
            {
                string filePath = GetBooksPath();
                if (!File.Exists(filePath))
                    return new List<string>();

                var books = JsonConvert.DeserializeObject<List<Books>>(
                    File.ReadAllText(filePath)
                );

                return books.Where(b => b.Copies > 0).Select(b => b.Title).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book dashboard error: {ex.Message}");
                return new List<string>();
            }
        }

        public override int GetBorrowedCount()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                    return 0;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                return borrows?.Count(b => b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Borrowed) ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Borrowed count error: {ex.Message}");
                return 0;
            }
        }

        public override int GetPendingCount()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                    return 0;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                return borrows?.Count(b => b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Pending) ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Pending count error: {ex.Message}");
                return 0;
            }
        }

        public override int GetOverdueCount()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                    return 0;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                if (borrows == null)
                    return 0;

                return borrows.Count(b => IsOverdue(b));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Overdue count error: {ex.Message}");
                return 0;
            }
        }

        public static int GetInventoryUtilization()
        {
            try
            {
                string filePath = GetBooksPath();
                if (!File.Exists(filePath))
                    return 0;

                var books = JsonConvert.DeserializeObject<List<Books>>(
                    File.ReadAllText(filePath)
                );

                if (books == null || books.Count == 0)
                    return 0;

                int totalCopies = books.Sum(b => b.Copies);
                int borrowedCount = GetBorrowedCountStatic();

                if (totalCopies == 0)
                    return 0;

                return (int)((borrowedCount / (double)(totalCopies + borrowedCount)) * 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inventory utilization error: {ex.Message}");
                return 0;
            }
        }

        public static int GetReturnRate()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                    return 100;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                if (borrows == null || borrows.Count == 0)
                    return 100;

                int totalBorrows = borrows.Count;
                int returnedCount = borrows.Count(b => b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Returned);

                return (int)((returnedCount / (double)totalBorrows) * 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Return rate error: {ex.Message}");
                return 0;
            }
        }

        public static int GetBorrowRate()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                    return 0;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                if (borrows == null || borrows.Count == 0)
                    return 0;

                int totalBorrows = borrows.Count;
                int activeBorrows = borrows.Count(b => 
                    b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Pending ||
                    b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Borrowed ||
                    b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Overdue);

                return (int)((activeBorrows / (double)totalBorrows) * 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Borrow rate error: {ex.Message}");
                return 0;
            }
        }

        public static List<dynamic> GetRecentTransactions(int limit = 7)
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                var transactions = new List<dynamic>();

                if (File.Exists(borrowFilePath))
                {
                    var json = File.ReadAllText(borrowFilePath);
                    var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                    if (borrows != null)
                    {
                        // Get most recent transactions and limit to specified count
                        var recent = borrows.OrderByDescending(b => b.RequestDate).Take(limit).ToList();

                        foreach (var borrow in recent)
                        {
                            transactions.Add(new
                            {
                                Student = borrow.StudentName ?? "Unknown",
                                Book = borrow.BookRequested ?? "Unknown",
                                Date = borrow.RequestDate.ToString("yyyy-MM-dd HH:mm"),
                                Status = borrow.Status.ToString()
                            });
                        }
                    }
                }

                return transactions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Recent transactions error: {ex.Message}");
                return new List<dynamic>();
            }
        }

        public override decimal GetCirculationRate()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                    return 0m;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                if (borrows == null || borrows.Count == 0)
                    return 0;

                string booksPath = GetBooksPath();
                if (!File.Exists(booksPath))
                    return 0;

                var books = JsonConvert.DeserializeObject<List<Books>>(
                    File.ReadAllText(booksPath)
                );

                if (books == null || books.Count == 0)
                    return 0;

                // Circulation rate = Borrowed books / Total books * 100
                int borrowedCount = borrows.Count(b => b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Borrowed);
                int totalBooks = books.Sum(b => b.Copies);

                if (totalBooks == 0)
                    return 0m;

                return (decimal)Math.Round((borrowedCount / (double)totalBooks) * 100, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Circulation rate error: {ex.Message}");
                return 0m;
            }
        }
        // Static wrappers for backward compatibility
        public static int GetTotalBooksStatic() => Instance.GetAvailableBookTitles().Count;
        public static int GetBorrowedCountStatic() => Instance.GetBorrowedCount();
        public static decimal GetCirculationRateStatic() => Instance.GetCirculationRate();
        public static decimal GetTotalUnpaidPenaltiesStatic() => Instance.GetTotalUnpaidPenalties();
        public static List<dynamic> GetOverdueBooksStatic() => Instance.GetOverdueBooks();
        public static int GetTotalStudentsStatic() => Instance.GetTotalStudents();
        public static int GetAvailableBookTitlesStatic() => Instance.GetAvailableBookTitles().Count;
        public static int GetPendingCountStatic() => Instance.GetPendingCount();
        public static int GetOverdueCountStatic() => Instance.GetOverdueCount();
        public static int GetActiveBorrowers()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                    return 0;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                if (borrows == null)
                    return 0;

                // Count unique students with active borrowing status
                var activeBorrowers = borrows
                    .Where(b => b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Borrowed ||
                                b.Status == LibraryManagementSystem.enumerator.BorrowStatus.Pending)
                    .Select(b => b.StudentName)
                    .Distinct()
                    .Count();

                return activeBorrowers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Active borrowers error: {ex.Message}");
                return 0;
            }
        }

        public static int GetTotalBooks()
        {
            try
            {
                string filePath = GetBooksPath();
                if (!File.Exists(filePath))
                    return 0;

                var books = JsonConvert.DeserializeObject<List<Books>>(
                    File.ReadAllText(filePath)
                );

                return books?.Count ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Total books error: {ex.Message}");
                return 0;
            }
        }

        public override decimal GetTotalUnpaidPenalties()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                    return 0m;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                if (borrows == null)
                    return 0;

                decimal totalPenalty = 0;
                var overdueBooks = borrows.Where(b => IsOverdue(b) && !b.FinePaid).ToList();

                foreach (var book in overdueBooks)
                {
                    DateTime dueDate = book.RequestDate.AddDays(BorrowDurationDays);
                    totalPenalty += CalculateFine(dueDate);
                }

                return totalPenalty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Total penalties error: {ex.Message}");
                return 0;
            }
        }

        public override List<dynamic> GetOverdueBooks()
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                var overdueList = new List<dynamic>();

                if (File.Exists(borrowFilePath))
                {
                    var json = File.ReadAllText(borrowFilePath);
                    var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json);

                    if (borrows != null)
                    {
                        var overdueBooks = borrows.Where(b => IsOverdue(b) || IsReturnAwaitingReceive(b)).ToList();

                        foreach (var borrow in overdueBooks)
                        {
                            DateTime dueDate = borrow.RequestDate.AddDays(BorrowDurationDays);
                            TimeSpan overdueSpan = DateTime.Now - dueDate;
                            int hoursOverdue = (int)Math.Ceiling(overdueSpan.TotalHours);
                            if (hoursOverdue < 0) hoursOverdue = 0;
                            decimal penalty = CalculateFine(dueDate);

                            bool awaitingReceive = IsReturnAwaitingReceive(borrow);
                            bool hasPenalty = penalty > 0;
                            string paymentStatus = hasPenalty
                                ? (borrow.FinePaid ? "Paid" : "Unpaid")
                                : string.Empty;

                            string actionText = awaitingReceive ? "Receive" : string.Empty;

                            overdueList.Add(new
                            {
                                Id = borrow.Id,
                                StudentName = borrow.StudentName,
                                BookTitle = borrow.BookRequested,
                                StudentBook = $"{borrow.StudentName} - {borrow.BookRequested}",
                                DueDateDelay = $"{dueDate:yyyy-MM-dd HH:mm} ({hoursOverdue} hours)",
                                PenaltyBreakdown = hasPenalty
                                    ? $"{hoursOverdue} hours × ₱{FinePerHour} = ₱{penalty}"
                                    : string.Empty,
                                Payment = paymentStatus,
                                ActionText = actionText,
                                AwaitingReceive = awaitingReceive,
                                DaysOverdue = (int)Math.Floor(overdueSpan.TotalDays),
                                PenaltyAmount = penalty
                            });
                        }
                    }
                }

                return overdueList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Overdue books error: {ex.Message}");
                return new List<dynamic>();
            }
        }

        public static int GetPenaltyCollectionProgress()
        {
            try
            {
                decimal totalPenalties = GetTotalUnpaidPenaltiesStatic();
                // Assuming some amount collected (for demo, calculate as partial)
                if (totalPenalties == 0)
                    return 0;
                // Assume 40% collection rate
                return 40;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Penalty collection progress error: {ex.Message}");
                return 0;
            }
        }

        private static bool IsOverdue(Borrow borrow)
        {
            if (borrow == null)
                return false;

            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Returned)
                return false;

            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Pending)
                return false;

            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Returning)
                return false;

            DateTime dueDate = borrow.RequestDate.AddDays(BorrowDurationDays);
            return DateTime.Now > dueDate;
        }

        private static bool IsReturnAwaitingReceive(Borrow borrow)
        {
            if (borrow == null)
                return false;

            return (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Returning
                    || borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Returned)
                && !borrow.ReceivedByLibrarian;
        }

        public static bool ReceiveReturnedBook(Guid borrowId)
        {
            try
            {
                string borrowFilePath = GetBorrowPath();
                if (!File.Exists(borrowFilePath))
                {
                    return false;
                }

                var json = File.ReadAllText(borrowFilePath);
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(json) ?? new List<Borrow>();

                var borrow = borrows.FirstOrDefault(b => b.Id == borrowId);
                if (borrow == null)
                {
                    return false;
                }

                borrow.ReceivedByLibrarian = true;

                // Move the returned book to returned.json
                var returnedController = new LibraryManagementSystem.controller.report.ReturnedController();
                bool moved = returnedController.MoveReturnedBook(borrow);
                
                if (!moved)
                {
                    Console.WriteLine($"Warning: Could not move returned book to returned.json");
                    return false;
                }

                // Remove from active borrow list
                borrows.Remove(borrow);
                File.WriteAllText(borrowFilePath, JsonConvert.SerializeObject(borrows, Formatting.Indented));

                // Restore book copies when received by librarian
                var bookController = new LibraryManagementSystem.controller.book.BookController();
                bool copiesRestored = bookController.UpdateBookCopies(borrow.BookRequested, 1);
                
                if (!copiesRestored)
                {
                    Console.WriteLine($"Warning: Could not restore copies for book '{borrow.BookRequested}'");
                }

                var notificationController = new LibraryManagementSystem.controller.student.StudentNotificationController();
                string message = $"Your book '{borrow.BookRequested}' has been received by the librarian. Thank you!";
                notificationController.AddNotification(borrow.StudentName, "Returned", message);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Receive returned book error: {ex.Message}");
                return false;
            }
        }

        private static decimal CalculateFine(DateTime dueDate)
        {
            TimeSpan overdue = DateTime.Now - dueDate;
            if (overdue.TotalHours <= 0)
                return 0;

            decimal hours = (decimal)Math.Ceiling(overdue.TotalHours);
            return hours * FinePerHour;
        }

    }
}
