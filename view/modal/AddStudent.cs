using LibraryManagementSystem.controller.studentRegistration;
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
    public partial class AddStudent : Form
    {
        StudentRegistration studentRegistration = new StudentRegistration();
        public AddStudent()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var userName = textBoxUsername.Text;
            var firstName = textBoxFirstname.Text;
            var lastName = textBoxLastname.Text;
            var department = textBoxDepartment.Text;
            var email = textBoxEmail.Text;
            var contactNumber = textBoxContactNumber.Text;
            var password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(userName) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(department) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(contactNumber) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            bool isSuccess = studentRegistration.RegisterStudent(userName, firstName, lastName, email, password, contactNumber, department);

            if (isSuccess)
            {
                MessageBox.Show("Student added successfully!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add student! Username or Email already exists.");
            }

        }
    }
}
