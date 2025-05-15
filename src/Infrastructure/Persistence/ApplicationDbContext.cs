using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<BranchMenuItem> BranchMenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurar relaciones y restricciones
            builder.Entity<Branch>()
                .HasMany(b => b.BranchMenuItems)
                .WithOne(bm => bm.Branch)
                .HasForeignKey(bm => bm.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MenuItem>()
                .HasMany(m => m.BranchMenuItems)
                .WithOne(bm => bm.MenuItem)
                .HasForeignKey(bm => bm.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MenuCategory>()
                .HasMany(c => c.MenuItems)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Branch>()
                .HasMany(b => b.Orders)
                .WithOne(o => o.Branch)
                .HasForeignKey(o => o.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MenuItem>()
                .HasMany(m => m.OrderDetails)
                .WithOne(od => od.MenuItem)
                .HasForeignKey(od => od.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar propiedades requeridas
            builder.Entity<Branch>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<MenuCategory>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<MenuItem>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(10, 2);

            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(10, 2);

            builder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasPrecision(10, 2);

            builder.Entity<OrderDetail>()
                .Property(od => od.Subtotal)
                .HasPrecision(10, 2);
        }
    }
}
