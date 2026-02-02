using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.model;
using LibraryManagementSystem.controller.student;

namespace LibraryManagementSystem.core.student
{
    internal class StudentNotificationService
    {
        private StudentNotificationController controller;

        public StudentNotificationService()
        {
            controller = new StudentNotificationController();
        }

        public NotificationResponse GetStudentNotifications(string studentId)
        {
            try
            {
                var notifications = controller.GetStudentNotifications(studentId);

                return new NotificationResponse
                {
                    Status = "SUCCESS",
                    Message = "Notifications retrieved successfully",
                    Notifications = notifications ?? new List<Notification>()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in StudentNotificationService: {ex.Message}");
                return new NotificationResponse
                {
                    Status = "FAILED",
                    Message = ex.Message,
                    Notifications = new List<Notification>()
                };
            }
        }
    }
}
