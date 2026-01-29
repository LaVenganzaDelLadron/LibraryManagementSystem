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
    public partial class ReportPage : Form
    {
        public ReportPage()
        {
            InitializeComponent();
            LoadOverduePenaltyData();
        }

        private void LoadOverduePenaltyData()
        {
            // Sample data - replace with actual data from your database/service
            dgvOverduePenalty.Rows.Add(
                "John Doe - C# Programming",
                "2026-01-20 (9 days)",
                "9 days × ₱10 = ₱90",
                "Unpaid"
            );
            
            dgvOverduePenalty.Rows.Add(
                "Jane Smith - Database Design",
                "2026-01-15 (14 days)",
                "14 days × ₱10 = ₱140",
                "Unpaid"
            );
            
            dgvOverduePenalty.Rows.Add(
                "Mike Johnson - Web Dev",
                "2026-01-22 (7 days)",
                "7 days × ₱10 = ₱70",
                "Paid"
            );
            
            dgvOverduePenalty.Rows.Add(
                "Sarah Williams - Java Basics",
                "2026-01-10 (19 days)",
                "19 days × ₱10 = ₱190",
                "Unpaid"
            );
        }

        private void dgvOverduePenalty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if Pay button was clicked
            if (e.ColumnIndex == dgvOverduePenalty.Columns["colActions"].Index)
            {
                string studentBook = dgvOverduePenalty.Rows[e.RowIndex].Cells["colStudentBook"].Value.ToString();
                string penaltyBreakdown = dgvOverduePenalty.Rows[e.RowIndex].Cells["colPenaltyBreakdown"].Value.ToString();
                string paymentStatus = dgvOverduePenalty.Rows[e.RowIndex].Cells["colPayment"].Value.ToString();

                if (paymentStatus == "Paid")
                {
                    MessageBox.Show("This penalty has already been paid.", "Already Paid", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"Process payment for {studentBook}?\n\nAmount: {penaltyBreakdown}",
                    "Process Payment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dgvOverduePenalty.Rows[e.RowIndex].Cells["colPayment"].Value = "Paid";
                    MessageBox.Show("Payment processed successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
