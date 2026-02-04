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
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _email;
        private string _password;
        private string _contactNo;
        private string _department;
        private DateTime _joinDate;
        private UserStatus _status;

        public Guid Id 
        { 
            get { return _id; }
            set { _id = value; }
        }

        public string FirstName 
        { 
            get { return _firstName ?? string.Empty; }
            set { _firstName = value; }
        }

        public string LastName 
        { 
            get { return _lastName ?? string.Empty; }
            set { _lastName = value; }
        }

        public string UserName 
        { 
            get { return _userName ?? string.Empty; }
            set { _userName = value; }
        }

        public string Email 
        { 
            get { return _email ?? string.Empty; }
            set { _email = value; }
        }

        public string Password 
        { 
            get { return _password ?? string.Empty; }
            set { _password = value; }
        }

        public string ContactNo 
        { 
            get { return _contactNo ?? string.Empty; }
            set { _contactNo = value; }
        }

        public string Department 
        { 
            get { return _department ?? string.Empty; }
            set { _department = value; }
        }

        public DateTime JoinDate 
        { 
            get { return _joinDate; }
            set { _joinDate = value; }
        }

        public UserStatus Status 
        { 
            get { return _status; }
            set { _status = value; }
        }


        public Users() 
        {
            _id = Guid.Empty;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _userName = string.Empty;
            _email = string.Empty;
            _password = string.Empty;
            _contactNo = string.Empty;
            _department = string.Empty;
            _joinDate = DateTime.Now;
            _status = UserStatus.Active;
        }

        public Users(string userName, string email, string password)
        {
            _id = Guid.NewGuid();
            _userName = userName;
            _email = email;
            _password = password;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _contactNo = string.Empty;
            _department = string.Empty;
            _joinDate = DateTime.Now;
            _status = UserStatus.Active;
        }


        public Users(string userName, string firstName, string lastName, string email, string password, string contactNo, string department, UserStatus status = UserStatus.Active) 
        {   
            _id = Guid.NewGuid();
            _userName = userName;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _password = password;
            _contactNo = contactNo;
            _department = department;
            _joinDate = DateTime.Now;
            _status = status;
        }
        
        public Users(Guid id, string firstName, string lastName, string userName, string email, string password, string contactNo, string department)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _userName = userName;
            _email = email;
            _password = password;
            _contactNo = contactNo;
            _department = department;
            _joinDate = DateTime.Now;
            _status = UserStatus.Active;
        }
    }
}
