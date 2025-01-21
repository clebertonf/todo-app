using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TodoModel> Todos { get; set; }

        /*
         protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // options.UseSqlite("DataSource=TodoApp.db;Cache=Shared");
        }
        */
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoModel>()
                .HasKey(t => t.Id);
            
            modelBuilder.Entity<TodoModel>()
                .Property(t => t.TodoTitle)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<TodoModel>()
                .Property(t => t.IsDone).IsRequired();
            
            modelBuilder.Entity<TodoModel>()
                .Property(t => t.CreateAt).HasColumnType("Date").IsRequired();
        }
    }
}
