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
    public partial class ProfilePage : Form
    {
        private DisplayProfile profileController;
        private Users currentUser;
        private string currentUsername;

        public ProfilePage()
        {
            InitializeComponent();
            ApplyButtonStyling();
            profileController = new DisplayProfile();
        }

        public ProfilePage(string username) : this()
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

                DisplayUserInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayUserInfo()
        {
            if (currentUser == null)
                return;

            // Display full name
            string fullName = GetFullName();
            lblFullName.Text = fullName;

            // Display first name
            lblFirstNameValue.Text = !string.IsNullOrWhiteSpace(currentUser.FirstName) 
                ? currentUser.FirstName 
                : "Not provided";

            // Display last name
            lblLastNameValue.Text = !string.IsNullOrWhiteSpace(currentUser.LastName) 
                ? currentUser.LastName 
                : "Not provided";

            // Display email
            lblEmailValue.Text = !string.IsNullOrWhiteSpace(currentUser.Email) 
                ? currentUser.Email 
                : "Not provided";

            // Display contact number
            lblContactValue.Text = !string.IsNullOrWhiteSpace(currentUser.ContactNo) 
                ? currentUser.ContactNo 
                : "Not provided";
        }

        private string GetFullName()
        {
            if (!string.IsNullOrWhiteSpace(currentUser.FirstName) && !string.IsNullOrWhiteSpace(currentUser.LastName))
            {
                return $"{currentUser.FirstName} {currentUser.LastName}";
            }
            else if (!string.IsNullOrWhiteSpace(currentUser.FirstName))
            {
                return currentUser.FirstName;
            }
            else if (!string.IsNullOrWhiteSpace(currentUser.LastName))
            {
                return currentUser.LastName;
            }
            else if (!string.IsNullOrWhiteSpace(currentUser.UserName))
            {
                return currentUser.UserName;
            }
            
            return "User";
        }

        private void ApplyButtonStyling()
        {
            // Set button flat style and border
            foreach (Control control in panelActionButtons.Controls)
            {
                if (control is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                }
            }
        }

        // Edit Profile Button Events
        private void BtnEditProfile_MouseEnter(object sender, EventArgs e)
        {
            btnEditProfile.BackColor = Color.FromArgb(39, 174, 96); // Darker green
        }

        private void BtnEditProfile_MouseLeave(object sender, EventArgs e)
        {
            btnEditProfile.BackColor = Color.FromArgb(46, 204, 113); // Original green
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            // Open Edit Profile page
            EditProfilePage editPage = new EditProfilePage(currentUsername);
            editPage.ShowDialog(this);
            
            // Reload profile after editing
            LoadUserProfile();
        }

        // Change Password Button Events
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword changePasswordPage = new ChangePassword(currentUsername);
            changePasswordPage.ShowDialog(this);
        }

        private void BtnChangePassword_MouseEnter(object sender, EventArgs e)
        {
            btnChangePassword.BackColor = Color.FromArgb(41, 128, 185); // Darker blue
        }

        private void BtnChangePassword_MouseLeave(object sender, EventArgs e)
        {
            btnChangePassword.BackColor = Color.FromArgb(52, 152, 219); // Original blue
        }

        // Back Button Events
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
            this.Close();
        }
    }
}
