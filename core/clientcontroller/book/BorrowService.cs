using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.model;
using System.IO;
using Newtonsoft.Json;
using LibraryManagementSystem.core;


namespace LibraryManagementSystem.core.clientcontroller.book
{
    internal class BorrowService
    {
        private static readonly string filePath = DataPathHelper.GetDataFilePath("borrow.json");

        public static BorrowResponse RequestBorrow()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new BorrowResponse
                    {
                        Status = "FAILED",
                        Message = "Borrow database not found"
                    };
                }
                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(
                    File.ReadAllText(filePath)
                );
                if (borrows == null || borrows.Count == 0)
                {
                    return new BorrowResponse
                    {
                        Status = "FAILED",
                        Message = "No borrow records available"
                    };
                }
                return new BorrowResponse
                {
                    Status = "SUCCESS",
                    Borrows = borrows
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Borrow service error: {ex.Message}");
                return new BorrowResponse
                {
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }

        public static AuthResponse ReturnBook(string borrowId)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new AuthResponse
                    {
                        Status = "FAILED",
                        Message = "Borrow database not found"
                    };
                }

                if (!Guid.TryParse(borrowId, out Guid id))
                {
                    return new AuthResponse
                    {
                        Status = "FAILED",
                        Message = "Invalid borrow ID"
                    };
                }

                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(
                    File.ReadAllText(filePath)
                ) ?? new List<Borrow>();

                var borrow = borrows.FirstOrDefault(b => b.Id == id);
                if (borrow == null)
                {
                    return new AuthResponse
                    {
                        Status = "FAILED",
                        Message = "Borrow record not found"
                    };
                }

                borrow.Status = (LibraryManagementSystem.enumerator.BorrowStatus)2; // Returning status (awaiting librarian confirmation)
                borrow.ReturnDate = DateTime.Now;
                borrow.ReceivedByLibrarian = false;

                File.WriteAllText(filePath, JsonConvert.SerializeObject(borrows, Formatting.Indented));

                return new AuthResponse
                {
                    Status = "SUCCESS",
                    Message = "Book returned successfully"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error returning book: {ex.Message}");
                return new AuthResponse
                {
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }

        public static AuthResponse MarkFineAsPaid(string borrowId)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new AuthResponse
                    {
                        Status = "FAILED",
                        Message = "Borrow database not found"
                    };
                }

                if (!Guid.TryParse(borrowId, out Guid id))
                {
                    return new AuthResponse
                    {
                        Status = "FAILED",
                        Message = "Invalid borrow ID"
                    };
                }

                var borrows = JsonConvert.DeserializeObject<List<Borrow>>(
                    File.ReadAllText(filePath)
                ) ?? new List<Borrow>();

                var borrow = borrows.FirstOrDefault(b => b.Id == id);
                if (borrow == null)
                {
                    return new AuthResponse
                    {
                        Status = "FAILED",
                        Message = "Borrow record not found"
                    };
                }

                borrow.FinePaid = true;

                File.WriteAllText(filePath, JsonConvert.SerializeObject(borrows, Formatting.Indented));

                return new AuthResponse
                {
                    Status = "SUCCESS",
                    Message = "Fine marked as paid"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking fine as paid: {ex.Message}");
                return new AuthResponse
                {
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }
    }
}
