using Hospital.Core.DTOs;
using Hospital.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repo
{
    public class AppDbContext : IdentityDbContext<PatientIdentity>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateChangesTracker();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateChangesTracker();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void UpdateChangesTracker()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityRef)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entityRef.UpdatedDate = DateTime.Now;
                            break;
                        case EntityState.Added:
                            entityRef.CreatedDate = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}
