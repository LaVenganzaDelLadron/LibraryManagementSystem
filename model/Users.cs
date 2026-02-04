using LibraryManagementSystem.enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LibraryManagementSystem.model
{
    public class Users
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public string Department { get; set; }
        public DateTime JoinDate { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Active;


        public Users() { }

        public Users(string userName, string email, string password)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            Password = password;
        }


        public Users(string userName, string firstName, string lastName, string email, string password, String contactNo, string department, UserStatus status = UserStatus.Active) 
        {   
            Id = Guid.NewGuid() ;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ContactNo = contactNo;
            Department = department;
            JoinDate = DateTime.Now;
            Status = status;
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
