using Microsoft.EntityFrameworkCore;
using GoodShoe.Models;

namespace GoodShoe.Data
{
    public class GoodShoeContext : DbContext
    {
        public GoodShoeContext (DbContextOptions<GoodShoeContext> options)
            : base(options)
        {
        }
        
        public DbSet<Product> Product { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Size).HasColumnType("decimal(3, 1)");

                // Create index on Gender for filter
                entity.HasIndex(e => e.Gender);
            });
        }
    }
}
