using LibraryManagementSystem.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagementSystem.controller.dashboard
{
    internal class DashboardController
    {
        public string GetUsername()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");

            if (!File.Exists(path))
                return "Unknown";

            try
            {
                string json = File.ReadAllText(path);
                List<Users> users = JsonConvert.DeserializeObject<List<Users>>(json);
                return users?.FirstOrDefault()?.UserName ?? "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        public async Task<List<SearchResult>> PerformSearchAsync(string query, CancellationToken token)
        {
            try
            {
                var results = await Task.Run(() => BuildSearchResults(query, token), token);
                return results;
            }
            catch
            {
                return new List<SearchResult>();
            }
        }

        private List<SearchResult> BuildSearchResults(string query, CancellationToken token)
        {
            var results = new List<SearchResult>();
            string q = query.Trim();

            if (string.IsNullOrWhiteSpace(q))
                return results;

            AddPageMatches(results, q);

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string booksPath = Path.Combine(baseDir, "books.json");
            string studentsPath = Path.Combine(baseDir, "students.json");
            string borrowPath = Path.Combine(baseDir, "borrow.json");

            // Search Books
            if (File.Exists(booksPath))
            {
                try
                {
                    var books = JsonConvert.DeserializeObject<List<Books>>(File.ReadAllText(booksPath)) ?? new List<Books>();
                    var matches = books.Where(b =>
                        (b.Title ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (b.Author ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (b.Category ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
                    ).Take(6);

                    foreach (var b in matches)
                    {
                        if (token.IsCancellationRequested) return results;
                        results.Add(new SearchResult
                        {
                            Type = SearchResultType.Book,
                            Title = b.Title ?? "Untitled Book",
                            Subtitle = $"{b.Author} • {b.Category} • Copies: {b.Copies}"
                        });
                    }
                }
                catch
                {
                    // Ignore invalid book data
                }
            }

            // Search Students
            if (File.Exists(studentsPath))
            {
                try
                {
                    var students = JsonConvert.DeserializeObject<List<Users>>(File.ReadAllText(studentsPath)) ?? new List<Users>();
                    var matches = students.Where(s =>
                        (s.UserName ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (s.Email ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (s.FirstName ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (s.LastName ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (s.Department ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
                    ).Take(6);

                    foreach (var s in matches)
                    {
                        if (token.IsCancellationRequested) return results;
                        string name = $"{s.FirstName} {s.LastName}".Trim();
                        results.Add(new SearchResult
                        {
                            Type = SearchResultType.Student,
                            Title = string.IsNullOrWhiteSpace(name) ? s.UserName : name,
                            Subtitle = $"{s.Email} • {s.Status}"
                        });
                    }
                }
                catch
                {
                    // Ignore invalid student data
                }
            }

            // Search Borrows
            if (File.Exists(borrowPath))
            {
                try
                {
                    var borrows = JsonConvert.DeserializeObject<List<Borrow>>(File.ReadAllText(borrowPath)) ?? new List<Borrow>();
                    var matches = borrows.Where(b =>
                        (b.StudentName ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (b.BookRequested ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        b.Status.ToString().IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
                    ).Take(6);

                    foreach (var b in matches)
                    {
                        if (token.IsCancellationRequested) return results;
                        results.Add(new SearchResult
                        {
                            Type = SearchResultType.Borrow,
                            Title = $"{b.StudentName} - {b.BookRequested}",
                            Subtitle = $"Status: {b.Status} • Date: {b.RequestDate:MMM dd, yyyy}"
                        });
                    }
                }
                catch
                {
                    // Ignore invalid borrow data
                }
            }

            if (results.Count == 0)
            {
                results.Add(new SearchResult
                {
                    Type = SearchResultType.None,
                    Title = "No results found",
                    Subtitle = "Try a different keyword"
                });
            }

            return results;
        }

        private void AddPageMatches(List<SearchResult> results, string query)
        {
            if (Matches(query, "dashboard", "home"))
            {
                results.Add(new SearchResult { Type = SearchResultType.PageHome, Title = "Dashboard", Subtitle = "Go to dashboard overview" });
            }
            if (Matches(query, "book", "books", "catalog", "library"))
            {
                results.Add(new SearchResult { Type = SearchResultType.PageBooks, Title = "Books", Subtitle = "Go to books management" });
            }
            if (Matches(query, "student", "students", "user", "users"))
            {
                results.Add(new SearchResult { Type = SearchResultType.PageStudents, Title = "Students", Subtitle = "Go to student management" });
            }
            if (Matches(query, "borrow", "borrowing", "request", "requests", "overdue"))
            {
                results.Add(new SearchResult { Type = SearchResultType.PageBorrowing, Title = "Borrowing", Subtitle = "Go to borrowing requests" });
            }
            if (Matches(query, "report", "reports", "penalty", "penalties", "analytics"))
            {
                results.Add(new SearchResult { Type = SearchResultType.PageReports, Title = "Reports", Subtitle = "Go to reports & penalties" });
            }
        }

        private bool Matches(string query, params string[] keywords)
        {
            return keywords.Any(k => k.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                     query.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public enum SearchResultType
        {
            None,
            PageHome,
            PageBooks,
            PageStudents,
            PageBorrowing,
            PageReports,
            Book,
            Student,
            Borrow
        }

        public class SearchResult
        {
            public SearchResultType Type { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }

            public override string ToString()
            {
                return string.IsNullOrWhiteSpace(Subtitle) ? Title : $"{Title}  -  {Subtitle}";
            }
        }
    }
}
