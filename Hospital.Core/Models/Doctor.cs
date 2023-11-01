using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Models
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }

    }
}
