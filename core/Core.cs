using LibraryManagementSystem.controller.book;
using LibraryManagementSystem.core.book;
using LibraryManagementSystem.core.clientcontroller.book;
using LibraryManagementSystem.core.services.auth;
using LibraryManagementSystem.core.student;
using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace LibraryManagementSystem.core
{
    internal class Core
    {
        private const int PORT = 4000;
        private TcpListener listener;

        public Core()
        {
            listener = new TcpListener(IPAddress.Any, PORT);
        }

        public async Task StartAsync()
        {
            listener.Start();
            Console.WriteLine($"Server running on port {PORT}");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected");

                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[2048];

                    int bytesRead;
                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received: {message}");

                        // Try to parse as JSON request
                        try
                        {
                            dynamic request = JsonConvert.DeserializeObject(message);
                            string action = request.action;

                            string responseJson = "";

                            if (action == "login")
                            {
                                string email = request.email;
                                string password = request.password;

                                AuthResponse authResponse = AuthService.Login(email, password);
                                responseJson = JsonConvert.SerializeObject(authResponse);
                            }
                            else if (action == "getBooks")
                            {
                                BookResponse bookResponse = BookServices.GetAllBooks();
                                responseJson = JsonConvert.SerializeObject(bookResponse);
                            }
                            else if (action == "getCategories")
                            {
                                var categoryResponse = CategoryService.GetAllCategories();
                                responseJson = JsonConvert.SerializeObject(categoryResponse);
                            }
                            else if (action == "getBorrows")
                            {
                                var borrowResponse = BorrowService.RequestBorrow();
                                responseJson = JsonConvert.SerializeObject(borrowResponse);
                            }
                            else if (action == "getRejectedBorrows")
                            {
                                var rejectResponse = RejectService.GetRejectedBorrows();
                                responseJson = JsonConvert.SerializeObject(rejectResponse);
                            }
                            else if (action == "getStudents")
                            {
                                var studentResponse = StudentService.GetAllStudents();
                                responseJson = JsonConvert.SerializeObject(studentResponse);
                            }
                            else if (action == "createBorrow")
                            {
                                string studentName = request.studentName;
                                string bookTitle = request.bookTitle;

                                BorrowController borrowController = new BorrowController();
                                bool success = borrowController.CreateBorrowRequest(studentName, bookTitle);

                                var createBorrowResponse = new AuthResponse
                                {
                                    Status = success ? "SUCCESS" : "FAILED",
                                    Message = success ? "Borrow request created successfully" : "Failed to create borrow request"
                                };
                                responseJson = JsonConvert.SerializeObject(createBorrowResponse);
                            }
                            else if (action == "getStudentNotifications")
                            {
                                string studentId = request.studentId;

                                StudentNotificationService notificationService = new StudentNotificationService();
                                var notificationResponse = notificationService.GetStudentNotifications(studentId);
                                responseJson = JsonConvert.SerializeObject(notificationResponse);
                            }
                            else if (action == "returnBook")
                            {
                                string borrowId = request.borrowId;
                                var returnResponse = BorrowService.ReturnBook(borrowId);
                                responseJson = JsonConvert.SerializeObject(returnResponse);
                            }
                            else if (action == "markFineAsPaid")
                            {
                                string borrowId = request.borrowId;
                                var fineResponse = BorrowService.MarkFineAsPaid(borrowId);
                                responseJson = JsonConvert.SerializeObject(fineResponse);
                            }
                            else
                            {
                                var errorResponse = new AuthResponse
                                {
                                    Status = "FAILED",
                                    Message = "Unknown action"
                                };
                                responseJson = JsonConvert.SerializeObject(errorResponse);
                            }

                            byte[] response = Encoding.UTF8.GetBytes(responseJson);
                            await stream.WriteAsync(response, 0, response.Length);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Request parsing error: {ex.Message}");
                            var errorResponse = new AuthResponse
                            {
                                Status = "FAILED",
                                Message = "Invalid request"
                            };
                            string errorJson = JsonConvert.SerializeObject(errorResponse);
                            byte[] errorBytes = Encoding.UTF8.GetBytes(errorJson);
                            await stream.WriteAsync(errorBytes, 0, errorBytes.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }

            Console.WriteLine("Client disconnected");
        }
    }
}
