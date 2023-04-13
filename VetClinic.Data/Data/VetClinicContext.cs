using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data.Data.Clinic;
using VetClinic.Data.Data.CMS;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Data.Data
{
    public class VetClinicContext : DbContext
    {
        public VetClinicContext(DbContextOptions<VetClinicContext> options)
            : base(options)
        {
        }
        public DbSet<News> News { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Title> Title { get; set; }
        public DbSet<ServiceGroup> ServiceGroup { get; set; }

    }
}
