using Microsoft.EntityFrameworkCore;

namespace HealthCare.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Patient>  Patients{ get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
