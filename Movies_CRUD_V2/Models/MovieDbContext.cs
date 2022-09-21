using Microsoft.EntityFrameworkCore;

namespace Movies_CRUD_V2.Models
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Movies> Movie { get; set; }
        public DbSet<Genres> Genre { get; set; }
    }
}
