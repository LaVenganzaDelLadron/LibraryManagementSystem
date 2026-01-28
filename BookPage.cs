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
    public partial class BookPage : Form
    {
        public BookPage()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            // Clear existing columns
            dataGridViewBooks.Columns.Clear();

            // Add columns
            dataGridViewBooks.Columns.Add("BookDetails", "Book Details");
            dataGridViewBooks.Columns.Add("Publication", "Publication");
            dataGridViewBooks.Columns.Add("Inventory", "Inventory");
            dataGridViewBooks.Columns.Add("Status", "Status");

            // Add Edit button column
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.Name = "Edit";
            editButtonColumn.HeaderText = "Action";
            editButtonColumn.Text = "Edit";
            editButtonColumn.UseColumnTextForButtonValue = true;
            editButtonColumn.Width = 80;
            dataGridViewBooks.Columns.Add(editButtonColumn);

            // Add Delete button column
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.Width = 80;
            dataGridViewBooks.Columns.Add(deleteButtonColumn);

            // Style the DataGridView
            dataGridViewBooks.EnableHeadersVisualStyles = false;
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewBooks.ColumnHeadersHeight = 40;
            dataGridViewBooks.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dataGridViewBooks.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dataGridViewBooks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            // Add sample data (you can replace this with actual data from your database)
            AddSampleData();
        }

        private void AddSampleData()
        {
            // Sample data - replace with actual database queries
            dataGridViewBooks.Rows.Add("The Great Gatsby - F. Scott Fitzgerald", "Scribner - 1925", "15 copies", "Available");
            dataGridViewBooks.Rows.Add("To Kill a Mockingbird - Harper Lee", "J.B. Lippincott & Co. - 1960", "10 copies", "Available");
            dataGridViewBooks.Rows.Add("1984 - George Orwell", "Secker & Warburg - 1949", "8 copies", "Limited");
            dataGridViewBooks.Rows.Add("Pride and Prejudice - Jane Austen", "T. Egerton - 1813", "12 copies", "Available");
            dataGridViewBooks.Rows.Add("The Catcher in the Rye - J.D. Salinger", "Little, Brown - 1951", "2 copies", "Low Stock");
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a button column
            if (e.RowIndex >= 0)
            {
                // Edit button clicked
                if (e.ColumnIndex == dataGridViewBooks.Columns["Edit"].Index)
                {
                    string bookDetails = dataGridViewBooks.Rows[e.RowIndex].Cells["BookDetails"].Value.ToString();
                    MessageBox.Show($"Edit book: {bookDetails}", "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Add your edit logic here
                }
                // Delete button clicked
                else if (e.ColumnIndex == dataGridViewBooks.Columns["Delete"].Index)
                {
                    string bookDetails = dataGridViewBooks.Rows[e.RowIndex].Cells["BookDetails"].Value.ToString();
                    DialogResult result = MessageBox.Show($"Are you sure you want to delete: {bookDetails}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.Yes)
                    {
                        dataGridViewBooks.Rows.RemoveAt(e.RowIndex);
                        MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Add your delete logic here (e.g., delete from database)
                    }
                }
            }
        }
    }
}
