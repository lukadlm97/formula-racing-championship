using FormulaCar.Championships.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormulaCar.Championships.Persistence;

public sealed class RepositoryDbContext : DbContext
{
    public RepositoryDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<MediaTag> MediaTags { get; set; }
    public DbSet<Circuite> Circuites { get; set; }
    public DbSet<Constructor> Constructors { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Raceweek> Raceweeks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
    }
}