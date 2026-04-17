using System;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetallesVenta { get; set; }
    public DbSet<Pago> Pagos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tabla Usuario
        modelBuilder.Entity<Usuario>()
            .ToTable("Usuario");
        
        modelBuilder.Entity<Usuario>()
            .HasKey(x => x.IdUsuario);

        modelBuilder.Entity<Usuario>()
            .Property(x => x.NombreCompleto)
            .HasMaxLength(100);

        modelBuilder.Entity<Usuario>()
            .Property(x => x.Correo)
            .HasMaxLength(100);

        modelBuilder.Entity<Usuario>()
            .Property(x => x.Contrasenia)
            .HasMaxLength(255);

        modelBuilder.Entity<Usuario>()
            .Property(x => x.Telefono)
            .HasMaxLength(20);

        modelBuilder.Entity<Usuario>()
            .Property(x => x.Rol)
            .HasMaxLength(30);

        // Tabla Proveedor
        modelBuilder.Entity<Proveedor>()
            .ToTable("Proveedor");

        modelBuilder.Entity<Proveedor>()
            .HasKey(x => x.IdProveedor);

        modelBuilder.Entity<Proveedor>()
            .Property(x => x.NombreCompleto)
            .HasMaxLength(100);

        modelBuilder.Entity<Proveedor>()
            .Property(x => x.Telefono)
            .HasMaxLength(20);

        modelBuilder.Entity<Proveedor>()
            .Property(x => x.Correo)
            .HasMaxLength(100);

        modelBuilder.Entity<Proveedor>()
            .Property(x => x.Direccion)
            .HasMaxLength(200);

        // Tabla Categoria
        modelBuilder.Entity<Categoria>()
            .ToTable("Categoria");

        modelBuilder.Entity<Categoria>()
            .HasKey(x => x.IdCategoria);

        modelBuilder.Entity<Categoria>()
            .Property(x => x.NombreCategoria)
            .HasMaxLength(100);

        modelBuilder.Entity<Categoria>()
            .Property(x => x.Descripcion)
            .HasMaxLength(200);

        // Tabla Producto
        modelBuilder.Entity<Producto>()
            .ToTable("Producto");

        modelBuilder.Entity<Producto>()
            .HasKey(x => x.IdProducto);

        modelBuilder.Entity<Producto>()
            .Property(x => x.NombreProducto)
            .HasMaxLength(100);

        modelBuilder.Entity<Producto>()
            .Property(x => x.Descripcion)
            .HasMaxLength(200);

        modelBuilder.Entity<Producto>()
            .Property(x => x.Marca)
            .HasMaxLength(50);

        modelBuilder.Entity<Producto>()
            .Property(x => x.Color)
            .HasMaxLength(50);

        modelBuilder.Entity<Producto>()
            .Property(x => x.Precio)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Producto>()
            .HasOne(x => x.Categoria)
            .WithMany(x => x.Productos)
            .HasForeignKey(x => x.IdCategoria);

        modelBuilder.Entity<Producto>()
            .HasOne(x => x.Proveedor)
            .WithMany(x => x.Productos)
            .HasForeignKey(x => x.IdProveedor);

        // Tabla Venta
        modelBuilder.Entity<Venta>()
            .ToTable("Venta");

        modelBuilder.Entity<Venta>()
            .HasKey(x => x.IdVenta);

        modelBuilder.Entity<Venta>()
            .Property(x => x.Total)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Venta>()
            .Property(x => x.ModalidadPago)
            .HasMaxLength(30);

        modelBuilder.Entity<Venta>()
            .Property(x => x.EstadoVenta)
            .HasMaxLength(30);

        modelBuilder.Entity<Venta>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Ventas)
            .HasForeignKey(x => x.IdUsuario);

        // Tabla DetalleVenta
        modelBuilder.Entity<DetalleVenta>()
            .ToTable("DetalleVenta");
        
        modelBuilder.Entity<DetalleVenta>()
            .HasKey(x => x.IdDetalleVenta);

        modelBuilder.Entity<DetalleVenta>()
            .Property(x => x.PrecioUnitario)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<DetalleVenta>()
            .Property(x => x.Subtotal)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<DetalleVenta>()
            .HasOne(x => x.Venta)
            .WithMany(x => x.DetallesVenta)
            .HasForeignKey(x => x.IdVenta);

        modelBuilder.Entity<DetalleVenta>()
            .HasOne(x => x.Producto)
            .WithMany(x => x.DetallesVenta)
            .HasForeignKey(x => x.IdProducto);

        // Tabla Pago
        modelBuilder.Entity<Pago>()
            .ToTable("Pago");

        modelBuilder.Entity<Pago>()
            .HasKey(x => x.IdPago);

        modelBuilder.Entity<Pago>()
            .Property(x => x.MetodoPago)
            .HasMaxLength(30);

        modelBuilder.Entity<Pago>()
            .Property(x => x.MontoPagado)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Pago>()
            .Property(x => x.SaldoPendiente)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Pago>()
            .Property(x => x.Interes)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Pago>()
            .Property(x => x.EstadoPago)
            .HasMaxLength(30);

        modelBuilder.Entity<Pago>()
            .HasOne(x => x.Venta)
            .WithMany(x => x.Pagos)
            .HasForeignKey(x => x.IdVenta);
    }
}
