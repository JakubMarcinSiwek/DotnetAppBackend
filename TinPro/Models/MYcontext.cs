using Microsoft.EntityFrameworkCore;

namespace TinPro.Models;

public class MYcontext : DbContext
{
    public MYcontext()
    {
    }

    public MYcontext(DbContextOptions<MYcontext> options) : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Bee> Bees { get; set; }
    public virtual DbSet<Place> Places { get; set; }
    public virtual DbSet<Trip> Trips { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Role>().HasKey(pm =>  pm.Id_Role );

        modelBuilder.Entity<Place>().HasKey(place => place.Id_Place);
        modelBuilder.Entity<Bee>().HasKey(bee => bee.Id_Bee);
        modelBuilder.Entity<Trip>().HasKey(trip => trip.Id_Trip);

        modelBuilder.Entity<Bee>()
            .HasMany(e => e.Trips)
            .WithMany(e => e.Bees)
            .UsingEntity(
                "BeesOnTrip",
                l => l.HasOne(typeof(Trip)).WithMany().HasForeignKey("Id_Trip").HasPrincipalKey(nameof(Trip.Id_Trip)),
                r => r.HasOne(typeof(Bee)).WithMany().HasForeignKey("Id_Bee").HasPrincipalKey(nameof(Bee.Id_Bee)),
                j => j.HasKey("Id_Bee", "Id_Trip"));
        
        modelBuilder.Entity<Trip>()
            .HasMany(e => e.Places)
            .WithMany(e => e.Trips)
            .UsingEntity(
                "PlacesOnTrip",
                l => l.HasOne(typeof(Place)).WithMany().HasForeignKey("Id_Place").HasPrincipalKey(nameof(Place.Id_Place)),
                r => r.HasOne(typeof(Trip)).WithMany().HasForeignKey("Id_Trip").HasPrincipalKey(nameof(Trip.Id_Trip)),
                j => j.HasKey("Id_Trip", "Id_Place"));

        modelBuilder.Entity<Bee>().HasOne(b => b.Role).WithMany(role => role.Bees).HasForeignKey(bee => bee.Id_Role);



    }
    
    
}
