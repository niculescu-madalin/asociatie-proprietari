using asociatie_proprietari.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace asociatie_proprietari.Data
{
    public class ApplicationDbContext : IdentityDbContext<Propietar>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<asociatie_proprietari.Models.Apartament> Apartament { get; set; } = default!;
        public DbSet<asociatie_proprietari.Models.Propietar> Propietar { get; set; } = default!;
        public DbSet<asociatie_proprietari.Models.ApartamentPropietar> ApartamentPropietar { get; set; } = default!;
    }
}
