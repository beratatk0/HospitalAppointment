using Hospital.Core.DTOs;
using Hospital.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Repositories
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<List<Patient>> GetPatientsWithAppointments();
        Task<bool> GetLogin(PatientLoginDto loginDto);
    }
}
