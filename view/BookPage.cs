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
using LibraryManagementSystem.model;

namespace LibraryManagementSystem
{
    public partial class BookPage : Form
    {
        AddCategory addCategory= new AddCategory();
        AddBook addBook = new AddBook();
        BookController bookService = new BookController();

        private int _selectedRowIndex = -1;
        private List<Books> _currentBooks = new List<Books>();

        public BookPage()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeCategoryFilter();
        }

        private void InitializeCategoryFilter()
        {
            comboBoxCategory.Items.Clear();
            var categories = bookService.GetAllCategories();
            foreach (var category in categories)
            {
                comboBoxCategory.Items.Add(category);
            }
            comboBoxCategory.SelectedIndex = 0;
            comboBoxCategory.SelectedIndexChanged += ComboBoxCategory_SelectedIndexChanged;
        }

        private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = comboBoxCategory.SelectedItem?.ToString() ?? "All Categories";
            FilterAndDisplay(selectedCategory);
        }

        private void FilterAndDisplay(string category)
        {
            _currentBooks = bookService.GetBooksByCategory(category);
            DisplayBooks(_currentBooks);
        }

        private void InitializeDataGridView()
        {
            // Clear existing columnsaaaa
            dataGridViewBooks.Columns.Clear();

            // Add columns for book information
            dataGridViewBooks.Columns.Add("Title", "Title");
            dataGridViewBooks.Columns.Add("Author", "Author");
            dataGridViewBooks.Columns.Add("Category", "Category");
            dataGridViewBooks.Columns.Add("PublishedDate", "Published Date");
            dataGridViewBooks.Columns.Add("Copies", "Copies");

            // Add Actions (three dots) column
            DataGridViewTextBoxColumn actionsColumn = new DataGridViewTextBoxColumn();
            actionsColumn.Name = "Actions";
            actionsColumn.HeaderText = "Actions";
            actionsColumn.Width = 70;
            actionsColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            actionsColumn.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            dataGridViewBooks.Columns.Add(actionsColumn);

            // Style the DataGridView
            dataGridViewBooks.EnableHeadersVisualStyles = false;
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 128, 128);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewBooks.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewBooks.ColumnHeadersHeight = 40;
            dataGridViewBooks.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dataGridViewBooks.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 150, 136);
            dataGridViewBooks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewBooks.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);
            dataGridViewBooks.RowTemplate.Height = 35;
            dataGridViewBooks.BorderStyle = BorderStyle.None;
            
            // Center align Published Date and Copies columns
            dataGridViewBooks.Columns["PublishedDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewBooks.Columns["Copies"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewBooks.Columns["Copies"].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            // Add event handlers
            dataGridViewBooks.CellMouseEnter += DataGridViewBooks_CellMouseEnter;
            dataGridViewBooks.CellMouseLeave += DataGridViewBooks_CellMouseLeave;
            dataGridViewBooks.CellFormatting += DataGridViewBooks_CellFormatting;

            // Load books from service
            LoadBooksData();
        }

        private void LoadBooksData()
        {
            _currentBooks = bookService.GetAllBooks();
            DisplayBooks(_currentBooks);
        }

        private void DisplayBooks(List<Books> books)
        {
            dataGridViewBooks.Rows.Clear();

            foreach (var book in books)
            {
                dataGridViewBooks.Rows.Add(
                    book.Title,
                    book.Author,
                    book.Category,
                    book.PublishedDate.Year.ToString(),
                    book.Copies,
                    "⋮"  // Three vertical dots
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
                var cellRect = dataGridViewBooks.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                contextMenuStripActions.Show(dataGridViewBooks, cellRect.Left, cellRect.Bottom);
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
                           $"Published Year: {book.PublishedDate.Year}\n\n" +
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

        private void DataGridViewBooks_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewBooks.Columns["Actions"].Index)
            {
                dataGridViewBooks.Cursor = Cursors.Hand;
            }
        }

        private void DataGridViewBooks_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewBooks.Cursor = Cursors.Default;
        }

        private void DataGridViewBooks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewBooks.Columns["Copies"].Index && e.RowIndex >= 0)
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int copies))
                {
                    if (copies == 0)
                    {
                        e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 20, 60); // Red
                    }
                    else if (copies < 5)
                    {
                        e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 140, 0); // Orange
                    }
                    else
                    {
                        e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0); // Green
                    }
                }
            }
        }
    }
}
