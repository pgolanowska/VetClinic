using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Data.Data.Clients;
using VetClinic.Data.Data.Clinic;
using VetClinic.Data.Data.CMS;
using VetClinic.Data.Data.Staff;

namespace VetClinic.Data.Data
{
    public class VetClinicContext : IdentityDbContext<ClientUser>
    {
        public VetClinicContext(DbContextOptions<VetClinicContext> options)
            : base(options)
        {
        }
        public DbSet<News> News { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<InfoPage> InfoPage { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Title> Title { get; set; }
        public DbSet<ServiceGroup> ServiceGroup { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<EmployeeServiceGroup> EmployeeServiceGroup { get; set; }
        public DbSet<ClinicSchedule> ClinicSchedule { get; set; }
        public DbSet<ClinicScheduleException> ClinicScheduleException { get; set; }
        public DbSet<EmployeeSchedule> EmployeeSchedule { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientUser> ClientUser { get; set; }
        public DbSet<SavedItem> SavedItem { get; set; }

    }
}
