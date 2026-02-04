using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System.IO;
using LibraryManagementSystem.core;
using LibraryManagementSystem.inheritance;

namespace LibraryManagementSystem.controller.notification
{
    internal class NotificationController : NotifInherit
    {
        private string notificationsBasePath;

        public NotificationController()
        {
            notificationsBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "notifications");
        }

        public List<Notification> GetAllNotifications()
        {
            List<Notification> allNotifications = new List<Notification>();

            try
            {
                if (!Directory.Exists(notificationsBasePath))
                {
                    Directory.CreateDirectory(notificationsBasePath);
                    return allNotifications;
                }

                var studentFolders = Directory.GetDirectories(notificationsBasePath);
                foreach (var studentFolder in studentFolders)
                {
                    string notificationsFile = Path.Combine(studentFolder, "notifications.json");

                    if (!File.Exists(notificationsFile))
                    {
                        continue;
                    }

                    try
                    {
                        string json = File.ReadAllText(notificationsFile);
                        var notifications = JsonConvert.DeserializeObject<List<Notification>>(json);

                        if (notifications != null && notifications.Count > 0)
                        {
                            allNotifications.AddRange(notifications);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading notifications from {studentFolder}: {ex.Message}");
                    }
                }

                // Sort by created date descending (newest first)
                allNotifications = allNotifications.OrderByDescending(n => n.CreatedDate).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all notifications: {ex.Message}");
            }

            return allNotifications;
        }

        public override dynamic GetStudentNotifications(string studentName)
        {
            List<Notification> studentNotifications = new List<Notification>();

            try
            {
                string studentFolder = Path.Combine(notificationsBasePath, studentName);
                string notificationsFile = Path.Combine(studentFolder, "notifications.json");

                if (!File.Exists(notificationsFile))
                {
                    return studentNotifications;
                }

                string json = File.ReadAllText(notificationsFile);
                var notifications = JsonConvert.DeserializeObject<List<Notification>>(json);

                if (notifications != null)
                {
                    studentNotifications = notifications.OrderByDescending(n => n.CreatedDate).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting student notifications: {ex.Message}");
            }

            return studentNotifications;
        }

        public bool SendNotification(string studentName, string type, string message)
        {
            try
            {
                string studentFolder = Path.Combine(notificationsBasePath, studentName);
                if (!Directory.Exists(studentFolder))
                {
                    Directory.CreateDirectory(studentFolder);
                }

                string notificationsFile = Path.Combine(studentFolder, "notifications.json");
                List<Notification> notifications = new List<Notification>();

                if (File.Exists(notificationsFile))
                {
                    string json = File.ReadAllText(notificationsFile);
                    notifications = JsonConvert.DeserializeObject<List<Notification>>(json) ?? new List<Notification>();
                }

                var newNotification = new Notification
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    Message = message,
                    CreatedDate = DateTime.Now,
                    IsRead = false
                };

                notifications.Add(newNotification);
                string updatedJson = JsonConvert.SerializeObject(notifications, Formatting.Indented);
                File.WriteAllText(notificationsFile, updatedJson);

                Console.WriteLine($"Notification sent to {studentName}: {message}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending notification: {ex.Message}");
                return false;
            }
        }

        // Abstract method implementation
        public override bool SendNotification(string studentName, string message, NotificationType type)
        {
            return SendNotification(studentName, type.ToString(), message);
        }

        public bool MarkAsRead(string studentName, Guid notificationId)
        {
            try
            {
                string studentFolder = Path.Combine(notificationsBasePath, studentName);
                string notificationsFile = Path.Combine(studentFolder, "notifications.json");

                if (!File.Exists(notificationsFile))
                {
                    return false;
                }

                string json = File.ReadAllText(notificationsFile);
                var notifications = JsonConvert.DeserializeObject<List<Notification>>(json) ?? new List<Notification>();

                var notification = notifications.FirstOrDefault(n => n.Id == notificationId);
                if (notification != null)
                {
                    notification.IsRead = true;
                    string updatedJson = JsonConvert.SerializeObject(notifications, Formatting.Indented);
                    File.WriteAllText(notificationsFile, updatedJson);
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

        // Abstract method implementation
        public override bool MarkAsRead(Guid notificationId)
        {
            // Cannot implement without student name context
            // This is a limitation of the current design
            throw new NotImplementedException("Use MarkAsRead(string studentName, Guid notificationId) instead.");
        }
    }
}
