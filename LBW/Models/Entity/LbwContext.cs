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

        // public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
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

                entity.Property(e => e.Clave)
                   .IsRequired(false)
                   .HasColumnName("contrasena_hash")
                   .HasMaxLength(255);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FechaCreacion")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");
            });
            */

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioID);

                entity.ToTable("USUARIO");

                entity.Property(e => e.UsuarioID)
                    .HasColumnName("USER_NAME")
                    .HasMaxLength(100);

                entity.Property(e => e.NombreCompleto)
                    .HasColumnName("FULL_NAME")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("EMAIL_ADDR")
                    .HasMaxLength(100)
                    .IsRequired(false);
    
                entity.Property(e => e.Rol)
                    .HasColumnName("ROL")   // Convertir de bit a bool
                    .IsRequired(false);        // Asumiendo que el campo es requerido

                entity.Property(e => e.GMT_OFFSET)
                    .HasColumnName("GMT_OFFSET")
                    .IsRequired(false); 

                entity.Property(e => e.UsuarioDeshabilitado)
                    .HasColumnName("USER_DISABLED")
                    .IsRequired(false);

                entity.Property(e => e.FechaDeshabilitado)
                    .HasColumnName("DATE_DISABLED")
                    .HasColumnType("datetime")
                    .IsRequired(false);

                // Si quieres que el campo FechaDeshabilitado tenga un valor predeterminado
                // de la fecha actual en caso de ser NULL, podrías usar algo como esto:
                // .HasDefaultValueSql("GETDATE()");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
