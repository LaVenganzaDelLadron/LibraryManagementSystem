using LibraryManagementSystem.model;
using LibraryManagementSystem.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.controller.studentRegistration
{
    internal class StudentRegistration
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("students.json");

        private List<Users> LoadStudents()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return new List<Users>();
            }
            var json = System.IO.File.ReadAllText(filePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(json) ?? new List<Users>();
        }

        private void SaveUsers(List<Users> users)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }


        public bool RegisterStudent(string userName, string firstName, string lastName, string email, string password, String contactNo, string department)
        {
            var students = LoadStudents();
            if (students.Any(u => u.UserName == userName || u.Email == email))
            {
                return false;
            }
            string hash = PasswordHelper.HashPassword(password);
            students.Add(new Users(userName, firstName, lastName, email, hash, contactNo, department));
            SaveUsers(students);
            return true;
        }

        public List<Users> GetAllStudents()
        {
            return LoadStudents();
        }

    }
}
