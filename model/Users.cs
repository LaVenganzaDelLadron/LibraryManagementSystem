using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class Users
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public string Department { get; set; }

        public Users() { }

        public Users(string userName, string email, string password)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            Password = password;
        }

        public Users(Guid id, string firstName, string lastName, string userName, string email, string password, string contactNo, string department)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Password = password;
            ContactNo = contactNo;
            Department = department;
        }

    }
}
