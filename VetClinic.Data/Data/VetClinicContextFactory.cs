using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Data.Data
{
    public class VetClinicContextFactory : IDesignTimeDbContextFactory<VetClinicContext>
    {
        public VetClinicContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VetClinicContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=VetClinicContext23;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new VetClinicContext(optionsBuilder.Options);
        }

    }
}
