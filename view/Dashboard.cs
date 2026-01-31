using LibraryManagementSystem.model;
using LibraryManagementSystem.controller.dashboard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using LibraryManagementSystem.view;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public partial class Dashboard : Form
    {
        private const string SearchPlaceholder = "Search here";
        private readonly System.Windows.Forms.Timer searchTimer = new System.Windows.Forms.Timer();
        private CancellationTokenSource searchCts;
        private DashboardController dashboardController = new DashboardController();

        HomePage home = new HomePage();
        BookPage book = new BookPage();
        StudentPage student = new StudentPage();
        BorrowingPage borrowing = new BorrowingPage();
        MonitoringPage monitoring = new MonitoringPage();
        ReportPage report = new ReportPage();
        LogoutPage logout = new LogoutPage();


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
            lblAdmin.Text = dashboardController.GetUsername();
            LoadForm(new HomePage());

            InitializeSearch();

            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;

            // Add click event to logo for refresh functionality
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Click += PictureBox1_Click;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void InitializeSearch()
        {
            textBoxSearch.ForeColor = Color.Gray;
            textBoxSearch.Text = SearchPlaceholder;

            listBoxSearchResults.Visible = false;
            listBoxSearchResults.Font = new Font("Microsoft Sans Serif", 9F);

            pictureBox2.Cursor = Cursors.Hand;

            searchTimer.Interval = 300;
            searchTimer.Tick += SearchTimer_Tick;

            this.Click += Dashboard_Click;
            pnlFormLoader.Click += Dashboard_Click;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            // Refresh all data by recreating page instances
            home = new HomePage();
            book = new BookPage();
            student = new StudentPage();
            borrowing = new BorrowingPage();
            report = new ReportPage();

            // Determine which page is currently loaded and reload it
            if (pnlFormLoader.Controls.Count > 0)
            {
                Form currentForm = pnlFormLoader.Controls[0] as Form;
                
                if (currentForm is HomePage)
                {
                    LoadForm(home);
                    pnlNav.Height = btnDashboard.Height;
                    pnlNav.Top = btnDashboard.Top;
                }
                else if (currentForm is BookPage)
                {
                    LoadForm(book);
                    pnlNav.Height = btnBooks.Height;
                    pnlNav.Top = btnBooks.Top;
                }
                else if (currentForm is StudentPage)
                {
                    LoadForm(student);
                    pnlNav.Height = btnStudent.Height;
                    pnlNav.Top = btnStudent.Top;
                }
                else if (currentForm is BorrowingPage)
                {
                    LoadForm(borrowing);
                    pnlNav.Height = btnBorrowing.Height;
                    pnlNav.Top = btnBorrowing.Top;
                }
                else if (currentForm is ReportPage)
                {
                    LoadForm(report);
                    pnlNav.Height = btnReports.Height;
                    pnlNav.Top = btnReports.Top;
                }
            }

            MessageBox.Show("Data refreshed successfully!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            LoadForm(student);
        }

        private void btnBorrowing_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnBorrowing.Height;
            pnlNav.Top = btnBorrowing.Top;
            pnlNav.Left = btnBorrowing.Left;
            LoadForm(borrowing);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnReports.Height;
            pnlNav.Top = btnReports.Top;
            pnlNav.Left = btnReports.Left;

            LoadForm(report);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnLogout.Height;
            pnlNav.Top = btnLogout.Top;
            pnlNav.Left = btnLogout.Left;

            logout.Show();
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            string query = textBoxSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(query) || query == SearchPlaceholder)
            {
                HideSearchResults();
                return;
            }

            await RunSearchAsync(query);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == SearchPlaceholder)
                return;

            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                HideSearchResults();
                return;
            }

            searchTimer.Stop();
            searchTimer.Start();
        }

        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == SearchPlaceholder)
            {
                textBoxSearch.Text = string.Empty;
                textBoxSearch.ForeColor = Color.Black;
            }
        }

        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                textBoxSearch.Text = SearchPlaceholder;
                textBoxSearch.ForeColor = Color.Gray;
                HideSearchResults();
            }
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (listBoxSearchResults.Visible && listBoxSearchResults.Items.Count > 0)
                {
                    listBoxSearchResults.SelectedIndex = 0;
                    OpenSelectedResult();
                }
                else
                {
                    searchTimer.Stop();
                    _ = RunSearchAsync(textBoxSearch.Text.Trim());
                }
            }
            else if (e.KeyCode == Keys.Down && listBoxSearchResults.Visible && listBoxSearchResults.Items.Count > 0)
            {
                listBoxSearchResults.Focus();
                listBoxSearchResults.SelectedIndex = 0;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string query = textBoxSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(query) || query == SearchPlaceholder)
            {
                HideSearchResults();
                return;
            }

            searchTimer.Stop();
            _ = RunSearchAsync(query);
        }

        private void listBoxSearchResults_MouseClick(object sender, MouseEventArgs e)
        {
            OpenSelectedResult();
        }

        private void listBoxSearchResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                OpenSelectedResult();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                HideSearchResults();
                textBoxSearch.Focus();
            }
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            HideSearchResults();
        }

        private async Task RunSearchAsync(string query)
        {
            try
            {
                searchCts?.Cancel();
                searchCts = new CancellationTokenSource();
                var token = searchCts.Token;

                var results = await dashboardController.PerformSearchAsync(query, token);

                if (token.IsCancellationRequested)
                    return;

                UpdateSearchResults(results);
            }
            catch
            {
                HideSearchResults();
            }
        }

        private void UpdateSearchResults(List<DashboardController.SearchResult> results)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateSearchResults(results)));
                return;
            }

            listBoxSearchResults.Items.Clear();
            foreach (var r in results)
            {
                listBoxSearchResults.Items.Add(r);
            }

            listBoxSearchResults.Visible = results.Count > 0;
            listBoxSearchResults.BringToFront();
        }

        private void OpenSelectedResult()
        {
            if (listBoxSearchResults.SelectedItem is DashboardController.SearchResult result)
            {
                if (result.Type == DashboardController.SearchResultType.None)
                {
                    return;
                }

                NavigateToResult(result);
                HideSearchResults();
            }
        }

        private void NavigateToResult(DashboardController.SearchResult result)
        {
            switch (result.Type)
            {
                case DashboardController.SearchResultType.PageHome:
                    pnlNav.Height = btnDashboard.Height;
                    pnlNav.Top = btnDashboard.Top;
                    pnlNav.Left = btnDashboard.Left;
                    LoadForm(home);
                    break;
                case DashboardController.SearchResultType.PageBooks:
                case DashboardController.SearchResultType.Book:
                    pnlNav.Height = btnBooks.Height;
                    pnlNav.Top = btnBooks.Top;
                    pnlNav.Left = btnBooks.Left;
                    LoadForm(book);
                    break;
                case DashboardController.SearchResultType.PageStudents:
                case DashboardController.SearchResultType.Student:
                    pnlNav.Height = btnStudent.Height;
                    pnlNav.Top = btnStudent.Top;
                    pnlNav.Left = btnStudent.Left;
                    LoadForm(student);
                    break;
                case DashboardController.SearchResultType.PageBorrowing:
                case DashboardController.SearchResultType.Borrow:
                    pnlNav.Height = btnBorrowing.Height;
                    pnlNav.Top = btnBorrowing.Top;
                    pnlNav.Left = btnBorrowing.Left;
                    LoadForm(borrowing);
                    break;
                case DashboardController.SearchResultType.PageReports:
                    pnlNav.Height = btnReports.Height;
                    pnlNav.Top = btnReports.Top;
                    pnlNav.Left = btnReports.Left;
                    LoadForm(report);
                    break;
            }
        }

        private void HideSearchResults()
        {
            listBoxSearchResults.Visible = false;
        }

        private void btnMonitoring_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnLogout.Height;
            pnlNav.Top = btnLogout.Top;
            pnlNav.Left = btnLogout.Left;

            LoadForm(monitoring);
        }
    }
}
