using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class AppointmentCreateDto
    {
        public string PatientUsername { get; set; }
        public string DoctorName { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
