using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LibraryManagementSystem.model;

namespace LibraryManagementSystem.controller.admin
{
    public class DisplayProfile
    {
        private string usersFilePath;

        public DisplayProfile()
        {
            usersFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json");
        }

        /// <summary>
        /// Gets user information by username
        /// </summary>
        public Users GetUserByUsername(string username)
        {
            try
            {
                if (!File.Exists(usersFilePath))
                {
                    Console.WriteLine("Users file not found");
                    return null;
                }

                string json = File.ReadAllText(usersFilePath);
                var users = JsonConvert.DeserializeObject<List<Users>>(json);

                if (users == null || users.Count == 0)
                    return null;

                return users.FirstOrDefault(u => 
                    u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user profile: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets user information by user ID
        /// </summary>
        public Users GetUserById(string userId)
        {
            try
            {
                if (!File.Exists(usersFilePath))
                {
                    Console.WriteLine("Users file not found");
                    return null;
                }

                string json = File.ReadAllText(usersFilePath);
                var users = JsonConvert.DeserializeObject<List<Users>>(json);

                if (users == null || users.Count == 0)
                    return null;

                return users.FirstOrDefault(u => u.Id.ToString() == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user profile: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Updates user profile information
        /// </summary>
        public bool UpdateUserProfile(Users updatedUser)
        {
            try
            {
                if (!File.Exists(usersFilePath))
                    return false;

                string json = File.ReadAllText(usersFilePath);
                var users = JsonConvert.DeserializeObject<List<Users>>(json);

                if (users == null)
                    return false;

                var userIndex = users.FindIndex(u => u.Id == updatedUser.Id);
                if (userIndex == -1)
                    return false;

                users[userIndex] = updatedUser;

                string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(usersFilePath, updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user profile: {ex.Message}");
                return false;
            }
        }
    }
}
