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
        private readonly DbSet<Patient> _dbSet;
        public PatientRepository(AppDbContext context) : base(context)
        {
            _dbSet = context.Set<Patient>();
        }

        public async Task<bool> GetLogin(PatientLoginDto loginDto)
        {
            return await _dbSet.AnyAsync(x => x.Username == loginDto.Username && x.Password == loginDto.Password);
        }

        public async Task<List<Patient>> GetPatientsWithAppointments()
        {
            return await _context.Patients.Include(x => x.Appointments).ToListAsync();
        }
    }
}
