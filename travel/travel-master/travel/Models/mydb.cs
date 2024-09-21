using Microsoft.EntityFrameworkCore;

namespace travel.Models
{
    public class mydb:DbContext
    {
        public mydb(DbContextOptions<mydb> options) : base(options) { }

        public DbSet<User> Users {get; set; }
        public DbSet<trip> Trips { get; set; }
        public DbSet<Itinerary> Itineries { get; set; }
    }
}
