using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.model;
using LibraryManagementSystem.core;

namespace LibraryManagementSystem.controller.book
{
    internal class BookController
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("books.json");

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

        public List<Books> GetBooksByCategory(string category)
        {
            var books = LoadBooks();
            if (string.IsNullOrWhiteSpace(category) || category == "All Categories")
                return books;
            
            return books.Where(b => b.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Books> SearchBooks(string query)
        {
            var books = LoadBooks();
            if (string.IsNullOrWhiteSpace(query))
                return books;

            query = query.ToLower();
            return books.Where(b =>
                (b.Title ?? "").ToLower().Contains(query) ||
                (b.Author ?? "").ToLower().Contains(query) ||
                (b.Category ?? "").ToLower().Contains(query)
            ).ToList();
        }

        public List<string> GetAllCategories()
        {
            var books = LoadBooks();
            var categories = books
                .Select(b => b.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
            
            categories.Insert(0, "All Categories");
            return categories;
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

        public bool UpdateBook(Books updatedBook)
        {
            try
            {
                var books = LoadBooks();
                var book = books.FirstOrDefault(b => b.Id == updatedBook.Id);
                if (book == null)
                {
                    return false;
                }

                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.PublishedDate = updatedBook.PublishedDate;
                book.Description = updatedBook.Description;
                book.Category = updatedBook.Category;
                book.Copies = updatedBook.Copies;

                SaveBooks(books);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book: {ex.Message}");
                return false;
            }
        }

        public bool UpdateBookCopies(string bookTitle, int change)
        {
            try
            {
                var books = LoadBooks();
                var book = books.FirstOrDefault(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
                
                if (book == null)
                {
                    return false;
                }

                // Prevent negative copies
                if (book.Copies + change < 0)
                {
                    return false;
                }

                book.Copies += change;
                SaveBooks(books);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book copies: {ex.Message}");
                return false;
            }
        }

    }
}
