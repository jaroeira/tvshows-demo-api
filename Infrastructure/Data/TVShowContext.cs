using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class TVShowContext : DbContext
{
    public TVShowContext(DbContextOptions<TVShowContext> options) : base(options)
    {
    }

    public DbSet<TVShow> TVShows { get; set; }
    public DbSet<TVShowGenre> TVShowGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
