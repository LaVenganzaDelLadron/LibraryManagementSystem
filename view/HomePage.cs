using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
            InitializeTransactionsDataGridView();
        }

        private void InitializeTransactionsDataGridView()
        {
            // Clear existing columns
            dataGridViewTransactions.Columns.Clear();

            // Add columns
            dataGridViewTransactions.Columns.Add("Student", "Student");
            dataGridViewTransactions.Columns.Add("Books", "Books");
            dataGridViewTransactions.Columns.Add("Date", "Date");
            dataGridViewTransactions.Columns.Add("Status", "Status");

            // Add Remove button column
            DataGridViewButtonColumn removeButtonColumn = new DataGridViewButtonColumn();
            removeButtonColumn.Name = "Remove";
            removeButtonColumn.HeaderText = "Action";
            removeButtonColumn.Text = "Remove";
            removeButtonColumn.UseColumnTextForButtonValue = true;
            removeButtonColumn.Width = 100;
            dataGridViewTransactions.Columns.Add(removeButtonColumn);

            // Style the DataGridView
            dataGridViewTransactions.EnableHeadersVisualStyles = false;
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewTransactions.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewTransactions.ColumnHeadersHeight = 40;
            dataGridViewTransactions.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dataGridViewTransactions.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dataGridViewTransactions.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            // Add sample data
            AddSampleTransactionData();
        }

        private void AddSampleTransactionData()
        {
            // Sample transaction data - replace with actual database queries
            dataGridViewTransactions.Rows.Add("John Doe", "The Great Gatsby", "2026-01-25", "Borrowed");
            dataGridViewTransactions.Rows.Add("Jane Smith", "To Kill a Mockingbird", "2026-01-24", "Returned");
            dataGridViewTransactions.Rows.Add("Mike Johnson", "1984", "2026-01-23", "Borrowed");
            dataGridViewTransactions.Rows.Add("Sarah Williams", "Pride and Prejudice", "2026-01-22", "Overdue");
            dataGridViewTransactions.Rows.Add("Tom Brown", "The Catcher in the Rye", "2026-01-21", "Returned");
            dataGridViewTransactions.Rows.Add("Emily Davis", "Harry Potter Series", "2026-01-20", "Borrowed");
            dataGridViewTransactions.Rows.Add("Chris Wilson", "The Hobbit", "2026-01-19", "Returned");
        }

        private void dataGridViewTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on the Remove button column
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewTransactions.Columns["Remove"].Index)
            {
                string student = dataGridViewTransactions.Rows[e.RowIndex].Cells["Student"].Value.ToString();
                string book = dataGridViewTransactions.Rows[e.RowIndex].Cells["Books"].Value.ToString();
                
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to remove this transaction?\n\nStudent: {student}\nBook: {book}",
                    "Confirm Remove",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    dataGridViewTransactions.Rows.RemoveAt(e.RowIndex);
                    MessageBox.Show("Transaction removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Add your database delete logic here
                }
            }
        }
    }
}
