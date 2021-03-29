using deploy_test.Models;
using Microsoft.EntityFrameworkCore;

namespace deploy_test.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Book> books { get; set; }
        public DbSet<User> users { get; set; }
    }
}
