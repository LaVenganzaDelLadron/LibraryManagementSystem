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
        BookController bookService = new BookController();
        CategoryController categoryController = new CategoryController();
        
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
            var publishedYear = textBoxPublication.Text;
            var description = textBoxDescription.Text;
            var copies = textBoxCopies.Text;
            var category = comboBoxCategory.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(publishedYear) || string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(copies) || string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                int year;
                int numCopies;
                
                // Validate year is a valid integer
                if (!int.TryParse(publishedYear, out year))
                {
                    MessageBox.Show("Please enter a valid year (e.g., 2024).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Validate year is not in the future
                int currentYear = DateTime.Now.Year;
                if (year > currentYear)
                {
                    MessageBox.Show($"Publication year cannot be in the future. Current year is {currentYear}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Validate year is reasonable (not too far in the past)
                if (year < 0000 || year > currentYear)
                {
                    MessageBox.Show($"Please enter a valid year between 1000 and {currentYear}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (!int.TryParse(copies, out numCopies) || numCopies < 0)
                {
                    MessageBox.Show("Please enter a valid number of copies.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Convert year to DateTime (January 1st of that year)
                DateTime pubDate = new DateTime(year, 1, 1);
                
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
