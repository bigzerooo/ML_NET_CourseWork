using Microsoft.EntityFrameworkCore;
using Trips.Data.Entities;

namespace Trips.Data.Context
{
    public class TripDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public TripDbContext(DbContextOptions<TripDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Trips);

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Ratings)
                .WithOne(r => r.Trip)
                .HasForeignKey(r => r.TripId);


            modelBuilder.Entity<User>()
                .HasMany(u => u.LeavedComments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Ratings)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasKey(r => new { r.TripId, r.UserId });
        }
    }
}
