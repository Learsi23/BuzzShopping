using Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuzzShopping.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<AddressEntity> Addresses { get; set; } = null!;
        public DbSet<CategoryEntity> Categories { get; set; } = null!;
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<OrderDetailEntity> OrderDetails { get; set; } = null!;
        public DbSet<ProductEntity> Products { get; set; } = null!;
        public DbSet<RoleEntity> Roles { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación entre User y Order
            // Un usuario puede tener muchos pedidos. Si el usuario se elimina, los pedidos también lo harán.
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación entre Product y OrderDetail
            // Un producto puede estar en muchos detalles de pedido. Si el producto se elimina, se restringe la eliminación de los detalles.
            modelBuilder.Entity<ProductEntity>()
                .HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación entre Order y OrderDetail
            // Un pedido tiene muchos detalles de pedido. Si el pedido se elimina, sus detalles también.
            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación entre Category y Product
            // Una categoría tiene muchos productos. No se puede eliminar una categoría si tiene productos asociados.
            modelBuilder.Entity<CategoryEntity>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación entre Order y ShippingAddress
            // Un pedido tiene una dirección de envío.
            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación entre Order y BillingAddress (opcional)
            // Un pedido puede tener una dirección de facturación.
            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.BillingAddress)
                .WithMany()
                .HasForeignKey(o => o.BillingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación entre User y Address
            // Un usuario tiene muchas direcciones. Si el usuario se elimina, las direcciones también.
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación entre User y Role
            // Un usuario tiene un rol. Si el rol se elimina, se restringe la eliminación de los usuarios.
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de los valores predeterminados para el enum del estado del pedido
            modelBuilder.Entity<OrderEntity>()
                .Property(o => o.Status)
                .HasConversion<string>(); // Esto permite guardar el enum como string en la base de datos

            // Configuración de la precisión para los campos decimal
            modelBuilder.Entity<ProductEntity>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<ProductEntity>().Property(p => p.SpecialOfferPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetailEntity>().Property(od => od.UnitPriceAtPurchase).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetailEntity>().Property(od => od.DiscountApplied).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetailEntity>().Property(od => od.LineTotal).HasPrecision(18, 2);
            modelBuilder.Entity<OrderEntity>().Property(o => o.Subtotal).HasPrecision(18, 2);
            modelBuilder.Entity<OrderEntity>().Property(o => o.ShippingCost).HasPrecision(18, 2);
            modelBuilder.Entity<OrderEntity>().Property(o => o.DiscountAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderEntity>().Property(o => o.Total).HasPrecision(18, 2);
        }
    }
}