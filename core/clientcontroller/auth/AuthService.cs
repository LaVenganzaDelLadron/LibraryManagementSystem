using LibraryManagementSystem.controller;
using LibraryManagementSystem.enumerator;
using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibraryManagementSystem.core;

namespace LibraryManagementSystem.core.services.auth
{
    internal class AuthService
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("students.json");

        public static AuthResponse Login(string email, string password)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new AuthResponse { Status = "FAILED", Message = "Students database not found" };
                }

                var users = JsonConvert.DeserializeObject<List<Users>>(
                    File.ReadAllText(filePath)
                );

                if (users == null || users.Count == 0)
                {
                    return new AuthResponse { Status = "FAILED", Message = "No users found in database" };
                }

                var user = users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                    return new AuthResponse { Status = "FAILED", Message = "User not found" };

                string hashed = PasswordHelper.HashPassword(password);

                if (user.Password != hashed)
                    return new AuthResponse { Status = "FAILED", Message = "Wrong password" };

                if (user.Status != UserStatus.Active)
                    return new AuthResponse { Status = "FAILED", Message = "Account is banned" };

                return new AuthResponse
                {
                    Status = "SUCCESS",
                    UserId = user.Id.ToString(),
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authentication error: {ex.Message}");
                return new AuthResponse { Status = "FAILED", Message = $"Authentication error: {ex.Message}" };
            }
        }
    }
}
