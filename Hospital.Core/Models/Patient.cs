using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Models
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MailAddress { get; set; }
        public string Sex { get; set; }
        public string City { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
