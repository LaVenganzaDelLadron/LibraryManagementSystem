using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class AuthRequest
    {
        public string Action { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
