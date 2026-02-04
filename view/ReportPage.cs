using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagementSystem.controller;
using LibraryManagementSystem.controller.report;
using LibraryManagementSystem.controller.payment;
using LibraryManagementSystem.view.modal;
using LibraryManagementSystem.core;

namespace LibraryManagementSystem.view
{
    public partial class ReportPage : Form
    {
        private ReportController reportController;
        private PaymentController paymentController;
        private List<dynamic> overdueBooksList;
        
        private class ReturnRowInfo
        {
            public Guid BorrowId { get; set; }
            public string StudentName { get; set; }
            public string BookTitle { get; set; }
        }

        public ReportPage()
        {
            InitializeComponent();
            reportController = new ReportController();
            paymentController = new PaymentController();
            LoadReportData();
            LoadOverduePenaltyData();
            InitializeDataGridViewEvents();
        }

        private void InitializeDataGridViewEvents()
        {
            dgvOverduePenalty.CellMouseEnter += DgvOverduePenalty_CellMouseEnter;
            dgvOverduePenalty.CellMouseLeave += DgvOverduePenalty_CellMouseLeave;
            dgvOverduePenalty.CellFormatting += DgvOverduePenalty_CellFormatting;
            dgvOverduePenalty.CellClick += DgvOverduePenalty_CellClick;
        }

