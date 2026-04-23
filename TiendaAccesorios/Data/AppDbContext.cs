using System;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetallesVenta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tabla Cliente
        modelBuilder.Entity<Cliente>()
            .ToTable("Cliente");

        modelBuilder.Entity<Cliente>()
            .HasKey(x => x.IdCliente);

        modelBuilder.Entity<Cliente>()
            .Property(x => x.NombreCompleto)
            .HasMaxLength(100);

        modelBuilder.Entity<Cliente>()
            .Property(x => x.Extension)
            .HasMaxLength(2);

        modelBuilder.Entity<Cliente>()
            .Property(x => x.Telefono)
            .HasMaxLength(20);

        // Tabla Categoria
        modelBuilder.Entity<Categoria>()
            .ToTable("Categoria");

        modelBuilder.Entity<Categoria>()
            .HasKey(x => x.IdCategoria);

        modelBuilder.Entity<Categoria>()
            .Property(x => x.NombreCategoria)
            .HasMaxLength(100);

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

        // Tabla Venta
        modelBuilder.Entity<Venta>()
            .ToTable("Venta");

        modelBuilder.Entity<Venta>()
            .HasKey(x => x.IdVenta);

        modelBuilder.Entity<Venta>()
            .Property(x => x.FormaDePago)
            .HasMaxLength(30);

        modelBuilder.Entity<Venta>()
            .Property(x => x.Total)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Venta>()
            .HasOne(x => x.Cliente)
            .WithMany(x => x.Ventas)
            .HasForeignKey(x => x.IdCliente);

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
    }
}
