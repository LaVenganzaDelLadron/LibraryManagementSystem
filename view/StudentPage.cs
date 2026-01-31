using LibraryManagementSystem.controller.studentRegistration;
using LibraryManagementSystem.view.modal;
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
    public partial class StudentPage : Form
    {
        AddStudent addStudent = new AddStudent();
        private int _selectedRowIndex = -1;
        private StudentRegistration _studentRegistration = new StudentRegistration();

        public StudentPage()
        {
            InitializeComponent();
            InitializeStudentsDataGridView();
        }

        private void InitializeStudentsDataGridView()
        {
            dataGridViewStudents.Columns.Clear();

            dataGridViewStudents.Columns.Add("StudentName", "Student Name");
            dataGridViewStudents.Columns.Add("Email", "Email");
            dataGridViewStudents.Columns.Add("ContactNumber", "Contact Number");
            dataGridViewStudents.Columns.Add("JoinDate", "Join Date");
            dataGridViewStudents.Columns.Add("Status", "Status");

            DataGridViewTextBoxColumn actionsColumn = new DataGridViewTextBoxColumn();
            actionsColumn.Name = "Actions";
            actionsColumn.HeaderText = "Actions";
            actionsColumn.Width = 70;
            actionsColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            actionsColumn.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            dataGridViewStudents.Columns.Add(actionsColumn);

            dataGridViewStudents.EnableHeadersVisualStyles = false;
            dataGridViewStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 128, 128);
            dataGridViewStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewStudents.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            dataGridViewStudents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewStudents.ColumnHeadersHeight = 40;
            dataGridViewStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
            dataGridViewStudents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 136);
            dataGridViewStudents.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewStudents.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);
            dataGridViewStudents.RowTemplate.Height = 35;
            dataGridViewStudents.BorderStyle = BorderStyle.None;
            
            // Center align JoinDate and Status columns
            dataGridViewStudents.Columns["JoinDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewStudents.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewStudents.Columns["Status"].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            // Add event handlers
            dataGridViewStudents.CellMouseEnter += DataGridViewStudents_CellMouseEnter;
            dataGridViewStudents.CellMouseLeave += DataGridViewStudents_CellMouseLeave;
            dataGridViewStudents.CellFormatting += DataGridViewStudents_CellFormatting;

            LoadStudentData();
        }

        private void LoadStudentData()
        {
            dataGridViewStudents.Rows.Clear();
            var students = _studentRegistration.GetAllStudents();

            foreach (var student in students)
            {
                dataGridViewStudents.Rows.Add(
                    student.FirstName + " " + student.LastName,
                    student.Email,
                    student.ContactNo,
                    student.JoinDate.ToString("yyyy-MM-dd"),
                    student.Status.ToString(),
                    "⋮"  // Three vertical dots
                );
            }
        }

        private void AddSampleStudentData()
        {
            dataGridViewStudents.Rows.Add("Amina Yusuf", "amina.yusuf@email.com", "+234 801 234 5678", "2025-09-12", "Active");
            dataGridViewStudents.Rows.Add("David Mensah", "david.mensah@email.com", "+233 24 123 4567", "2025-10-02", "Active");
            dataGridViewStudents.Rows.Add("Grace Njeri", "grace.njeri@email.com", "+254 712 345 678", "2025-11-18", "Active");
            dataGridViewStudents.Rows.Add("Samuel Okoro", "samuel.okoro@email.com", "+234 809 876 5432", "2026-01-05", "Active");
        }

        private void dataGridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == dataGridViewStudents.Columns["Actions"].Index)
            {
                _selectedRowIndex = e.RowIndex;
                var cellRect = dataGridViewStudents.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                contextMenuStripActions.Show(dataGridViewStudents, cellRect.Left, cellRect.Bottom);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex < 0)
            {
                return;
            }

            string name = dataGridViewStudents.Rows[_selectedRowIndex].Cells["StudentName"].Value.ToString();
            MessageBox.Show($"Edit student: {name}", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void banToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex < 0)
            {
                return;
            }

            string name = dataGridViewStudents.Rows[_selectedRowIndex].Cells["StudentName"].Value.ToString();
            DialogResult result = MessageBox.Show($"Ban student: {name}?", "Confirm Ban", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Student has been banned.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex < 0)
            {
                return;
            }

            string name = dataGridViewStudents.Rows[_selectedRowIndex].Cells["StudentName"].Value.ToString();
            DialogResult result = MessageBox.Show($"Remove student: {name}?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                dataGridViewStudents.Rows.RemoveAt(_selectedRowIndex);
                _selectedRowIndex = -1;
                MessageBox.Show("Student removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            addStudent = new AddStudent();
            if (addStudent.ShowDialog() == DialogResult.OK)
            {
                LoadStudentData();
            }
        }

        private void DataGridViewStudents_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewStudents.Columns["Actions"].Index)
            {
                dataGridViewStudents.Cursor = Cursors.Hand;
            }
        }

        private void DataGridViewStudents_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewStudents.Cursor = Cursors.Default;
        }

        private void DataGridViewStudents_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewStudents.Columns["Status"].Index && e.RowIndex >= 0)
            {
                string status = e.Value?.ToString() ?? "";
                
                if (status.IndexOf("Active", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0); // Green
                }
                else if (status.IndexOf("Banned", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 20, 60); // Red
                }
                else if (status.IndexOf("Inactive", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128); // Gray
                }
            }
        }
    }
}
