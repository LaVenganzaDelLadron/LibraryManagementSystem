using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.model;
using LibraryManagementSystem.core;

namespace LibraryManagementSystem.controller.student
{
    internal class StudentNotificationController
    {
        private readonly string notificationsFolder;

        public StudentNotificationController()
        {
            // Use DataPathHelper for consistent paths in Data directory
            notificationsFolder = DataPathHelper.GetDataSubdirectoryPath("notifications");
            
            // Ensure notifications folder exists
            if (!System.IO.Directory.Exists(notificationsFolder))
            {
                System.IO.Directory.CreateDirectory(notificationsFolder);
            }
        }

        private string GetStudentNotificationPath(string studentId)
        {
            return System.IO.Path.Combine(notificationsFolder, studentId);
        }

        private string GetStudentNotificationFile(string studentId)
        {
            return System.IO.Path.Combine(GetStudentNotificationPath(studentId), "notifications.json");
        }

        private List<Notification> LoadStudentNotifications(string studentId)
        {
            try
            {
                string filePath = GetStudentNotificationFile(studentId);
                if (!System.IO.File.Exists(filePath))
                {
                    return new List<Notification>();
                }
                var json = System.IO.File.ReadAllText(filePath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Notification>>(json) ?? new List<Notification>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading student notifications: {ex.Message}");
                return new List<Notification>();
            }
        }

        private void SaveStudentNotifications(string studentId, List<Notification> notifications)
        {
            try
            {
                string studentPath = GetStudentNotificationPath(studentId);
                if (!System.IO.Directory.Exists(studentPath))
                {
                    System.IO.Directory.CreateDirectory(studentPath);
                }

                string filePath = GetStudentNotificationFile(studentId);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(notifications, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving student notifications: {ex.Message}");
            }
        }

        public List<Notification> GetStudentNotifications(string studentId)
        {
            return LoadStudentNotifications(studentId);
        }

        public bool AddNotification(string studentId, string type, string message)
        {
            try
            {
                var notifications = LoadStudentNotifications(studentId);
                var newNotification = new Notification
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    Message = message,
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };
                notifications.Add(newNotification);
                SaveStudentNotifications(studentId, notifications);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding notification: {ex.Message}");
                return false;
            }
        }

        public bool ClearNotification(string studentId, Guid notificationId)
        {
            try
            {
                var notifications = LoadStudentNotifications(studentId);
                var notification = notifications.FirstOrDefault(n => n.Id == notificationId);
                if (notification != null)
                {
                    notifications.Remove(notification);
                    SaveStudentNotifications(studentId, notifications);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing notification: {ex.Message}");
                return false;
            }
        }

        public bool MarkAsRead(string studentId, Guid notificationId)
        {
            try
            {
                var notifications = LoadStudentNotifications(studentId);
                var notification = notifications.FirstOrDefault(n => n.Id == notificationId);
                if (notification != null)
                {
                    notification.IsRead = true;
                    SaveStudentNotifications(studentId, notifications);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking notification as read: {ex.Message}");
                return false;
            }
        }
    }
}
