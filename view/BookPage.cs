using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagementSystem.controller.book;
using LibraryManagementSystem.view.modal;

namespace LibraryManagementSystem
{
    public partial class BookPage : Form
    {
        AddCategory addCategory= new AddCategory();
        AddBook addBook = new AddBook();
        BookService bookService = new BookService();

        private int _selectedRowIndex = -1;

        public BookPage()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            // Clear existing columnsaaaa
            dataGridViewBooks.Columns.Clear();
            aaaaaa

            // Add columns for book information
            dataGridViewBooks.Columns.Add("Title", "Title");
            dataGridViewBooks.Columns.Add("Author", "Author");
            dataGridViewBooks.Columns.Add("Category", "Category");
            dataGridViewBooks.Columns.Add("PublishedDate", "Published Date");
            dataGridViewBooks.Columns.Add("Copies", "Copies");

            // Add Actions (three dots) column
            DataGridViewButtonColumn actionsColumn = new DataGridViewButtonColumn();
            actionsColumn.Name = "Actions";
            actionsColumn.HeaderText = "Actions";
            actionsColumn.Text = "...";
            actionsColumn.UseColumnTextForButtonValue = true;
            actionsColumn.Width = 60;
            dataGridViewBooks.Columns.Add(actionsColumn);

            // Style the DataGridView
            dataGridViewBooks.EnableHeadersVisualStyles = false;
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewBooks.ColumnHeadersHeight = 40;
            dataGridViewBooks.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            dataGridViewBooks.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dataGridViewBooks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            // Load books from service
            LoadBooksData();
        }

        private void LoadBooksData()
        {
            dataGridViewBooks.Rows.Clear();
            var books = bookService.GetAllBooks();

            foreach (var book in books)
            {
                dataGridViewBooks.Rows.Add(
                    book.Title,
                    book.Author,
                    book.Category,
                    book.PublishedDate.ToString("yyyy-MM-dd"),
                    book.Copies
                );
            }
        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            _selectedRowIndex = e.RowIndex;

            if (e.ColumnIndex == dataGridViewBooks.Columns["Actions"].Index)
            {
                var cellRect = dataGridViewBooks.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var menuPoint = dataGridViewBooks.PointToScreen(new Point(cellRect.Left, cellRect.Bottom));
                contextMenuStripActions.Show(menuPoint);
            }
            else
            {
                // Show book details when clicking on any other cell
                ShowBookDetails(_selectedRowIndex);
            }
        }

        private void ShowBookDetails(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dataGridViewBooks.Rows.Count)
                return;

            var books = bookService.GetAllBooks();
            if (rowIndex >= books.Count)
                return;

            var book = books[rowIndex];
            string details = $"Title: {book.Title}\n\n" +
                           $"Author: {book.Author}\n\n" +
                           $"Published Date: {book.PublishedDate:yyyy-MM-dd}\n\n" +
                           $"Category: {book.Category}\n\n" +
                           $"Description: {book.Description}\n\n" +
                           $"Copies Available: {book.Copies}";

            MessageBox.Show(details, "Book Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex < 0)
            {
                return;
            }

            string bookTitle = dataGridViewBooks.Rows[_selectedRowIndex].Cells["Title"].Value.ToString();
            MessageBox.Show($"Edit functionality for '{bookTitle}' will be implemented soon.", "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex < 0)
            {
                return;
            }

            var books = bookService.GetAllBooks();
            if (_selectedRowIndex >= books.Count)
                return;

            var book = books[_selectedRowIndex];
            DialogResult result = MessageBox.Show($"Are you sure you want to delete: {book.Title}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (bookService.DeleteBook(book.Id))
                {
                    LoadBooksData();
                    _selectedRowIndex = -1;
                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            addCategory.ShowDialog();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            addBook = new AddBook();
            if (addBook.ShowDialog() == DialogResult.OK)
            {
                LoadBooksData();
            }
        }
    }
}
