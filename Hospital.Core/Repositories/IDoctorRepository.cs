﻿using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Repositories
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<List<Doctor>> GetDoctorsWithAppointments();
    }
}