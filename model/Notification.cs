using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.model
{
    internal class Notification
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
    }

    internal class NotificationResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
