using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibraryManagementSystem.core;

namespace LibraryManagementSystem.core.book
{
    internal class BookServices
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("books.json");

        public static BookResponse GetAllBooks()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new BookResponse
                    {
                        Status = "FAILED",
                        Message = $"Books database not found at {filePath}"
                    };
                }
               
                var books = JsonConvert.DeserializeObject<List<Books>>(
                    File.ReadAllText(filePath)
                );

                if (books == null || books.Count == 0)
                {
                    return new BookResponse
                    {
                        Status = "FAILED",
                        Message = "No books available"
                    };
                }

                return new BookResponse
                {
                    Status = "SUCCESS",
                    Books = books
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book service error: {ex.Message}");
                return new BookResponse
                {
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }

        public static BookResponse SearchByTitle(string title)
        {
            var books = JsonConvert.DeserializeObject<List<Books>>(
                File.ReadAllText(filePath)
            );

            var result = books
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .ToList();

            return new BookResponse
            {
                Status = "SUCCESS",
                Books = result
            };
        }
    }
}
