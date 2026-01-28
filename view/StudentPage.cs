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

            DataGridViewButtonColumn actionsColumn = new DataGridViewButtonColumn();
            actionsColumn.Name = "Actions";
            actionsColumn.HeaderText = "Actions";
            actionsColumn.Text = "...";
            actionsColumn.UseColumnTextForButtonValue = true;
            actionsColumn.Width = 60;
            dataGridViewStudents.Columns.Add(actionsColumn);

            dataGridViewStudents.EnableHeadersVisualStyles = false;
            dataGridViewStudents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dataGridViewStudents.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewStudents.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            dataGridViewStudents.ColumnHeadersHeight = 40;
            dataGridViewStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewStudents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dataGridViewStudents.DefaultCellStyle.SelectionForeColor = Color.White;

            AddSampleStudentData();
        }

        private void AddSampleStudentData()
        {
            dataGridViewStudents.Rows.Add("Amina Yusuf", "amina.yusuf@email.com", "+234 801 234 5678", "2025-09-12");
            dataGridViewStudents.Rows.Add("David Mensah", "david.mensah@email.com", "+233 24 123 4567", "2025-10-02");
            dataGridViewStudents.Rows.Add("Grace Njeri", "grace.njeri@email.com", "+254 712 345 678", "2025-11-18");
            dataGridViewStudents.Rows.Add("Samuel Okoro", "samuel.okoro@email.com", "+234 809 876 5432", "2026-01-05");
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
                var cellRect = dataGridViewStudents.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var menuPoint = dataGridViewStudents.PointToScreen(new Point(cellRect.Left, cellRect.Bottom));
                contextMenuStripActions.Show(menuPoint);
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
            addStudent.ShowDialog();
        }
    }
}
