using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagementSystem.core;
using LibraryManagementSystem.model;
using Newtonsoft.Json;

namespace LibraryManagementSystem.controller.admin
{
    internal class ChangePasswordController
    {
        private static readonly string usersFilePath = DataPathHelper.GetDataFilePath("users.json");

        /// <summary>
        /// Change user password after verifying current password
        /// </summary>
        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || 
                string.IsNullOrWhiteSpace(currentPassword) || 
                string.IsNullOrWhiteSpace(newPassword))
            {
                return false;
            }

            try
            {
                // Load users
                if (!File.Exists(usersFilePath))
                {
                    return false;
                }

                string json = File.ReadAllText(usersFilePath);
                List<Users> users = JsonConvert.DeserializeObject<List<Users>>(json);

                if (users == null)
                {
                    return false;
                }

                // Find user by username (case-insensitive)
                var user = users.FirstOrDefault(u => 
                    u.UserName != null && u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    return false;
                }

                // Verify current password
                string currentPasswordHash = PasswordHelper.HashPassword(currentPassword);
                if (user.Password != currentPasswordHash)
                {
                    return false;
                }

                // Update to new password
                user.Password = PasswordHelper.HashPassword(newPassword);

                // Save back to file
                string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(usersFilePath, updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing password: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Verify if a password matches the user's current password
        /// </summary>
        public bool VerifyPassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            try
            {
                if (!File.Exists(usersFilePath))
                {
                    return false;
                }

                string json = File.ReadAllText(usersFilePath);
                List<Users> users = JsonConvert.DeserializeObject<List<Users>>(json);

                if (users == null)
                {
                    return false;
                }

                var user = users.FirstOrDefault(u => 
                    u.UserName != null && u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    return false;
                }

                string passwordHash = PasswordHelper.HashPassword(password);
                return user.Password == passwordHash;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verifying password: {ex.Message}");
                return false;
            }
        }
    }
}
