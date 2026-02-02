using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class BorrowRequest
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected

        public BorrowRequest() { }
    }
}
