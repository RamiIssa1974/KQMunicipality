using KQApi.Models.ProcessSharePoint.Entities;
using KQGeneral.Models.Registration;
using Microsoft.EntityFrameworkCore;

namespace KQApi.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            :base(options) 
        {
            
        }
         
        public DbSet<SqlStudent> Students { get; set; } = null!;
        public DbSet<SqlSchool> Schools { get; set; } = null!;
        public DbSet<SqlTeacher> VTeachers { get; set; } = null!;
        public DbSet<SqlRegistrationType> RegistrationTypes { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
