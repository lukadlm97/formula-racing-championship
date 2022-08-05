using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaCar.Championships.Persistence
{
    public  sealed  class RepositoryDbContext:DbContext
    {
        public RepositoryDbContext(DbContextOptions contextOptions):base(contextOptions)
        {


        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Driver> Drivers { get; set; }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
    }
}
