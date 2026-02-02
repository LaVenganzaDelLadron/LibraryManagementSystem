using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.enumerator
{
    internal enum BorrowStatus
    {
        Pending = 0,
        Borrowed = 1,
        Returning = 2,
        Returned = 3,
        Overdue = 4,
        Lost = 5
    }
}
