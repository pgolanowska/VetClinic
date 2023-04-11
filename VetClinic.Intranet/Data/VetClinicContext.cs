using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetClinic.Intranet.Models.CMS;
using VetClinic.Intranet.Models.Staff;
using VetClinic.Intranet.Models.Clinic;

namespace VetClinic.Intranet.Data
{
    public class VetClinicContext : DbContext
    {
        public VetClinicContext (DbContextOptions<VetClinicContext> options)
            : base(options)
        {
        }

        public DbSet<VetClinic.Intranet.Models.CMS.News> News { get; set; } = default!;

        public DbSet<VetClinic.Intranet.Models.Staff.Employee>? Employee { get; set; }

        public DbSet<VetClinic.Intranet.Models.Staff.Position>? Position { get; set; }

        public DbSet<VetClinic.Intranet.Models.Clinic.ServiceGroup>? ServiceGroup { get; set; }

        public DbSet<VetClinic.Intranet.Models.Staff.Title>? Title { get; set; }
    }
}
