using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibraryManagementSystem.model;
using LibraryManagementSystem.core;
using Newtonsoft.Json;

namespace LibraryManagementSystem.controller.report
{
    internal class ReturnedController
    {
        private readonly string filePath;

        public ReturnedController()
        {
            // All data files are in: bin\Debug\Data\
            filePath = LibraryManagementSystem.core.DataPathHelper.GetDataFilePath("returned.json");
        }

        private List<Borrow> LoadReturnedBooks()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return new List<Borrow>();
            }
            var json = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Borrow>>(json) ?? new List<Borrow>();
        }

        private void SaveReturnedBooks(List<Borrow> borrows)
        {
            string json = JsonConvert.SerializeObject(borrows, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }

        public List<Borrow> GetAllReturnedBooks()
        {
            return LoadReturnedBooks();
        }

        public bool MoveReturnedBook(Borrow borrow)
        {
            try
            {
                if (borrow == null)
                    return false;

                var returnedBooks = LoadReturnedBooks();
                returnedBooks.Add(borrow);
                SaveReturnedBooks(returnedBooks);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving returned book: {ex.Message}");
                return false;
            }
        }

        public bool DeleteReturnedBook(Guid borrowId)
        {
            try
            {
                var returnedBooks = LoadReturnedBooks();
                var book = returnedBooks.FirstOrDefault(b => b.Id == borrowId);

                if (book == null)
                {
                    return false;
                }

                returnedBooks.Remove(book);
                SaveReturnedBooks(returnedBooks);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting returned book: {ex.Message}");
                return false;
            }
        }
    }
}

