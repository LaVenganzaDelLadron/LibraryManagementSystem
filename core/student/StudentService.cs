using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibraryManagementSystem.core;


namespace LibraryManagementSystem.core.student
{
    internal class StudentService
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("students.json");

        public static StudentResponse GetAllStudents()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new StudentResponse
                    {
                        Status = "FAILED",
                        Message = "Students database not found"
                    };
                }

                var students = JsonConvert.DeserializeObject<List<Users>>(
                    File.ReadAllText(filePath)
                );

                if (students == null || students.Count == 0)
                {
                    return new StudentResponse
                    {
                        Status = "FAILED",
                        Message = "No students available"
                    };
                }

                return new StudentResponse
                {
                    Status = "SUCCESS",
                    Students = students
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Student service error: {ex.Message}");
                return new StudentResponse
                {
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }

        public static StudentResponse SearchByUsernameOrEmail(string keyword)
        {
            try
            {
                var students = JsonConvert.DeserializeObject<List<Users>>(
                    File.ReadAllText(filePath)
                );

                var result = students
                    .Where(s => s.UserName.ToLower().Contains(keyword.ToLower()) ||
                                s.Email.ToLower().Contains(keyword.ToLower()))
                    .ToList();

                return new StudentResponse
                {
                    Status = "SUCCESS",
                    Students = result
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Student search error: {ex.Message}");
                return new StudentResponse
                {
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }
    }
}
