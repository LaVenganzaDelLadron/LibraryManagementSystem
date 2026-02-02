using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagementSystem.model;
using LibraryManagementSystem.enumerator;
using LibraryManagementSystem.core;
using Newtonsoft.Json;

namespace LibraryManagementSystem.controller.monitoring
{
    internal class MonitoringController
    {
        private const int BorrowDurationDays = 1;
        private const decimal FinePerHour = 3m;
        private List<Borrow> borrowList = new List<Borrow>();
        private FileSystemWatcher fileWatcher;

        public event Action OnDataChanged;

        public MonitoringController()
        {
            InitializeFileWatcher();
        }

        private void InitializeFileWatcher()
        {
            try
            {
                string borrowJsonPath = LibraryManagementSystem.core.DataPathHelper.GetDataFilePath("borrow.json");
                string directoryPath = Path.GetDirectoryName(borrowJsonPath);

                fileWatcher = new FileSystemWatcher
                {
                    Path = directoryPath,
                    Filter = "borrow.json",
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
                };

                fileWatcher.Changed += OnBorrowFileChanged;
                fileWatcher.EnableRaisingEvents = true;
            }
            catch
            {
                // Ignore file watcher errors
            }
        }

        private void OnBorrowFileChanged(object sender, FileSystemEventArgs e)
        {
            LoadBorrowData();
            OnDataChanged?.Invoke();
        }

        public void LoadBorrowData()
        {
            try
            {
                string borrowPath = LibraryManagementSystem.core.DataPathHelper.GetDataFilePath("borrow.json");
                if (!File.Exists(borrowPath))
                {
                    borrowList = new List<Borrow>();
                    return;
                }

                var data = JsonConvert.DeserializeObject<List<Borrow>>(File.ReadAllText(borrowPath)) ?? new List<Borrow>();
                borrowList = data;
            }
            catch
            {
                borrowList = new List<Borrow>();
            }
        }

        public List<MonitoringRow> GetMonitoringRows(string filter = "All")
        {
            var rows = new List<MonitoringRow>();

            // Filter based on COMPUTED display status, not raw enum
            IEnumerable<Borrow> filtered = borrowList;
            if (!string.Equals(filter, "All", StringComparison.OrdinalIgnoreCase))
            {
                filtered = filtered.Where(b =>
                {
                    DateTime dueDate = b.RequestDate.AddDays(BorrowDurationDays);
                    string displayStatus = GetDisplayStatus(b, dueDate);
                    return displayStatus.Equals(filter, StringComparison.OrdinalIgnoreCase);
                });
            }

            foreach (var borrow in filtered)
            {
                DateTime dueDate = borrow.RequestDate.AddDays(BorrowDurationDays);
                string statusDisplay = GetDisplayStatus(borrow, dueDate);
                string countdown = GetCountdownText(statusDisplay, dueDate);
                string fine = GetFineText(statusDisplay, dueDate);

                rows.Add(new MonitoringRow
                {
                    StudentName = borrow.StudentName,
                    BookRequested = borrow.BookRequested,
                    Status = statusDisplay,
                    Countdown = countdown,
                    Fine = fine,
                    DueDate = dueDate,
                    BorrowStatus = borrow.Status
                });
            }

            return rows;
        }

        public void UpdateCountdowns(List<MonitoringRow> rows)
        {
            foreach (var row in rows)
            {
                // Recompute display status from the original enum state + current time
                // Never mutate row.Status or row.BorrowStatus - they should remain constant from GetMonitoringRows()
                DateTime dueDate = row.DueDate;
                
                // For display purposes, calculate what the status should be RIGHT NOW based on enum
                string currentDisplayStatus = GetDisplayStatus(new Borrow 
                { 
                    Status = row.BorrowStatus,
                    RequestDate = dueDate.AddDays(-BorrowDurationDays)
                }, dueDate);
                
                // Update countdown and fine based on current time
                row.Countdown = GetCountdownText(currentDisplayStatus, dueDate);
                row.Fine = GetFineText(currentDisplayStatus, dueDate);
                
                // Note: We do NOT update row.Status here - it's for display only and was set correctly in GetMonitoringRows()
            }
        }

        private string GetDisplayStatus(Borrow borrow, DateTime dueDate)
        {
            if (borrow == null || borrow.Status == null)
                return "Unknown";

            // For these statuses, NEVER change them - they are terminal states
            if (borrow.Status == BorrowStatus.Returned)
                return "Returned";

            // Returning status shows as "Returned" in monitoring (book is on its way back)
            if (borrow.Status == BorrowStatus.Returning)
                return "Returned";

            if (borrow.Status == BorrowStatus.Pending)
                return "Pending";

            // Lost status (if it exists in enum) is also terminal
            if (borrow.Status.ToString().Equals("Lost", StringComparison.OrdinalIgnoreCase))
                return "Lost";

            // For Borrowed: check if it's now overdue
            if (borrow.Status == BorrowStatus.Borrowed)
            {
                return DateTime.Now > dueDate ? "Overdue" : "Borrowed";
            }

            // For Overdue enum: always Overdue
            if (borrow.Status == BorrowStatus.Overdue)
                return "Overdue";

            // Fallback
            return borrow.Status.ToString();
        }

        private string GetCountdownText(string statusDisplay, DateTime dueDate)
        {
            if (statusDisplay.Equals("Returned", StringComparison.OrdinalIgnoreCase))
                return "-"; // Stop the countdown when returned

            if (statusDisplay.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                return "Awaiting approval";

            return FormatCountdown(dueDate);
        }

        private string FormatCountdown(DateTime dueDate)
        {
            TimeSpan diff = dueDate - DateTime.Now;
            if (diff.TotalSeconds >= 0)
            {
                return $"Due in {diff.Days}d {diff.Hours:D2}:{diff.Minutes:D2}:{diff.Seconds:D2}";
            }

            diff = DateTime.Now - dueDate;
            return $"Overdue by {diff.Days}d {diff.Hours:D2}:{diff.Minutes:D2}:{diff.Seconds:D2}";
        }

        private string GetFineText(string statusDisplay, DateTime dueDate)
        {
            if (!statusDisplay.Equals("Overdue", StringComparison.OrdinalIgnoreCase))
                return "₱ 0";

            TimeSpan overdue = DateTime.Now - dueDate;
            if (overdue.TotalHours <= 0)
                return "₱ 0";

            decimal hours = (decimal)Math.Ceiling(overdue.TotalHours);
            decimal fine = hours * FinePerHour;
            return $"₱ {fine:N0}";
        }

        public void Dispose()
        {
            fileWatcher?.Dispose();
        }
    }

    internal class MonitoringRow
    {
        public string StudentName { get; set; }
        public string BookRequested { get; set; }
        public string Status { get; set; }
        public string Countdown { get; set; }
        public string Fine { get; set; }
        public DateTime DueDate { get; set; }
        public BorrowStatus BorrowStatus { get; set; }
    }
}

