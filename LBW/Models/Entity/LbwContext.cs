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
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<TipoAnalisis> TipoAnalisiss { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Instrumento> Instrumentos { get; set; }
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

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.HasKey(e => e.ID_LOCATION);

                entity.ToTable("UBICACION");

                entity.Property(e => e.ID_LOCATION)
                    .HasColumnName("ID_LOCATION")
                    .HasMaxLength(100);

                entity.Property(e => e.Name_location)
                    .HasColumnName("NAME_LOCATION")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")  
                    .IsRequired(false);       

                entity.Property(e => e.Contact)
                    .HasColumnName("CONTACT")
                    .IsRequired(false);

            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(e => e.IdSite);

                entity.ToTable("SITE");

                entity.Property(e => e.IdSite)
                    .HasColumnName("ID_SITE")
                    .HasMaxLength(100);

                entity.Property(e => e.NameSite)
                    .HasColumnName("NAME_SITE")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Compania)
                    .HasColumnName("COMPANIA")
                    .HasMaxLength(100)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Unidad>(entity =>
            {
                entity.HasKey(e => e.IdUnidad);

                entity.ToTable("UNIDAD");

                entity.Property(e => e.IdUnidad)
                    .HasColumnName("ID_UNIDAD")
                    .HasMaxLength(100);

                entity.Property(e => e.Nombre)
                    .HasColumnName("NAME_UNIDAD")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.DisplayString)
                    .HasColumnName("DISPLAY_STRING")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.ChangedBy)
                   .HasColumnName("CHANGED_BY")
                   .HasMaxLength(100)
                   .IsRequired(false);

                entity.Property(e => e.ChangedOn)
                    .HasColumnName("CHANGED_ON")
                    .IsRequired(false);

                entity.Property(e => e.Removed)
                    .HasColumnName("REMOVED")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Description)
                   .HasColumnName("DESCRIPTION")
                   .HasMaxLength(100)
                   .IsRequired(false);

            });

            modelBuilder.Entity<TipoAnalisis>(entity =>
            {
                entity.HasKey(e => e.IdTipoA);

                entity.ToTable("TIPO_ANALISIS");

                entity.Property(e => e.IdTipoA)
                    .HasColumnName("ID_TIPOA")
                    .HasMaxLength(100);

                entity.Property(e => e.NombreA)
                    .HasColumnName("NAME_TIPO_ANALISIS")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Removed)
                   .HasColumnName("REMOVED")
                   .IsRequired(false);

            });

            modelBuilder.Entity < Lista>(entity =>
            {
                entity.HasKey(e => e.IdLista);

                entity.ToTable("LISTA");

                entity.Property(e => e.IdLista)
                    .HasColumnName("ID_LISTA")
                    .HasMaxLength(100);

                entity.Property(e => e.List)
                    .HasColumnName("LIST")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.NameLista)
                    .HasColumnName("NAME_LIST")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Value)
                   .HasColumnName("VALUE")
                   .HasMaxLength(100)
                   .IsRequired(false);

                entity.Property(e => e.OrderNumber)
                  .HasColumnName("ORDER_NUMBER")
                  .IsRequired(false);
            });

            modelBuilder.Entity<Instrumento>(entity =>
            {
                entity.HasKey(e => e.IdInstrumento);

                entity.ToTable("INSTRUMENTO");

                entity.Property(e => e.IdInstrumento)
                    .HasColumnName("ID_INSTRUMENTO")
                    .HasMaxLength(100);

                entity.Property(e => e.IdCodigo)
                    .HasColumnName("ID_CODIGO")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("DESCRIPCION")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.Nombre)
                   .HasColumnName("NOMBRE")
                   .HasMaxLength(100)
                   .IsRequired(false);

                entity.Property(e => e.Tipo)
                  .HasColumnName("TIPO")
                  .HasMaxLength(100)
                  .IsRequired(false);

                entity.Property(e => e.Vendor)
                 .HasColumnName("VENDOR")
                 .HasMaxLength(100)
                 .IsRequired(false);

                entity.Property(e => e.Habilitado)
                 .HasColumnName("HABILITADO")
                 .IsRequired(false);

                entity.Property(e => e.FechaCalibrado)
               .HasColumnName("FECHA_CALIBRACION")
               .IsRequired(false);

                entity.Property(e => e.FechaCaducidad)
               .HasColumnName("FECHA_CADUCIDAD")
               .IsRequired(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
