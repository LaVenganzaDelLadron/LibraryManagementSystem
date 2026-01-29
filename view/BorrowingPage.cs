using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem.view
{
    public partial class BorrowingPage : Form
    {
        private int selectedRowIndex = -1;

        public BorrowingPage()
        {
            InitializeComponent();
            LoadBorrowingRequests();
        }

        private void LoadBorrowingRequests()
        {
            // Sample data - replace with actual data from your database/service
            dgvBorrowingRequests.Rows.Add("John Doe", "C# Programming", "2026-01-25", "Pending");
            dgvBorrowingRequests.Rows.Add("Jane Smith", "Database Design", "2026-01-28", "Pending");
            dgvBorrowingRequests.Rows.Add("Mike Johnson", "Web Development", "2026-01-29", "Approved");
        }

        private void dgvBorrowingRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if Actions button was clicked
            if (e.ColumnIndex == dgvBorrowingRequests.Columns["colActions"].Index)
            {
                selectedRowIndex = e.RowIndex;
                
                // Get the location to show the context menu
                var rect = dgvBorrowingRequests.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var point = new Point(rect.Left, rect.Bottom);
                
                // Show context menu
                contextMenuStripActions.Show(dgvBorrowingRequests, point);
            }
        }

        private void approveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0) return;

            string studentName = dgvBorrowingRequests.Rows[selectedRowIndex].Cells["colStudentName"].Value.ToString();
            string bookName = dgvBorrowingRequests.Rows[selectedRowIndex].Cells["colBookRequested"].Value.ToString();

            var result = MessageBox.Show($"Approve request for {studentName} to borrow '{bookName}'?",
                "Approve Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dgvBorrowingRequests.Rows[selectedRowIndex].Cells["colStatus"].Value = "Approved";
                MessageBox.Show("Request approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rejectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex < 0) return;

            string studentName = dgvBorrowingRequests.Rows[selectedRowIndex].Cells["colStudentName"].Value.ToString();
            string bookName = dgvBorrowingRequests.Rows[selectedRowIndex].Cells["colBookRequested"].Value.ToString();

            var result = MessageBox.Show($"Reject request for {studentName} to borrow '{bookName}'?",
                "Reject Request", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                dgvBorrowingRequests.Rows[selectedRowIndex].Cells["colStatus"].Value = "Rejected";
                MessageBox.Show("Request rejected.", "Rejected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
