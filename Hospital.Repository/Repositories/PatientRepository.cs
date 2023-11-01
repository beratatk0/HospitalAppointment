using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Hospital.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repo.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<bool> GetLogin(PatientLoginDto loginDto)
        {
            bool login = await _context.Patients.AnyAsync(x => x.Username == loginDto.Username && x.Password == loginDto.Password);
            return login;
        }

        public async Task<List<Patient>> GetPatientsWithAppointments()
        {
            return await _context.Patients.Include(x => x.Appointments).ToListAsync();
        }
    }
}
