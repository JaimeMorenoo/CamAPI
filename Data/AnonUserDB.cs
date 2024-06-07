using Camp.Models;
using Microsoft.EntityFrameworkCore;

namespace Camp.Data
{
    public class AnonUserDB : DbContext
    {
        public AnonUserDB(DbContextOptions<AnonUserDB> options) : base (options) { }

        public DbSet<User> users { get; set; }

        public DbSet<Spot> campingSpots { get; set; } // Here we initate the creation of the tables in the DB
        public DbSet<Booking> bookings { get; set; }

        
    }
}
