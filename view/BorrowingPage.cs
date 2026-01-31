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
using LibraryManagementSystem.controller.book;
using LibraryManagementSystem.model;

namespace LibraryManagementSystem.view
{
    public partial class BorrowingPage : Form
    {
        private BorrowController borrowController;
        private Guid selectedBorrowId;
        private FileSystemWatcher fileWatcher;
        private Timer refreshTimer;
        private string currentFilter = "All";

        public BorrowingPage()
        {
            InitializeComponent();
            borrowController = new BorrowController();
            InitializeFileWatcher();
            InitializeRefreshTimer();
            InitializeDataGridViewEvents();
            LoadBorrowingRequests();
        }

        private void InitializeDataGridViewEvents()
        {
            dgvBorrowingRequests.CellMouseEnter += DgvBorrowingRequests_CellMouseEnter;
            dgvBorrowingRequests.CellMouseLeave += DgvBorrowingRequests_CellMouseLeave;
            dgvBorrowingRequests.CellFormatting += DgvBorrowingRequests_CellFormatting;
        }

        private void DgvBorrowingRequests_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4) // Actions column
            {
                dgvBorrowingRequests.Cursor = Cursors.Hand;
            }
        }

        private void DgvBorrowingRequests_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvBorrowingRequests.Cursor = Cursors.Default;
        }

        private void DgvBorrowingRequests_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0) // Status column
            {
                string status = e.Value?.ToString() ?? "";
                
                if (status.IndexOf("Pending", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 140, 0); // Orange
                }
                else if (status.IndexOf("Borrowed", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(155, 89, 182); // Purple
                }
                else if (status.IndexOf("Returned", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113); // Green
                }
                else if (status.IndexOf("Overdue", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60); // Red
                }
                else if (status.IndexOf("Rejected", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166); // Gray
                }
            }
        }

        private void InitializeFileWatcher()
        {
            try
            {
                string borrowJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "borrow.json");
                string directoryPath = Path.GetDirectoryName(borrowJsonPath);

                fileWatcher = new FileSystemWatcher();
                fileWatcher.Path = directoryPath;
                fileWatcher.Filter = "borrow.json";
                fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
                fileWatcher.Changed += OnBorrowFileChanged;
                fileWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing file watcher: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeRefreshTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 2000; // Refresh every 2 seconds
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (currentFilter == "All")
            {
                LoadBorrowingRequests();
            }
            else if (currentFilter == "Rejected")
            {
                LoadRejectedRequests();
            }
            else
            {
                LoadBorrowingRequestsByStatus(currentFilter);
            }
        }

        private void OnBorrowFileChanged(object sender, FileSystemEventArgs e)
        {
            // File watcher runs on a different thread, so we need to invoke on the UI thread
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => RefreshBorrowingData()));
            }
            else
            {
                RefreshBorrowingData();
            }
        }

        private void RefreshBorrowingData()
        {
            // Add a small delay to ensure file is fully written
            System.Threading.Thread.Sleep(100);
            
            if (currentFilter == "All")
            {
                LoadBorrowingRequests();
            }
            else if (currentFilter == "Rejected")
            {
                LoadRejectedRequests();
            }
            else
            {
                LoadBorrowingRequestsByStatus(currentFilter);
            }
        }

        private void BorrowingPage_Load(object sender, EventArgs e)
        {
            LoadBorrowingRequests();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            // Clean up resources
            if (fileWatcher != null)
            {
                fileWatcher.EnableRaisingEvents = false;
                fileWatcher.Dispose();
            }
            
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }
        }

        private void LoadBorrowingRequests()
        {
            try
            {
                currentFilter = "All";
                var borrows = borrowController.GetAllBorrows();
                var rejectedBorrows = borrowController.GetAllRejectedBorrows();
                
                // Store current scroll position
                int scrollPosition = dgvBorrowingRequests.FirstDisplayedScrollingRowIndex;
                
                dgvBorrowingRequests.Rows.Clear();

                // Load active borrowing requests
                if (borrows != null && borrows.Count > 0)
                {
                    foreach (var borrow in borrows)
                    {
                        string displayStatus = GetDisplayStatus(borrow);
                        int rowIndex = dgvBorrowingRequests.Rows.Add(
                            borrow.StudentName,
                            borrow.BookRequested,
                            borrow.RequestDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            displayStatus,
                            "⋮"
                        );
                        // Store the actual Borrow ID in the row's Tag so we can retrieve it later
                        dgvBorrowingRequests.Rows[rowIndex].Tag = new { Type = "Borrow", Id = borrow.Id };
                    }
                }

                // Load rejected requests
                if (rejectedBorrows != null && rejectedBorrows.Count > 0)
                {
                    foreach (var reject in rejectedBorrows)
                    {
                        int rowIndex = dgvBorrowingRequests.Rows.Add(
                            reject.StudentName,
                            reject.BookRequested,
                            reject.RequestDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            reject.Status,
                            "⋮"
                        );
                        // Store the actual Reject ID in the row's Tag
                        dgvBorrowingRequests.Rows[rowIndex].Tag = new { Type = "Reject", Id = reject.Id };
                    }
                }

                // Restore scroll position if valid
                if (scrollPosition >= 0 && scrollPosition < dgvBorrowingRequests.Rows.Count)
                {
                    dgvBorrowingRequests.FirstDisplayedScrollingRowIndex = scrollPosition;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading borrowing requests: {ex.Message}");
            }
        }

        private void LoadBorrowingRequestsByStatus(string status)
        {
            try
            {
                currentFilter = status;
                var allBorrows = borrowController.GetAllBorrows();
                
                // Store current scroll position
                int scrollPosition = dgvBorrowingRequests.FirstDisplayedScrollingRowIndex;
                
                dgvBorrowingRequests.Rows.Clear();

                // Filter based on COMPUTED display status, not raw enum
                List<Borrow> filteredBorrows = allBorrows
                    .Where(b => GetDisplayStatus(b).Equals(status, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (filteredBorrows.Count > 0)
                {
                    foreach (var borrow in filteredBorrows)
                    {
                        string displayStatus = GetDisplayStatus(borrow);
                        int rowIndex = dgvBorrowingRequests.Rows.Add(
                            borrow.StudentName,
                            borrow.BookRequested,
                            borrow.RequestDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            displayStatus,
                            "⋮"
                        );
                        // Store the actual Borrow ID in the row's Tag so filtering doesn't break selection
                        dgvBorrowingRequests.Rows[rowIndex].Tag = new { Type = "Borrow", Id = borrow.Id };
                    }
                }

                // Restore scroll position if valid
                if (scrollPosition >= 0 && scrollPosition < dgvBorrowingRequests.Rows.Count)
                {
                    dgvBorrowingRequests.FirstDisplayedScrollingRowIndex = scrollPosition;
                }
            }
            catch (Exception ex)
            {
                // Silently log error to avoid popup spam during auto-refresh
                Console.WriteLine($"Error loading borrowing requests: {ex.Message}");
            }
        }

        public void RequestBorrow(string studentName, string bookTitle)
        {
            try
            {
                bool success = borrowController.CreateBorrowRequest(studentName, bookTitle);
                if (success)
                {
                    MessageBox.Show("Borrow request created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBorrowingRequests();
                }
                else
                {
                    MessageBox.Show("Failed to create borrow request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating borrow request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBorrowingRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex >= 0) // Actions column
            {
                // Get the actual ID from the row's Tag instead of relying on row index
                DataGridViewRow selectedRow = dgvBorrowingRequests.Rows[e.RowIndex];
                
                if (selectedRow.Tag == null)
                {
                    MessageBox.Show("Error: Could not retrieve record ID. Please refresh the page.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // The Tag contains the record type and ID
                dynamic tagData = selectedRow.Tag;
                string recordType = tagData.Type;
                Guid recordId = tagData.Id;

                if (recordType == "Reject")
                {
                    // For rejected requests, show remove option only
                    selectedBorrowId = recordId;
                    ShowRemoveMenu();
                }
                else if (recordType == "Borrow")
                {
                    // For active borrowing requests, show approve/reject options
                    selectedBorrowId = recordId;
                    contextMenuStripActions.Show(Cursor.Position);
                }
            }
        }

        private void ShowRemoveMenu()
        {
            ContextMenuStrip removeMenu = new ContextMenuStrip();
            ToolStripMenuItem removeItem = new ToolStripMenuItem("Remove");
            removeItem.Click += RemoveRejectedRequest_Click;
            removeMenu.Items.Add(removeItem);
            removeMenu.Show(Cursor.Position);
        }

        private void RemoveRejectedRequest_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this rejected request?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool success = borrowController.DeleteRejectedBorrow(selectedBorrowId);
                    if (success)
                    {
                        MessageBox.Show("Rejected request deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRejectedRequests();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete rejected request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting rejected request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void approveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = borrowController.ApproveBorrowRequest(selectedBorrowId);
                if (success)
                {
                    MessageBox.Show("Borrow request approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBorrowingRequests();
                }
                else
                {
                    MessageBox.Show("Failed to approve borrow request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving borrow request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rejectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = borrowController.RejectBorrowRequest(selectedBorrowId);
                if (success)
                {
                    MessageBox.Show("Borrow request rejected successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBorrowingRequests();
                }
                else
                {
                    MessageBox.Show("Failed to reject borrow request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting borrow request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadBorrowingRequests();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadBorrowingRequestsByStatus("Pending");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadBorrowingRequestsByStatus("Borrowed");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadBorrowingRequestsByStatus("Returned");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadBorrowingRequestsByStatus("Overdue");
        }

        private void btnRejected_Click(object sender, EventArgs e)
        {
            LoadRejectedRequests();
        }

        private void LoadRejectedRequests()
        {
            try
            {
                currentFilter = "Rejected";
                var rejectedBorrows = borrowController.GetAllRejectedBorrows();
                
                // Store current scroll position
                int scrollPosition = dgvBorrowingRequests.FirstDisplayedScrollingRowIndex;
                
                dgvBorrowingRequests.Rows.Clear();

                if (rejectedBorrows != null && rejectedBorrows.Count > 0)
                {
                    foreach (var reject in rejectedBorrows)
                    {
                        int rowIndex = dgvBorrowingRequests.Rows.Add(
                            reject.StudentName,
                            reject.BookRequested,
                            reject.RequestDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            reject.Status,
                            "⋮"
                        );
                        // Store the actual Reject ID in the row's Tag
                        dgvBorrowingRequests.Rows[rowIndex].Tag = new { Type = "Reject", Id = reject.Id };
                    }
                }

                // Restore scroll position if valid
                if (scrollPosition >= 0 && scrollPosition < dgvBorrowingRequests.Rows.Count)
                {
                    dgvBorrowingRequests.FirstDisplayedScrollingRowIndex = scrollPosition;
                }
            }
            catch (Exception ex)
            {
                // Silently log error to avoid popup spam
                Console.WriteLine($"Error loading rejected requests: {ex.Message}");
            }
        }

        private string GetDisplayStatus(Borrow borrow)
        {
            if (borrow == null)
                return "Unknown";

            // Terminal states - never change
            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Pending)
                return "Pending";

            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Returned)
                return "Returned";

            // Lost status (if enum supports it)
            if (borrow.Status.ToString().Equals("Lost", StringComparison.OrdinalIgnoreCase))
                return "Lost";

            // For Borrowed: check if actually overdue
            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Borrowed)
            {
                if (borrowController.IsBorrowOverdue(borrow))
                    return "Overdue";
                return "Borrowed";
            }

            // Overdue enum value
            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Overdue)
                return "Overdue";

            // Fallback
            return borrow.Status.ToString();
        }
    }
}
