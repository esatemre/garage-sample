namespace TheGarage.Sample.Core.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Entities;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garage>().ToTable("Garages");
            modelBuilder.Entity<GarageOwner>().ToTable("GarageOwners");
            modelBuilder.Entity<GarageDoor>().ToTable("GarageDoors");
            modelBuilder.Entity<GarageDoorUpdate>().ToTable("GarageDoorUpdates");
            //one to one relationship for Garage<->GarageOwner
            modelBuilder.Entity<GarageOwner>()
                .HasOne(a => a.Garage).WithOne(b => b.GarageOwner)
                .HasForeignKey<Garage>(e => e.GarageOwnerId);
        }

        public DbSet<Garage> Garages { get; set; }
        public DbSet<GarageOwner> GarageOwners { get; set; }
        public DbSet<GarageDoor> GarageDoors { get; set; }
        public DbSet<GarageDoorUpdate> GarageDoorUpdates { get; set; }
    }
}