        private void DgvOverduePenalty_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvOverduePenalty.Columns["colActions"].Index)
            {
                dgvOverduePenalty.Cursor = Cursors.Hand;
            }
        }

        private void DgvOverduePenalty_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvOverduePenalty.Cursor = Cursors.Default;
        }

        private void DgvOverduePenalty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvOverduePenalty.Columns["colActions"].Index)
            {
                return;
            }

            var rowInfo = dgvOverduePenalty.Rows[e.RowIndex].Tag as ReturnRowInfo;
            if (rowInfo == null)
            {
                return;
            }

            string dueDateDelay = dgvOverduePenalty.Rows[e.RowIndex].Cells["colDueDateDelay"].Value?.ToString() ?? string.Empty;
            string penaltyBreakdown = dgvOverduePenalty.Rows[e.RowIndex].Cells["colPenaltyBreakdown"].Value?.ToString() ?? string.Empty;
            string payment = dgvOverduePenalty.Rows[e.RowIndex].Cells["colPayment"].Value?.ToString() ?? string.Empty;

            using (var details = new OverduePenaltyDetails(
                rowInfo.StudentName,
                rowInfo.BookTitle,
                dueDateDelay,
                penaltyBreakdown,
                payment))
            {
                details.ShowDialog(this);
            }
        }

        private void DgvOverduePenalty_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvOverduePenalty.Columns["colPayment"].Index && e.RowIndex >= 0)
            {
                string payment = e.Value?.ToString() ?? "";
                
                if (payment.IndexOf("Paid", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113); // Green
                }
                else if (payment.IndexOf("Unpaid", StringComparison.OrdinalIgnoreCase) >= 0 || payment.IndexOf("Pending", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60); // Red
                }
                else if (payment.IndexOf("No Penalty", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141); // Gray
                }
                else if (payment.IndexOf("Partial", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15); // Yellow/Orange
                }
            }
            else if (e.ColumnIndex == dgvOverduePenalty.Columns["colPenaltyBreakdown"].Index && e.RowIndex >= 0)
            {
                // Make penalty amounts stand out
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60); // Red for amounts
            }
        }

        private void LoadReportData()
        {
            try
            {
                var metrics = reportController.GetReportMetrics();
                
                double circulationRate = GetCirculationRate();
                int activeBorrowers = GetActiveBorrowers();
                int newAcquisitions = GetNewAcquisitions();
                
                lblCirculationRate.Text = circulationRate.ToString("0.0") + "%";
                lblBorrower.Text = activeBorrowers.ToString();
                lblNewAcquisition.Text = newAcquisitions.ToString();

                // Sync penalties from current overdue data into payments.json
                SyncPaymentPenaltiesFromOverdue();

                // Calculate totals directly from payments data
                var payments = paymentController.GetAllPayments();
                decimal totalPenalty = payments.Sum(p => p.TotalPenalty);
                decimal totalPaid = payments.Sum(p => p.AmountPaid);
                decimal totalUnpaidPenalties = payments.Sum(p => p.UnpaidBalance);

                lblTotalUnpaid.Text = "₱ " + totalUnpaidPenalties.ToString("N0");

                int paidProgress = 0;
                int unpaidProgress = 0;

                if (totalPenalty > 0)
                {
                    decimal percentage = (totalPaid / totalPenalty) * 100;
                    paidProgress = Math.Max(0, Math.Min(100, (int)Math.Round(percentage)));
                    unpaidProgress = 100 - paidProgress;
                }

                // Apply correct values to progress bars
                // Zero-penalty case keeps both bars empty
                progressBarPaid.Value = paidProgress;
                progressBarUnpaid.Value = unpaidProgress;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading report data: {ex.Message}");
            }
        }

        private void SyncPaymentPenaltiesFromOverdue()
        {
            try
            {
                var overdueBooks = reportController.GetOverdueBooksList();

                var penaltiesByStudent = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

                foreach (dynamic overdue in overdueBooks)
                {
                    if (overdue == null)
                        continue;

                    decimal penaltyAmount = 0;
                    try
                    {
                        penaltyAmount = (decimal)overdue.PenaltyAmount;
                    }
                    catch
                    {
                        penaltyAmount = 0;
                    }

                    if (penaltyAmount <= 0)
                        continue;

                    string studentName = overdue.StudentName as string ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(studentName))
                        continue;

                    if (!penaltiesByStudent.ContainsKey(studentName))
                        penaltiesByStudent[studentName] = 0;

                    penaltiesByStudent[studentName] += penaltyAmount;
                }

                // Update penalties for students with current overdue fines
                foreach (var entry in penaltiesByStudent)
                {
                    paymentController.UpdateTotalPenalty(entry.Key, entry.Value);
                }

                // Clear penalties for students who no longer have overdue fines
                var allPayments = paymentController.GetAllPayments();
                foreach (var payment in allPayments)
                {
                    if (!penaltiesByStudent.ContainsKey(payment.StudentName) && payment.TotalPenalty > 0)
                    {
                        paymentController.UpdateTotalPenalty(payment.StudentName, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error syncing payment penalties: {ex.Message}");
            }
        }

        private double GetCirculationRate()
        {
            try
            {
                return reportController.GetCirculationRateMetric();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating circulation rate: {ex.Message}");
                return 0;
            }
        }

        private int GetActiveBorrowers()
        {
            try
            {
                return reportController.GetActiveBorrowersMetric();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating active borrowers: {ex.Message}");
                return 0;
            }
        }

        private int GetNewAcquisitions()
        {
            try
            {
                return reportController.GetReportMetrics().TotalBooks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating new acquisitions: {ex.Message}");
                return 0;
            }
        }

        private void LoadOverduePenaltyData()
        {
            try
            {
                dgvOverduePenalty.Rows.Clear();
                overdueBooksList = reportController.GetOverdueBooksList();

                if (overdueBooksList.Count == 0)
                {
                    dgvOverduePenalty.Rows.Add("No overdue books", "-", "-", "-", "");
                    return;
                }

                foreach (var overdue in overdueBooksList)
                {
                    int rowIndex = dgvOverduePenalty.Rows.Add(
                        overdue.StudentBook,
                        overdue.DueDateDelay,
                        overdue.PenaltyBreakdown,
                        overdue.Payment,
                        overdue.ActionText
                    );

                    dgvOverduePenalty.Rows[rowIndex].Tag = new ReturnRowInfo
                    {
                        BorrowId = overdue.Id,
                        StudentName = overdue.StudentName,
                        BookTitle = overdue.BookTitle
                    };

                    if (string.IsNullOrWhiteSpace(overdue.ActionText))
                    {
                        dgvOverduePenalty.Rows[rowIndex].Cells["colActions"].ReadOnly = true;
                        dgvOverduePenalty.Rows[rowIndex].Cells["colActions"].Value = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading overdue data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOverduePenalty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvOverduePenalty.Columns["colActions"].Index)
            {
                var rowInfo = dgvOverduePenalty.Rows[e.RowIndex].Tag as ReturnRowInfo;
                if (rowInfo == null)
                {
                    return;
                }

                string actionText = dgvOverduePenalty.Rows[e.RowIndex].Cells["colActions"].Value?.ToString();
                if (!string.Equals(actionText, "Receive", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                var result = MessageBox.Show(
                    $"Receive returned book '{rowInfo.BookTitle}' from {rowInfo.StudentName}?",
                    "Receive Return",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    bool received = reportController.ReceiveReturnedBook(rowInfo.BorrowId);
                    if (!received)
                    {
                        MessageBox.Show("Failed to receive the return. Please try again.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Return received successfully. Notification sent to the student.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadOverduePenaltyData();
                    LoadReportData();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Create export folder in Downloads
                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string exportFolder = Path.Combine(downloadsPath, "LibraryExports");
                if (!Directory.Exists(exportFolder))
                {
                    Directory.CreateDirectory(exportFolder);
                }

                // Generate filename with current date
                string dateStamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");

                // Export all data
                ExportBorrowedBooks(exportFolder, dateStamp);
                ExportPayments(exportFolder, dateStamp);
                ExportRejected(exportFolder, dateStamp);
                ExportReturned(exportFolder, dateStamp);

                MessageBox.Show($"Data exported successfully to:\n{exportFolder}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the export folder
                System.Diagnostics.Process.Start("explorer.exe", exportFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportBorrowedBooks(string folder, string dateStamp)
        {
            try
            {
                string borrowFilePath = DataPathHelper.GetDataFilePath("borrow.json");
                if (!File.Exists(borrowFilePath))
                    return;

                var json = File.ReadAllText(borrowFilePath);
                var borrows = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(json) ?? new List<dynamic>();

                string csvPath = Path.Combine(folder, $"BorrowedBooks_{dateStamp}.csv");
                using (StreamWriter sw = new StreamWriter(csvPath, false, Encoding.UTF8))
                {
                    // Header
                    sw.WriteLine("ID,Student Name,Book Requested,Request Date,Return Date,Status,Fine Paid,Received By Librarian");

                    // Data rows
                    foreach (var borrow in borrows)
                    {
                        sw.WriteLine($"{EscapeCsv(borrow.Id?.ToString())}," +
                                   $"{EscapeCsv(borrow.StudentName?.ToString())}," +
                                   $"{EscapeCsv(borrow.BookRequested?.ToString())}," +
                                   $"{EscapeCsv(borrow.RequestDate?.ToString())}," +
                                   $"{EscapeCsv(borrow.ReturnDate?.ToString())}," +
                                   $"{EscapeCsv(borrow.Status?.ToString())}," +
                                   $"{EscapeCsv(borrow.FinePaid?.ToString())}," +
                                   $"{EscapeCsv(borrow.ReceivedByLibrarian?.ToString())}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exporting borrowed books: {ex.Message}");
            }
        }

        private void ExportPayments(string folder, string dateStamp)
        {
            try
            {
                string paymentFilePath = DataPathHelper.GetDataFilePath("payments.json");
                if (!File.Exists(paymentFilePath))
                    return;

                var json = File.ReadAllText(paymentFilePath);
                var payments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(json) ?? new List<dynamic>();

                string csvPath = Path.Combine(folder, $"Payments_{dateStamp}.csv");
                using (StreamWriter sw = new StreamWriter(csvPath, false, Encoding.UTF8))
                {
                    // Header
                    sw.WriteLine("Student Name,Total Penalty,Amount Paid,Unpaid Balance,Paid Percentage,Unpaid Percentage");

                    // Data rows
                    foreach (var payment in payments)
                    {
                        sw.WriteLine($"{EscapeCsv(payment.StudentName?.ToString())}," +
                                   $"{EscapeCsv(payment.TotalPenalty?.ToString())}," +
                                   $"{EscapeCsv(payment.AmountPaid?.ToString())}," +
                                   $"{EscapeCsv(payment.UnpaidBalance?.ToString())}," +
                                   $"{EscapeCsv(payment.PaidPercentage?.ToString())}," +
                                   $"{EscapeCsv(payment.UnpaidPercentage?.ToString())}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exporting payments: {ex.Message}");
            }
        }

        private void ExportRejected(string folder, string dateStamp)
        {
            try
            {
                string rejectFilePath = DataPathHelper.GetDataFilePath("reject.json");
                if (!File.Exists(rejectFilePath))
                    return;

                var json = File.ReadAllText(rejectFilePath);
                var rejects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(json) ?? new List<dynamic>();

                string csvPath = Path.Combine(folder, $"Rejected_{dateStamp}.csv");
                using (StreamWriter sw = new StreamWriter(csvPath, false, Encoding.UTF8))
                {
                    // Header
                    sw.WriteLine("ID,Student Name,Book Requested,Request Date,Rejection Date,Reason");

                    // Data rows
                    foreach (var reject in rejects)
                    {
                        sw.WriteLine($"{EscapeCsv(reject.Id?.ToString())}," +
                                   $"{EscapeCsv(reject.StudentName?.ToString())}," +
                                   $"{EscapeCsv(reject.BookRequested?.ToString())}," +
                                   $"{EscapeCsv(reject.RequestDate?.ToString())}," +
                                   $"{EscapeCsv(reject.RejectionDate?.ToString())}," +
                                   $"{EscapeCsv(reject.Reason?.ToString())}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exporting rejected: {ex.Message}");
            }
        }

        private void ExportReturned(string folder, string dateStamp)
        {
            try
            {
                string returnedFilePath = DataPathHelper.GetDataFilePath("returned.json");
                if (!File.Exists(returnedFilePath))
                    return;

                var json = File.ReadAllText(returnedFilePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    // Create empty CSV with headers
                    string csvPath = Path.Combine(folder, $"Returned_{dateStamp}.csv");
                    using (StreamWriter sw = new StreamWriter(csvPath, false, Encoding.UTF8))
                    {
                        sw.WriteLine("ID,Student Name,Book Requested,Request Date,Return Date,Status,Fine Paid,Received By Librarian");
                    }
                    return;
                }

                var returned = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(json) ?? new List<dynamic>();

                string csvPath2 = Path.Combine(folder, $"Returned_{dateStamp}.csv");
                using (StreamWriter sw = new StreamWriter(csvPath2, false, Encoding.UTF8))
                {
                    // Header
                    sw.WriteLine("ID,Student Name,Book Requested,Request Date,Return Date,Status,Fine Paid,Received By Librarian");

                    // Data rows
                    foreach (var ret in returned)
                    {
                        sw.WriteLine($"{EscapeCsv(ret.Id?.ToString())}," +
                                   $"{EscapeCsv(ret.StudentName?.ToString())}," +
                                   $"{EscapeCsv(ret.BookRequested?.ToString())}," +
                                   $"{EscapeCsv(ret.RequestDate?.ToString())}," +
                                   $"{EscapeCsv(ret.ReturnDate?.ToString())}," +
                                   $"{EscapeCsv(ret.Status?.ToString())}," +
                                   $"{EscapeCsv(ret.FinePaid?.ToString())}," +
                                   $"{EscapeCsv(ret.ReceivedByLibrarian?.ToString())}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exporting returned: {ex.Message}");
            }
        }

        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            // Escape quotes and wrap in quotes if contains comma, quote, or newline
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            return value;
        }
    }
}
