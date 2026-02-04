using LibraryManagementSystem.model;
using LibraryManagementSystem.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.inheritance;

namespace LibraryManagementSystem.controller.student
{
    internal class StudentRegistration : StudentInherit
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

        // Abstract method implementation
        public override bool RegisterStudent(string firstName, string lastName, string email, string department)
        {
            string userName = $"{firstName.ToLower()}.{lastName.ToLower()}";
            string defaultPassword = "student123";
            string defaultContactNo = "N/A";
            return RegisterStudent(userName, firstName, lastName, email, defaultPassword, defaultContactNo, department);
        }

        public override List<Users> GetAllStudents()
        {
            return LoadStudents();
        }

        public override bool DeleteStudent(string username)
        {
            try
            {
                var students = LoadStudents();
                var studentToRemove = students.FirstOrDefault(s => s.UserName == username);
                
                if (studentToRemove == null)
                {
                    return false;
                }
                
                students.Remove(studentToRemove);
                SaveUsers(students);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Abstract method implementation
        public override bool UpdateStudent(string studentName, Users updatedStudent)
        {
            try
            {
                var students = LoadStudents();
                var student = students.FirstOrDefault(s => s.UserName == studentName);
                
                if (student == null)
                {
                    return false;
                }
                
                // Update student properties
                students.Remove(student);
                students.Add(updatedStudent);
                SaveUsers(students);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override Users GetStudentByName(string studentName)
        {
            var students = LoadStudents();
            return students.FirstOrDefault(s => s.UserName == studentName);
        }

    }
}
