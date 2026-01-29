using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class Books
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Copies { get; set; }
    
    public Books() { }


        public Books(string title, string author, DateTime publishedDate, string description, string category, int copies)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            PublishedDate = publishedDate;
            Description = description;
            Category = category;
            Copies = copies;
        }

        public Books(Guid id, string title, string author, DateTime publishedDate, string description, string category, int copies)
        {
            Id = id;
            Title = title;
            Author = author;
            PublishedDate = publishedDate;
            Description = description;
            Category = category;
            Copies = copies;
        }

    }
}
