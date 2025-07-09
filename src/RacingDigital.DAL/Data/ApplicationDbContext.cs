using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RacingDigital.DAL.Models;

namespace RacingDigital.DAL;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<RaceResult> RaceResults { get; set; }
    public DbSet<Racecourse> Racecourses { get; set; }
    public DbSet<Horse> Horses { get; set; }
    public DbSet<Jockey> Jockeys { get; set; }
    public DbSet<Note> Notes { get; set; }

}
