using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System.IO;

namespace LibraryManagementSystem.view
{
    public partial class NotificationPage : Form
    {
        private Timer refreshTimer;
        private FileSystemWatcher notificationWatcher;
        private string notificationsBasePath;

        public NotificationPage()
        {
            InitializeComponent();
            notificationsBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "notifications");
        }

        private void InitializeFileWatcher()
        {
            try
            {
                if (!Directory.Exists(notificationsBasePath))
                {
                    Directory.CreateDirectory(notificationsBasePath);
                }

                notificationWatcher = new FileSystemWatcher(notificationsBasePath);
                notificationWatcher.Filter = "*.json";
                notificationWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
                notificationWatcher.Changed += NotificationFile_Changed;
                notificationWatcher.EnableRaisingEvents = true;

                Console.WriteLine($"File watcher initialized for: {notificationsBasePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing file watcher: {ex.Message}");
            }
        }

        private void NotificationFile_Changed(object sender, FileSystemEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    System.Threading.Thread.Sleep(500);
                    LoadAllNotifications();
                }));
            }
            else
            {
                System.Threading.Thread.Sleep(500);
                LoadAllNotifications();
            }
        }

        private void InitializeRefreshTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 3000; // Refresh every 3 seconds
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadAllNotifications();
        }

        private void NotificationPage_Load(object sender, EventArgs e)
        {
            InitializeRefreshTimer();
            InitializeFileWatcher();
            LoadAllNotifications();
        }

        private void LoadAllNotifications()
        {
            try
            {
                dgvNotifications.Rows.Clear();

                // Load all notifications from all student folders
                if (Directory.Exists(notificationsBasePath))
                {
                    var studentFolders = Directory.GetDirectories(notificationsBasePath);
                    foreach (var studentFolder in studentFolders)
                    {
                        LoadNotificationsFromFolder(studentFolder);
                    }
                }

                if (dgvNotifications.Rows.Count == 0)
                {
                    dgvNotifications.Rows.Add("Info", "No notifications at this time", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading notifications: {ex.Message}");
            }
        }

        private void LoadNotificationsFromFolder(string studentFolder)
        {
            try
            {
                string notificationsFile = Path.Combine(studentFolder, "notifications.json");
                
                if (!File.Exists(notificationsFile))
                {
                    return;
                }

                string json = File.ReadAllText(notificationsFile);
                var notifications = JsonConvert.DeserializeObject<List<Notification>>(json);

                if (notifications == null || notifications.Count == 0)
                {
                    return;
                }

                // Get student name from folder name
                string studentName = Path.GetFileName(studentFolder);

                // Group notifications by book title and type, keep only the latest
                var groupedNotifications = notifications
                    .GroupBy(n => new { BookTitle = ExtractBookTitle(n.Message), Type = n.Type })
                    .Select(g => g.OrderByDescending(n => n.CreatedDate).First())
                    .OrderByDescending(n => n.CreatedDate)
                    .ToList();

                foreach (var notification in groupedNotifications)
                {
                    string displayMessage = FormatNotificationMessage(
                        notification.Type,
                        ExtractBookTitle(notification.Message),
                        notification.Message,
                        studentName
                    );

                    int rowIndex = dgvNotifications.Rows.Add(
                        notification.Type ?? "Info",
                        displayMessage,
                        notification.CreatedDate.ToString("yyyy-MM-dd HH:mm")
                    );

                    // Color code by type
                    switch (notification.Type?.ToLower())
                    {
                        case "approved":
                            dgvNotifications.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201); // Light green
                            break;
                        case "rejected":
                            dgvNotifications.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 205, 210); // Light red
                            break;
                        case "overdue":
                            dgvNotifications.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 224); // Light orange
                            break;
                        case "returned":
                            dgvNotifications.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 240, 255); // Light blue
                            break;
                        default:
                            dgvNotifications.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Light gray
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading notifications from {studentFolder}: {ex.Message}");
            }
        }

        private string ExtractBookTitle(string message)
        {
            // Extract book title from message (between quotes)
            int start = message.IndexOf("'");
            if (start >= 0)
            {
                int end = message.IndexOf("'", start + 1);
                if (end > start)
                {
                    return message.Substring(start + 1, end - start - 1);
                }
            }
            return "Unknown";
        }

        private string FormatNotificationMessage(string type, string bookTitle, string originalMessage, string studentName)
        {
            switch (type?.ToLower())
            {
                case "approved":
                    // Extract due date if present
                    if (originalMessage.Contains("Due date:"))
                    {
                        int dueDateIndex = originalMessage.IndexOf("Due date:");
                        string dueDate = originalMessage.Substring(dueDateIndex + 10).Trim();
                        return $"✓ {studentName} - {bookTitle} - Due: {dueDate}";
                    }
                    return $"✓ {studentName} - {bookTitle} - Ready for pickup";
                    
                case "rejected":
                    return $"✗ {studentName} - {bookTitle} - Request denied";
                    
                case "overdue":
                    return $"⚠ {studentName} - {bookTitle} - Overdue";
                    
                case "returned":
                    return $"↩ {studentName} - {bookTitle} - Returned and received";
                    
                case "pending":
                    return $"⏳ {studentName} - {bookTitle} - Awaiting approval";
                    
                default:
                    return originalMessage;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }
            if (notificationWatcher != null)
            {
                notificationWatcher.EnableRaisingEvents = false;
                notificationWatcher.Dispose();
            }
        }
    }
}
