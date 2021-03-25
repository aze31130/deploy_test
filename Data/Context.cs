using deploy_test.Models;
using Microsoft.EntityFrameworkCore;

namespace deplot_test.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Book> Book { get; set; }
        public DbSet<Book_description> Book_Description { get; set; }
    }
}
