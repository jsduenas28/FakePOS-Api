using Microsoft.EntityFrameworkCore;

namespace fakePOSApi.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }
        public DbSet<Kardex> Kardex { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.CodCategoria)
                .IsUnique();

            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.CodProducto)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.CodUser)
                .IsUnique();
        }
    }
}
