using ClinicManagementSystem.Configurations;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Data
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Medicines> Medicines { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<Appointment> appointments { get; set; }
        
    }
}
