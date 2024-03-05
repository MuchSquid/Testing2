using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBW.Models.Entity
{

    public partial class LbwContext : DbContext
    {

        public LbwContext(DbContextOptions<LbwContext> options)
               : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioID);

                entity.ToTable("Usuarios");

                entity.Property(e => e.UsuarioID)
                    .HasColumnName("UsuarioID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("Nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(50);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FechaCreacion")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.CuentaID);

                entity.ToTable("Cuentas");

                entity.Property(e => e.CuentaID)
                    .HasColumnName("CuentaID");

                entity.Property(e => e.UsuarioID)
                    .HasColumnName("UsuarioID");

                entity.Property(e => e.TipoCuenta)
                    .IsRequired()
                    .HasColumnName("TipoCuenta")
                    .HasMaxLength(50);

                entity.Property(e => e.Saldo)
                    .HasColumnName("Saldo")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Cuentas)
                    .HasForeignKey(d => d.UsuarioID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuentas_Usuarios");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProductoID);

                entity.ToTable("Productos");

                entity.Property(e => e.ProductoID)
                    .HasColumnName("ProductoID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("Nombre")
                    .HasMaxLength(100);

                entity.Property(e => e.Precio)
                    .HasColumnName("Precio")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Stock)
                    .HasColumnName("Stock");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
