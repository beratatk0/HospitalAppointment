using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DoctorWithAppointmentsDto : DoctorDto
    {
        public List<AppointmentDto> Appointments { get; set; }
    }
}
