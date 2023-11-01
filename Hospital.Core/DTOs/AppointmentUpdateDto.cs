﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class AppointmentUpdateDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string City { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}