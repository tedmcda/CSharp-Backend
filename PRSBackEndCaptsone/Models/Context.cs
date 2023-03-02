using Microsoft.EntityFrameworkCore;

namespace PRSBackEndCaptsone.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLine> RequestLines { get; set; }

        public Context(DbContextOptions<Context> options ) : base( options )
        {
        }

     }
}
