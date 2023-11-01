﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.DTOs
{
    public class DoctorDto : BaseDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
    }
}
