using Microsoft.EntityFrameworkCore;
using odiazon.models.c_cart;
using odiazon.models.m_category;
using odiazon.models.m_order;
using odiazon.models.m_product;
using odiazon.models.m_productOrder;
using odiazon.models.m_user;

namespace odiazon.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<ProductOrder>? ProductOrders { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Cart>? Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many to Many
            modelBuilder.Entity<ProductOrder>()
                .HasKey(ct => new { ct.ProductId, ct.OrderId });

            // make an index unique
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.Reference).IsUnique();
        }
    }
}
