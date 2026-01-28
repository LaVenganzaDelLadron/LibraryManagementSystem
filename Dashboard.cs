using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LibraryManagementSystem
{
    public partial class Dashboard : Form
    {

        HomePage home = new HomePage();
        BookPage book = new BookPage();



        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,
                int nTopRect,
                int nRightRec,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
            );

        public Dashboard()
        {
            InitializeComponent();
            LoadForm(new HomePage());
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void LoadForm(Form form)
        {
            pnlFormLoader.Controls.Clear();
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.TopMost = true;
            form.Dock = DockStyle.None;
            pnlFormLoader.Controls.Add(form);
            form.BringToFront();
            form.Show();
        }


        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            LoadForm(home);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnBooks.Height;
            pnlNav.Top = btnBooks.Top;
            pnlNav.Left = btnBooks.Left;
            LoadForm(book);
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnStudent.Height;
            pnlNav.Top = btnStudent.Top;
            pnlNav.Left = btnStudent.Left;
        }

        private void btnBorrowing_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnBorrowing.Height;
            pnlNav.Top = btnBorrowing.Top;
            pnlNav.Left = btnBorrowing.Left;
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnReports.Height;
            pnlNav.Top = btnReports.Top;
            pnlNav.Left = btnReports.Left;
        }
    }
}
