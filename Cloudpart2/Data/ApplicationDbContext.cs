using CloudPart1.Models;
using Microsoft.EntityFrameworkCore;

 

namespace CloudPart1.Data
{
    
    
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            // Register models as DbSets (tables)
            public DbSet<Venue> Venue { get; set; }
            public DbSet<Event> Event { get; set; }
            public DbSet<Booking> Booking { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configure relationships/constraints
                modelBuilder.Entity<Venue>()
                    .HasMany(v => v.Bookings)
                    .WithOne(b => b.Venue)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent venue deletion if booked

                modelBuilder.Entity<Event>()
                    .HasOne(e => e.Booking)
                    .WithOne(b => b.Event)
                    .HasForeignKey<Booking>(b => b.EventId);

            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.EventId, b.VenueId })
                .IsUnique();
        }
        }
    }


