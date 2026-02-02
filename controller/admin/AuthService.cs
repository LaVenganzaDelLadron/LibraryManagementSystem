using System;
using System.Collections.Generic;
using LibraryManagementSystem.model;
using System.IO;
using LibraryManagementSystem.core;
using Newtonsoft.Json;
using System.Linq;

namespace LibraryManagementSystem.controller
{
    internal class AuthService
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("users.json");

        private List<Users> LoadUsers()
        {
            if (!File.Exists(filePath))
            {
                return new List<Users>();
            }
            var json = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Users>>(json) ?? new List<Users>();
        }

        private void SaveUsers(List<Users> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }


        public bool Signup(string username, string email, string password)
        {
            var users = LoadUsers();

            if (users.Any(u => u.UserName == username || u.Email == email))
                return false;

            string hash = PasswordHelper.HashPassword(password);
            users.Add(new Users(username, email, hash));

            SaveUsers(users);
            return true;
        }


        public bool Login(string email, string password)
        {
            var users = LoadUsers();
            string hash = PasswordHelper.HashPassword(password);

            return users.Any(u => u.Email == email && u.Password == hash);
        }

    }
}
