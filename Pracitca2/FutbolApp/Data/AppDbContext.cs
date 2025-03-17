using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Club> Clubes { get; set; }
    public DbSet<Futbolista> Futbolistas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación uno a muchos: Un Club puede tener muchos Futbolistas
        modelBuilder.Entity<Futbolista>()
            .HasOne(f => f.Club)
            .WithMany(c => c.Futbolistas)
            .HasForeignKey(f => f.ClubId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
