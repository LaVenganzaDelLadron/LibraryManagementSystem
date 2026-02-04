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
using LibraryManagementSystem.model;

namespace LibraryManagementSystem.view.modal
{
    internal partial class EditBook : Form
    {
        private readonly BookController _bookController = new BookController();
        private Guid _bookId;

        public EditBook(Books book)
        {
            InitializeComponent();
            InitializeCategoryList();
            WireEvents();
            LoadBook(book);
        }

        private void WireEvents()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void InitializeCategoryList()
        {
            comboBoxCategory.Items.Clear();
            var categories = _bookController.GetAllCategories();
            foreach (var category in categories)
            {
                if (!string.Equals(category, "All Categories", StringComparison.OrdinalIgnoreCase))
                {
                    comboBoxCategory.Items.Add(category);
                }
            }
        }

        private void LoadBook(Books book)
        {
            if (book == null)
            {
                MessageBox.Show("Unable to load book details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            _bookId = book.Id;
            textBoxTitle.Text = book.Title;
            textBoxAuthor.Text = book.Author;
            textBoxPublication.Text = book.PublishedDate.Year.ToString();
            textBoxCopies.Text = book.Copies.ToString();
            textBoxDescription.Text = book.Description;

            if (!comboBoxCategory.Items.Contains(book.Category))
            {
                comboBoxCategory.Items.Add(book.Category);
            }
            comboBoxCategory.SelectedItem = book.Category;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string title = textBoxTitle.Text.Trim();
            string author = textBoxAuthor.Text.Trim();
            string publicationInput = textBoxPublication.Text.Trim();
            string description = textBoxDescription.Text.Trim();
            string category = comboBoxCategory.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Title, Author, and Category are required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxCopies.Text.Trim(), out int copies) || copies < 0)
            {
                MessageBox.Show("Copies must be a valid non-negative number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime publishedDate;
            if (int.TryParse(publicationInput, out int year))
            {
                if (year < 1000 || year > 9999)
                {
                    MessageBox.Show("Publication year must be a valid year.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                publishedDate = new DateTime(year, 1, 1);
            }
            else if (!DateTime.TryParse(publicationInput, out publishedDate))
            {
                MessageBox.Show("Publication must be a valid year or date.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedBook = new Books(_bookId, title, author, publishedDate, description, category, copies);
            bool updated = _bookController.UpdateBook(updatedBook);

            if (updated)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Failed to update book. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
