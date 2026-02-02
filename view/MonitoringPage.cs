using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LibraryManagementSystem.controller.monitoring;

namespace LibraryManagementSystem.view
{
    public partial class MonitoringPage : Form
    {
        private readonly System.Windows.Forms.Timer countdownTimer = new System.Windows.Forms.Timer();
        private MonitoringController monitoringController;
        private List<MonitoringRow> currentRows = new List<MonitoringRow>();
        private string currentFilter = "All";

        public MonitoringPage()
        {
            InitializeComponent();
            monitoringController = new MonitoringController();
            monitoringController.OnDataChanged += OnDataChanged;
            InitializeCountdownTimer();
            LoadMonitoringData();
        }

        private void InitializeCountdownTimer()
        {
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            UpdateCountdowns();
        }

        private void OnDataChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(LoadMonitoringData));
            }
            else
            {
                LoadMonitoringData();
            }
        }

        private void LoadMonitoringData()
        {
            monitoringController.LoadBorrowData();
            RenderRows();
        }

        private void RenderRows()
        {
            dgvMonitoring.Rows.Clear();
            currentRows = monitoringController.GetMonitoringRows(currentFilter);

            if (currentRows.Count == 0)
            {
                dgvMonitoring.Rows.Add("No records", "-", "-", "-", "-");
                return;
            }

            foreach (var row in currentRows)
            {
                int rowIndex = dgvMonitoring.Rows.Add(
                    row.StudentName,
                    row.BookRequested,
                    row.Status,
                    row.Countdown,
                    row.Fine
                );

                dgvMonitoring.Rows[rowIndex].Tag = row.DueDate;
            }
        }

        private void UpdateCountdowns()
        {
            if (currentRows.Count == 0)
                return;

            monitoringController.UpdateCountdowns(currentRows);

            for (int i = 0; i < currentRows.Count && i < dgvMonitoring.Rows.Count; i++)
            {
                dgvMonitoring.Rows[i].Cells["colCountdown"].Value = currentRows[i].Countdown;
                dgvMonitoring.Rows[i].Cells["colFine"].Value = currentRows[i].Fine;
                dgvMonitoring.Rows[i].Cells["colStatus"].Value = currentRows[i].Status;
            }
        }

        private void dgvMonitoring_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dgvMonitoring.Columns["colStatus"].Index)
            {
                string status = e.Value?.ToString() ?? "";
                if (status.IndexOf("Pending", StringComparison.OrdinalIgnoreCase) >= 0)
                    e.CellStyle.ForeColor = Color.FromArgb(241, 196, 15);
                else if (status.IndexOf("Borrowed", StringComparison.OrdinalIgnoreCase) >= 0)
                    e.CellStyle.ForeColor = Color.FromArgb(155, 89, 182);
                else if (status.IndexOf("Returned", StringComparison.OrdinalIgnoreCase) >= 0)
                    e.CellStyle.ForeColor = Color.FromArgb(46, 204, 113);
                else if (status.IndexOf("Overdue", StringComparison.OrdinalIgnoreCase) >= 0)
                    e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else if (e.ColumnIndex == dgvMonitoring.Columns["colFine"].Index)
            {
                string fine = e.Value?.ToString() ?? "";
                if (!fine.Equals("₱ 0") && !fine.Equals("-"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            currentFilter = "All";
            RenderRows();
        }

        private void btnBorrowed_Click(object sender, EventArgs e)
        {
            currentFilter = "Borrowed";
            RenderRows();
        }

        private void btnOverdue_Click(object sender, EventArgs e)
        {
            currentFilter = "Overdue";
            RenderRows();
        }

        private void btnReturned_Click(object sender, EventArgs e)
        {
            currentFilter = "Returned";
            RenderRows();
        }

        private void btnPending_Click(object sender, EventArgs e)
        {
            currentFilter = "Pending";
            RenderRows();
        }
    }
}

