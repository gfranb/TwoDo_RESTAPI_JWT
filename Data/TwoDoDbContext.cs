using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TwoDo.Models;

namespace TwoDo.Data
{
    public class TwoDoDbContext : DbContext
    {
        public TwoDoDbContext(DbContextOptions<TwoDoDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
        .UseLazyLoadingProxies()
        .UseNpgsql("Host=127.0.0.1;Port=5432;Database=twodo;Username=postgres;Password=admin");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Assignments)
                .HasForeignKey(e => e.UserId);
        }
    }
}
