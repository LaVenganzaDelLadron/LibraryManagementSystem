using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.model;


namespace LibraryManagementSystem.controller.book
{
    internal class BookService
    {
        private static readonly string filePath = "books.json";

        private List<Books> LoadBooks()
        {
            if (!System.IO.File.Exists(filePath))
            {
                return new List<Books>();
            }
            var json = System.IO.File.ReadAllText(filePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Books>>(json) ?? new List<Books>();
        }

        private void SaveBooks(List<Books> books)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(books, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }

        public bool AddBook(string title, string author, DateTime publishedDate, string description, string categoryId, int copies) { 
            
            var books = LoadBooks();
            books.Add(new Books(title, author, publishedDate, description, categoryId, copies));
            SaveBooks(books);
            return true;
        }

        public List<Books> GetAllBooks()
        {
            return LoadBooks();
        }

        public bool DeleteBook(Guid id)
        {
            var books = LoadBooks();
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return false;
            }

            books.Remove(book);
            SaveBooks(books);
            return true;
        }

    }
}
