using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class RejectResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<Reject> RejectedBorrows { get; set; }

        public RejectResponse()
        {
            RejectedBorrows = new List<Reject>();
        }
    }
}
