using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibraryManagementSystem.model;
using LibraryManagementSystem.core;
using Newtonsoft.Json;

namespace LibraryManagementSystem.controller.book
{
    internal class BorrowController
    {
        // Use the centralized DataPathHelper for consistent file path resolution
        private readonly string filePath;
        private readonly string rejectFilePath;
        private const int BorrowDurationDays = 1;

        public BorrowController()
        {
            // All data files are now in: bin\Debug\Data\
            filePath = LibraryManagementSystem.core.DataPathHelper.GetDataFilePath("borrow.json");
            rejectFilePath = LibraryManagementSystem.core.DataPathHelper.GetDataFilePath("reject.json");
        }

        private List<model.Borrow> LoadBorrows()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return new List<Borrow>();
            }
            var json = System.IO.File.ReadAllText(filePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<model.Borrow>>(json) ?? new List<model.Borrow>();
        }


        private void SaveBorrows(List<model.Borrow> borrows)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(borrows, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }

        private List<model.Reject> LoadRejectedBorrows()
        {
            if (!System.IO.File.Exists(rejectFilePath))
            {
                return new List<Reject>();
            }
            var json = System.IO.File.ReadAllText(rejectFilePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<model.Reject>>(json) ?? new List<model.Reject>();
        }

        private void SaveRejectedBorrows(List<model.Reject> rejects)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(rejects, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(rejectFilePath, json);
        }

        private void AddStudentNotification(string studentId, string type, string message)
        {
            try
            {
                // Use centralized Data directory for notifications
                string notificationsBaseFolder = LibraryManagementSystem.core.DataPathHelper.EnsureDataSubdirectory("notifications");

                // Try to find the correct student folder
                string[] possibleFolders = new[]
                {
                    studentId,  // Try exact match first
                    studentId.Replace(" ", "_"),  // Try with underscores
                    studentId.ToLower(),  // Try lowercase
                    studentId.ToLower().Replace(" ", "_")  // Try lowercase with underscores
                };

                string studentFolderPath = null;

                // Check which folder exists
                foreach (var folder in possibleFolders)
                {
                    string potentialPath = System.IO.Path.Combine(notificationsBaseFolder, folder);
                    if (System.IO.Directory.Exists(potentialPath))
                    {
                        studentFolderPath = potentialPath;
                        break;
                    }
                }

                // If no folder found, create one with the studentId as provided
                if (studentFolderPath == null)
                {
                    studentFolderPath = System.IO.Path.Combine(notificationsBaseFolder, studentId);
                }

                if (!System.IO.Directory.Exists(studentFolderPath))
                {
                    System.IO.Directory.CreateDirectory(studentFolderPath);
                }

                string notificationsFile = System.IO.Path.Combine(studentFolderPath, "notifications.json");
                List<Notification> notifications = new List<Notification>();

                if (System.IO.File.Exists(notificationsFile))
                {
                    var json = System.IO.File.ReadAllText(notificationsFile);
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
                System.IO.File.WriteAllText(notificationsFile, updatedJson);

                Console.WriteLine($"Notification added for student {studentId} at path {studentFolderPath}: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student notification: {ex.Message}");
            }
        }

        public List<Borrow> GetAllBorrows()
        {
            return LoadBorrows();
        }

        public List<Borrow> GetOverdueBorrows()
        {
            var borrows = LoadBorrows();
            return borrows.Where(IsOverdue).ToList();
        }

        public bool IsBorrowOverdue(Borrow borrow)
        {
            return IsOverdue(borrow);
        }

        public List<Reject> GetAllRejectedBorrows()
        {
            return LoadRejectedBorrows();
        }

        public bool DeleteRejectedBorrow(Guid rejectId)
        {
            try
            {
                var rejects = LoadRejectedBorrows();
                var reject = rejects.FirstOrDefault(r => r.Id == rejectId);
                if (reject != null)
                {
                    rejects.Remove(reject);
                    SaveRejectedBorrows(rejects);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting rejected borrow: {ex.Message}");
                return false;
            }
        }

        public bool CreateBorrowRequest(string studentName, string bookTitle)
        {
            try
            {
                var borrows = LoadBorrows();
                var newBorrow = new Borrow
                {
                    Id = Guid.NewGuid(),
                    StudentName = studentName,
                    BookRequested = bookTitle,
                    RequestDate = DateTime.Now,
                    Status = LibraryManagementSystem.enumerator.BorrowStatus.Pending
                };
                borrows.Add(newBorrow);
                SaveBorrows(borrows);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating borrow request: {ex.Message}");
                return false;
            }
        }

        public bool ApproveBorrowRequest(Guid borrowId)
        {
            try
            {
                var borrows = LoadBorrows();
                var borrow = borrows.FirstOrDefault(b => b.Id == borrowId);
                if (borrow != null)
                {
                    // Decrease book copies by 1
                    BookController bookController = new BookController();
                    bool copiesUpdated = bookController.UpdateBookCopies(borrow.BookRequested, -1);
                    
                    if (!copiesUpdated)
                    {
                        Console.WriteLine($"Warning: Could not update book copies for '{borrow.BookRequested}'");
                        return false;
                    }

                    borrow.Status = LibraryManagementSystem.enumerator.BorrowStatus.Borrowed;
                    SaveBorrows(borrows);
                    
                    // Add approval notification to student
                    DateTime dueDate = DateTime.Now.AddDays(1);
                    string notificationMessage = $"Your request for '{borrow.BookRequested}' has been approved! You have 24 hours to pick it up. Due date: {dueDate:yyyy-MM-dd HH:mm}";
                    AddStudentNotification(borrow.StudentName, "Approved", notificationMessage);
                    
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error approving borrow request: {ex.Message}");
                return false;
            }
        }

        public bool RejectBorrowRequest(Guid borrowId)
        {
            try
            {
                var borrows = LoadBorrows();
                var borrow = borrows.FirstOrDefault(b => b.Id == borrowId);
                if (borrow != null)
                {
                    var rejects = LoadRejectedBorrows();
                    rejects.Add(new Reject
                    {
                        Id = borrow.Id,
                        StudentName = borrow.StudentName,
                        BookRequested = borrow.BookRequested,
                        RequestDate = borrow.RequestDate,
                        Status = "Rejected"
                    });
                    SaveRejectedBorrows(rejects);
                    borrows.Remove(borrow);
                    SaveBorrows(borrows);
                    
                    // Add rejection notification to student
                    string notificationMessage = $"Your request for '{borrow.BookRequested}' has been rejected by the librarian. Please contact the library for more information.";
                    AddStudentNotification(borrow.StudentName, "Rejected", notificationMessage);
                    
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error rejecting borrow request: {ex.Message}");
                return false;
            }
        }

        private bool IsOverdue(Borrow borrow)
        {
            if (borrow == null)
                return false;

            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Returned)
                return false;

            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Returning)
                return false;

            if (borrow.Status == LibraryManagementSystem.enumerator.BorrowStatus.Pending)
                return false;

            DateTime dueDate = borrow.RequestDate.AddDays(BorrowDurationDays);
            return DateTime.Now > dueDate;
        }

        public bool DeleteBorrowRecord(Guid borrowId)
        {
            try
            {
                var borrows = LoadBorrows();
                var borrow = borrows.FirstOrDefault(b => b.Id == borrowId);
                
                if (borrow == null)
                {
                    return false;
                }

                // Only allow deletion of returned books
                if (borrow.Status != LibraryManagementSystem.enumerator.BorrowStatus.Returned)
                {
                    return false;
                }

                borrows.Remove(borrow);
                SaveBorrows(borrows);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting borrow record: {ex.Message}");
                return false;
            }
        }

    }
}
