using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { Database.EnsureCreated(); }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}
