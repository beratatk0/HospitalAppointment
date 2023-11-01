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
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Doctor>> GetDoctorsWithAppointments()
        {
            return await _context.Doctors.Include(x => x.Appointments).ToListAsync();
        }
    }
}
