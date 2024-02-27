using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Models;
using System.Collections.Generic;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Match> Matches { get; set; }

    // Voeg meer DbSet<T> properties hier toe voor andere entiteiten
}
