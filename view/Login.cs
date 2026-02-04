using LibraryManagementSystem.controller;
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
    public partial class Login : Form
    {
        AuthService authService = new AuthService();
        public Login()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            Signup signup = new Signup();
            signup.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = textBoxEmail.Text;
            var password = textBoxPassword.Text;


            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            bool isSuccess = authService.Login(email, password);

            if (isSuccess)
            {
                string username = authService.GetLoggedInUsername(email, password);
                MessageBox.Show("Login successful!");
                Dashboard dashboard = new Dashboard(username);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login failed! Invalid email or password.");
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
