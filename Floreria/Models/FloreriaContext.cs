using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Floreria.Models
{
    public partial class FloreriaContext : DbContext
    {
        public FloreriaContext()
        {
        }

        public FloreriaContext(DbContextOptions<FloreriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArregloFloral> ArregloFlorals { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; } = null!;
        public virtual DbSet<DetalleProdExtra> DetalleProdExtras { get; set; } = null!;
        public virtual DbSet<Entrega> Entregas { get; set; } = null!;
        public virtual DbSet<Pago> Pagos { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<ProductoExtra> ProductoExtras { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArregloFloral>(entity =>
            {
                entity.HasKey(e => e.IdModelo)
                    .HasName("PK__ArregloF__813C23722FD317CB");

                entity.ToTable("ArregloFloral");

                entity.Property(e => e.IdModelo)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Modelo");

                entity.Property(e => e.DescripcionModelo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreModelo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioArreglo).HasColumnType("money");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__E005FBFFDA795B3F");

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Cliente");

                entity.Property(e => e.MedioContacto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DetallePedido");

                entity.Property(e => e.IdModelo).HasColumnName("ID_Modelo");

                entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");

                entity.Property(e => e.MontoTotal).HasColumnType("money");

                entity.HasOne(d => d.IdModeloNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdModelo)
                    .HasConstraintName("FK_DetallePedido_ArregloFloral");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK_DetallePedido_Pedido");
            });

            modelBuilder.Entity<DetalleProdExtra>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DetalleProdExtra");

                entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.Property(e => e.MontoTotal).HasColumnType("money");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK_DetalleProdExtra_Pedido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_DetalleProdExtra_Producto");
            });

            modelBuilder.Entity<Entrega>(entity =>
            {
                entity.HasKey(e => e.IdEntrega)
                    .HasName("PK__Entrega__FEDACB7F26FCA915");

                entity.ToTable("Entrega");

                entity.Property(e => e.IdEntrega)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Entrega");

                entity.Property(e => e.DireccionEntrega)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoEntrega)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaHoraEntrega).HasColumnType("datetime");

                entity.Property(e => e.NombreRecibe)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenciaDomicilio)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago)
                    .HasName("PK__Pagos__AE88B42997375F35");

                entity.Property(e => e.IdPago)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Pago");

                entity.Property(e => e.EstadoPago)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPago).HasColumnType("date");

                entity.Property(e => e.TipoPago)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPedido).HasColumnType("money");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("PK__Pedido__FD7680702EC72536");

                entity.ToTable("Pedido");

                entity.Property(e => e.IdPedido)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Pedido");

                entity.Property(e => e.EstadoPedido)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPedido).HasColumnType("date");

                entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");

                entity.Property(e => e.IdEntrega).HasColumnName("ID_Entrega");

                entity.Property(e => e.IdPago).HasColumnName("ID_Pago");

                entity.HasOne(d => d.oCliente)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Pedido_Cliente");

                entity.HasOne(d => d.oEntrega)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdEntrega)
                    .HasConstraintName("FK_Pedido_Entrega");

                entity.HasOne(d => d.oPago)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdPago)
                    .HasConstraintName("FK_Pedido_Pagos");
            });

            modelBuilder.Entity<ProductoExtra>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__9B4120E2AE2EED73");

                entity.ToTable("ProductoExtra");

                entity.Property(e => e.IdProducto)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Producto");

                entity.Property(e => e.DescripcionProd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreProduct)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioProd).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
