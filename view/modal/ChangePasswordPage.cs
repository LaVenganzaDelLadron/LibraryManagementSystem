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

namespace LibraryManagementSystem.view
{
    public partial class ChangePassword : Form
    {
        private string currentUsername;
        private ChangePasswordController passwordController;

        public ChangePassword(string username)
        {
            InitializeComponent();
            currentUsername = username;
            passwordController = new ChangePasswordController();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                MessageBox.Show("Please enter your current password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Please enter a new password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation do not match.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (currentPassword == newPassword)
            {
                MessageBox.Show("New password must be different from current password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            // Attempt to change password
            bool success = passwordController.ChangePassword(currentUsername, currentPassword, newPassword);

            if (success)
            {
                MessageBox.Show("Password changed successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Clear fields
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to change password. Current password is incorrect.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentPassword.Focus();
                txtCurrentPassword.SelectAll();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel? Any changes will be lost.",
                "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // Hover effects for Save button
        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.FromArgb(39, 174, 96); // Darker green
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.FromArgb(46, 204, 113); // Original green
        }

        // Hover effects for Cancel button
        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.FromArgb(149, 165, 166); // Darker gray
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.FromArgb(189, 195, 199); // Original gray
        }

        // Focus effects for text fields
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null)
            {
                txt.BackColor = Color.White;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null)
            {
                txt.BackColor = Color.FromArgb(245, 245, 245);
            }
        }
    }
}
