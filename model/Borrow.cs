using LibraryManagementSystem.enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class Borrow
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string BookRequested { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public BorrowStatus Status { get; set; } = BorrowStatus.Pending;
        public bool FinePaid { get; set; } = false;
        public bool ReceivedByLibrarian { get; set; } = false;
        public Borrow() { }


    }
}
