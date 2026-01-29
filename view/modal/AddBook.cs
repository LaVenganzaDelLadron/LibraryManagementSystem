using LibraryManagementSystem.controller.book;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem.view.modal
{
    public partial class AddBook : Form
    {
        BookService bookService = new BookService();
        Category categoryController = new Category();
        
        public AddBook()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = categoryController.GetAllCategories();
            comboBoxCategory.Items.Clear();
            
            if (categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    comboBoxCategory.Items.Add(category.Name);
                }
                comboBoxCategory.SelectedIndex = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var title = textBoxTitle.Text;
            var author = textBoxAuthor.Text;
            var publishedDate = textBoxPublication.Text;
            var description = textBoxDescription.Text;
            var copies = textBoxCopies.Text;
            var category = comboBoxCategory.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(publishedDate) || string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(copies) || string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DateTime pubDate;
                int numCopies;
                if (!DateTime.TryParse(publishedDate, out pubDate))
                {
                    MessageBox.Show("Please enter a valid publication date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(copies, out numCopies) || numCopies < 0)
                {
                    MessageBox.Show("Please enter a valid number of copies.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                bool isAdded = bookService.AddBook(title, author, pubDate, description, category, numCopies);
                if (isAdded)
                {
                    MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add book. It may already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
