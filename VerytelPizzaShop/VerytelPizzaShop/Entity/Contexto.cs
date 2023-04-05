using Microsoft.EntityFrameworkCore;
using VerytelPizzaShop.Models;

namespace VerytelPizzaShop.Entity
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.Property(e => e.Id).HasColumnName("id_order");
                entity.Property(e => e.Address).HasColumnName("addres");
                entity.Property(e => e.Size).HasColumnName("size");
                entity.Property(e => e.Toppings).HasColumnName("toppings");
                entity.Property(e => e.Instructions).HasColumnName("instructions");
                entity.Property(e => e.CreationDate).HasColumnName("creation_date").HasDefaultValueSql("current_timestamp");
                entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue("order placed");
            });
        }
    }
}
