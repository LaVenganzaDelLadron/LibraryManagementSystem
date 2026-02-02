using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class Reject
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string BookRequested { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = "Rejected";

        public Reject() { }
    }
}
