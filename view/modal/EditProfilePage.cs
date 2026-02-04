using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagementSystem.controller.admin;
using LibraryManagementSystem.model;

namespace LibraryManagementSystem.view
{
    public partial class EditProfilePage : Form
    {
        private Color originalTextBoxBg = Color.FromArgb(245, 245, 245);
        private Color focusedTextBoxBg = Color.FromArgb(255, 255, 255);
        private Color focusedBorderColor = Color.FromArgb(52, 152, 219);
        
        private DisplayProfile profileController;
        private Users currentUser;
        private string currentUsername;

        public EditProfilePage()
        {
            InitializeComponent();
            ApplyTextBoxStyling();
            profileController = new DisplayProfile();
        }

        public EditProfilePage(string username) : this()
        {
            currentUsername = username;
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(currentUsername))
                {
                    MessageBox.Show("No user session found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                currentUser = profileController.GetUserByUsername(currentUsername);

                if (currentUser == null)
                {
                    MessageBox.Show("Unable to load user profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                PopulateFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFields()
        {
            if (currentUser == null)
                return;

            txtFirstName.Text = currentUser.FirstName ?? "";
            txtLastName.Text = currentUser.LastName ?? "";
            txtEmail.Text = currentUser.Email ?? "";
            txtContact.Text = currentUser.ContactNo ?? "";
        }

        private void SaveChanges()
        {
            try
            {
                if (currentUser == null)
                    return;

                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("First Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("Last Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLastName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // Update user object
                currentUser.FirstName = txtFirstName.Text.Trim();
                currentUser.LastName = txtLastName.Text.Trim();
                currentUser.Email = txtEmail.Text.Trim();
                currentUser.ContactNo = txtContact.Text.Trim();

                // Save to database
                bool success = profileController.UpdateUserProfile(currentUser);

                if (success)
                {
                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyTextBoxStyling()
        {
            // Apply consistent styling to all text boxes
            foreach (Control control in panelEditCard.Controls)
            {
                if (control is TextBox txt)
                {
                    txt.BackColor = originalTextBoxBg;
                    txt.Cursor = Cursors.IBeam;
                }
            }
        }

        // TextBox Input Events - Focus and Hover Effects
        private void TxtInput_MouseEnter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && !txt.Focused)
            {
                txt.BackColor = Color.FromArgb(250, 250, 250); // Slightly lighter on hover
            }
        }

        private void TxtInput_MouseLeave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && !txt.Focused)
            {
                txt.BackColor = originalTextBoxBg;
            }
        }

        private void TxtInput_GotFocus(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null)
            {
                txt.BackColor = focusedTextBoxBg;
                txt.SelectAll(); // Select all text when focused
            }
        }

        private void TxtInput_LostFocus(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null)
            {
                txt.BackColor = originalTextBoxBg;
            }
        }

        // Save Changes Button Events (Primary - Green)
        private void BtnSaveChanges_MouseEnter(object sender, EventArgs e)
        {
            btnSaveChanges.BackColor = Color.FromArgb(39, 174, 96); // Darker green
            btnSaveChanges.Font = new Font(btnSaveChanges.Font, FontStyle.Bold);
        }

        private void BtnSaveChanges_MouseLeave(object sender, EventArgs e)
        {
            btnSaveChanges.BackColor = Color.FromArgb(46, 204, 113); // Original green
            btnSaveChanges.Font = new Font(btnSaveChanges.Font, FontStyle.Bold);
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        // Cancel Button Events (Secondary - Blue)
        private void BtnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.FromArgb(41, 128, 185); // Darker blue
        }

        private void BtnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.FromArgb(52, 152, 219); // Original blue
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel? Any unsaved changes will be lost.", 
                "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // Back Button Events (Tertiary - Gray)
        private void BtnBack_MouseEnter(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(149, 165, 166); // Darker gray
        }

        private void BtnBack_MouseLeave(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(189, 195, 199); // Original gray
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
