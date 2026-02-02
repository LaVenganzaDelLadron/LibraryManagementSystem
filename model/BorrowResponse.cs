using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class BorrowResponse
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public List<Borrow> Borrows { get; set; }
    }
}
