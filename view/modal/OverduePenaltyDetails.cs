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
    public partial class OverduePenaltyDetails : Form
    {
        public OverduePenaltyDetails()
        {
            InitializeComponent();
        }

        public OverduePenaltyDetails(
            string studentName,
            string bookTitle,
            string dueDateDelay,
            string penaltyBreakdown,
            string payment) : this()
        {
            lblStudentValue.Text = studentName;
            lblBookValue.Text = bookTitle;
            lblDueDateValue.Text = dueDateDelay;
            lblPenaltyValue.Text = string.IsNullOrWhiteSpace(penaltyBreakdown) ? "-" : penaltyBreakdown;
            lblPaymentValue.Text = string.IsNullOrWhiteSpace(payment) ? "" : payment;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
