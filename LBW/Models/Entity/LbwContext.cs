using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
            public DbSet<Analisis> Analisiss { get; set; }
            public DbSet<AnalisisDetalle> AnalisisDetalles { get; set; }
            public DbSet<Planta> Plantas { get; set; }
            public DbSet<PuntoMuestra> PuntoMuestras { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Plantilla> Plantillas { get; set; }
            public DbSet<PlantillaDetalle> PlantillaDetalles { get; set; }
            public DbSet<Proyecto> Proyectos { get; set; }
            public DbSet<Muestra> Muestras { get; set; }
            public DbSet<Resultado> Resultados { get; set; }
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

                modelBuilder.Entity<Lista>(entity =>
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

                modelBuilder.Entity<Analisis>(entity =>
                {
                    entity.HasKey(e => e.IdAnalisis);

                    entity.ToTable("ANALISIS");

                    entity.Property(e => e.IdAnalisis)
                        .HasColumnName("ID_ANALISIS");

                    entity.Property(e => e.IdTipoA)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_TIPOA")
                        .IsFixedLength();

                    entity.Property(e => e.NameAnalisis)
                        .HasColumnName("NAME_ANALISIS")
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.Property(e => e.Version)
                        .HasColumnName("VERSION")
                        .IsRequired(false);

                    entity.Property(e => e.Active)
                       .HasColumnName("ACTIVE")
                       .IsRequired(false);

                    entity.Property(e => e.CommonName)
                      .HasColumnName("COMMON_NAME")
                      .HasMaxLength(100)
                      .IsRequired(false);

                    entity.Property(e => e.Description)
                     .HasColumnName("DESCRIPTION")
                     .HasMaxLength(100)
                     .IsRequired(false);

                    entity.Property(e => e.AliasName)
                     .HasColumnName("ALIAS_NAME")
                     .HasMaxLength(100)
                     .IsRequired(false);

                    entity.Property(e => e.ChangedOn)
                   .HasColumnName("CHANGED_ON")
                   .IsRequired(false);

                    entity.Property(e => e.ChangedBy)
                   .HasColumnName("CHANGED_BY")
                   .HasMaxLength(100)
                   .IsRequired(false);

                    entity.HasOne(d => d.IdANavigation)
                      .WithMany(p => p.Analisiss)
                      .HasForeignKey(d => d.IdTipoA)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__ANALISIS__ID_TIP__5FB337D6");
                });

                modelBuilder.Entity<AnalisisDetalle>(entity =>
                {
                    entity.HasKey(e => e.IdComp);

                    entity.ToTable("ANALISIS_DETALLE");

                    entity.Property(e => e.IdComp)
                        .HasColumnName("ID_COMPONENT");

                    entity.Property(e => e.IdAnalisis)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_ANALISIS")
                        .IsFixedLength();

                    entity.Property(e => e.IdUnidad)
                       .IsRequired()
                       .IsUnicode(false)
                       .HasColumnName("ID_UNIDAD")
                       .IsFixedLength();

                    entity.Property(e => e.NameComponent)
                        .HasColumnName("NAME_COMPONENT")
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.Property(e => e.Version)
                        .HasColumnName("VERSION")
                        .IsRequired(false);

                    entity.Property(e => e.AnalisisData)
                      .HasColumnName("ANALISIS")
                      .HasMaxLength(100)
                      .IsRequired(false);

                    entity.Property(e => e.Units)
                     .HasColumnName("UNITS")
                     .HasMaxLength(100)
                     .IsRequired(false);

                    entity.Property(e => e.Minimun)
                     .HasColumnName("MINIMUM")
                     .IsRequired(false);

                    entity.Property(e => e.Maximun)
                   .HasColumnName("MAXIMUM")
                   .IsRequired(false);

                    entity.Property(e => e.Reportable)
                   .HasColumnName("REPORTABLE")
                   .IsRequired(false);

                    entity.Property(e => e.ClampLow)
                     .HasColumnName("CLAMP_LOW")
                     .HasMaxLength(100)
                     .IsRequired(false);

                    entity.Property(e => e.ClampHigh)
                     .HasColumnName("CLAMP_HIGH")
                     .HasMaxLength(100)
                     .IsRequired(false);

                    entity.HasOne(d => d.IdAnalisisNavigation)
                      .WithMany(p => p.AnalisisDetallesA)
                      .HasForeignKey(d => d.IdAnalisis)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__ANALISIS___ID_AN__70DDC3D8");


                    entity.HasOne(d => d.IdUnidadNavitation)
                      .WithMany(p => p.AnalisisDetallesU)
                      .HasForeignKey(d => d.IdUnidad)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__ANALISIS___ID_UN__71D1E811");
                });

                modelBuilder.Entity<Planta>(entity =>
                {
                    entity.HasKey(e => e.IdPlanta);

                    entity.ToTable("PLANTA");

                    entity.Property(e => e.IdPlanta)
                        .HasColumnName("ID_PLANTA");

                    entity.Property(e => e.IdCliente)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_CLIENTE")
                        .IsFixedLength();

                    entity.Property(e => e.IdSite)
                       .IsRequired()
                       .IsUnicode(false)
                       .HasColumnName("ID_SITE")
                       .IsFixedLength();

                    entity.Property(e => e.NamePl)
                        .HasColumnName("NAME_PLANTA")
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.Property(e => e.ChangedBy)
                        .HasColumnName("CHANGED_BY")
                        .IsRequired(false);

                    entity.Property(e => e.ChangedOn)
                      .HasColumnName("CHANGED_ON")
                      .IsRequired(false);

                    entity.Property(e => e.Removed)
                     .HasColumnName("REMOVED")
                     .IsRequired(false);

                    entity.Property(e => e.Description)
                     .HasColumnName("DESCRIPTION")
                     .IsRequired(false);


                    entity.HasOne(d => d.IdSiteNavigationP)
                      .WithMany(p => p.PlantasS)
                      .HasForeignKey(d => d.IdSite)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__PLANTA__ID_SITE__412EB0B6");


                    entity.HasOne(d => d.IdClienteNavigationP)
                      .WithMany(p => p.PlantasC)
                      .HasForeignKey(d => d.IdCliente)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__PLANTA__ID_CLIEN__403A8C7D");
                });

                modelBuilder.Entity<PuntoMuestra>(entity =>
                {
                    entity.HasKey(e => e.IdPm);

                    entity.ToTable("PUNTO_MUESTRA");

                    entity.Property(e => e.IdPm)
                        .HasColumnName("ID_PM");

                    entity.Property(e => e.IdPlanta)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_PLANTA")
                        .IsFixedLength();

                    entity.Property(e => e.NamePm)
                        .HasColumnName("NAME_PM")

                        .IsRequired(false);

                    entity.Property(e => e.ChangedBy)
                        .HasMaxLength(100)
                        .HasColumnName("CHANGED_BY")
                        .IsRequired(false);

                    entity.Property(e => e.ChangedOn)
                      .HasColumnName("CHANGED_ON")
                      .IsRequired(false);

                    entity.Property(e => e.Description)
                     .HasColumnName("DESCRIPTION")
                     .IsRequired(false);

                    entity.Property(e => e.C_CodPunto)
                   .HasColumnName("C_COD_PUNTO")
                   .IsRequired(false);

                    entity.HasOne(d => d.IdPlantaNavigation)
                      .WithMany(p => p.PuntoMuestrasP)
                      .HasForeignKey(d => d.IdPlanta)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__PUNTO_MUE__ID_PL__46E78A0C");
                });

                modelBuilder.Entity<Cliente>(entity =>
                {
                    entity.HasKey(e => e.IdCliente);

                    entity.ToTable("CLIENTE");

                    entity.Property(e => e.IdCliente)
                        .HasColumnName("ID_CLIENTE");

                    entity.Property(e => e.IdSite)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_SITE")
                        .IsFixedLength();

                    entity.Property(e => e.NameCliente)
                        .HasColumnName("NAME_CLIENTE")
                        .HasMaxLength(100)
                        .IsRequired(false);


                    entity.Property(e => e.Description)
                     .HasColumnName("DESCRIPTION")
                     .HasMaxLength(100)
                     .IsRequired(false);

                    entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(100)
                    .IsRequired(false);


                    entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(100)
                    .IsRequired(false);


                    entity.Property(e => e.Contact)
                    .HasColumnName("CONTACT")
                    .HasMaxLength(100)
                    .IsRequired(false);


                    entity.Property(e => e.ChangedOn)
                      .HasColumnName("CHANGED_ON")
                      .IsRequired(false);

                    entity.Property(e => e.ChangedBy)
                        .HasMaxLength(100)
                        .HasColumnName("CHANGED_BY")
                        .IsRequired(false);

                    entity.Property(e => e.EmailAddrs)
                      .HasMaxLength(100)
                      .HasColumnName("EMAIL_ADDR")
                      .IsRequired(false);

                    entity.Property(e => e.C_ClientesAgua)
                      .HasMaxLength(100)
                      .HasColumnName("C_CLIENTES_AGUA")
                      .IsRequired(false);


                    entity.HasOne(d => d.IdSiteNavigationC)
                      .WithMany(p => p.ClienteS)
                      .HasForeignKey(d => d.IdSite)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__CLIENTE__ID_SITE__37A5467C");
                });


                modelBuilder.Entity<Plantilla>(entity =>
                {
                    entity.HasKey(e => e.IdTL);

                    entity.ToTable("PLANTILLA");

                    entity.Property(e => e.IdTL)
                        .HasColumnName("ID_TL");

                    entity.Property(e => e.IdCliente)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_CLIENTE")
                        .IsFixedLength();

                    entity.Property(e => e.NameTlist)
                        .HasColumnName("NAME_TLIST")
                        .HasMaxLength(100)
                        .IsRequired(false);


                    entity.Property(e => e.Description)
                     .HasColumnName("DESCRIPCION")
                     .HasMaxLength(100)
                     .IsRequired(false);

                    entity.Property(e => e.ChangedOn)
                      .HasColumnName("CHANGED_ON")
                      .IsRequired(false);

                    entity.Property(e => e.ChangedBy)
                        .HasMaxLength(100)
                        .HasColumnName("CHANGED_BY")
                        .IsRequired(false);

                    entity.Property(e => e.Removed)
                       .HasColumnName("REMOVED")
                       .IsRequired(false);

                    entity.HasOne(d => d.IdClienteNavigation)
                      .WithMany(p => p.PlantillaC)
                      .HasForeignKey(d => d.IdCliente)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_PLANTILLA_CLIENTE");
                });

                modelBuilder.Entity<PlantillaDetalle>(entity =>
                {
                    entity.HasKey(e => e.Id_TLE);

                    entity.ToTable("PLANTILLA_DETALLE");

                    entity.Property(e => e.Id_TLE)
                        .HasColumnName("ID_TLE");

                    entity.Property(e => e.Id_TL)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_TL")
                        .IsFixedLength();

                    entity.Property(e => e.Id_Analysis)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_ANALISIS")
                        .IsFixedLength();

                    entity.Property(e => e.Name)
                        .HasColumnName("NAME")
                        .HasMaxLength(100)
                        .IsRequired(false);


                    entity.Property(e => e.Analysis)
                     .HasColumnName("ANALYSIS")
                     .HasMaxLength(100)
                     .IsRequired(false);

                    entity.Property(e => e.OrderNumber)
                      .HasColumnName("ORDER_NUMBER")
                      .IsRequired(false);

                    entity.HasOne(d => d.IdAnalysisNavigation)
                      .WithMany(p => p.PlantillaDetalleA)
                      .HasForeignKey(d => d.Id_Analysis)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__PLANTILLA__ID_AN__6A30C649");


                    entity.HasOne(d => d.IdTLNavigation)
                      .WithMany(p => p.PlantillaDetalleP)
                      .HasForeignKey(d => d.Id_TL)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__PLANTILLA__ID_TL__693CA210");
                });

                modelBuilder.Entity<Proyecto>(entity =>
                {
                    entity.HasKey(e => e.IdProyecto);

                    entity.ToTable("PROYECTO");

                    entity.Property(e => e.IdProyecto)
                        .HasColumnName("ID_PROYECTO");

                    entity.Property(e => e.ID_TL)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_TL")
                        .IsFixedLength();

                    entity.Property(e => e.ID_Cliente)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_CLIENTE")
                        .IsFixedLength();

                    entity.Property(e => e.Name)
                        .HasColumnName("NAME_PROYECTO")
                        .HasMaxLength(100)
                        .IsRequired(false);


                    entity.Property(e => e.TemplateVersion)
                     .HasColumnName("TEMPLATE_VERSION")
                     .IsRequired(false);

                    entity.Property(e => e.Description)
                      .HasMaxLength(100)
                      .HasColumnName("DESCRIPTION")
                      .IsRequired(false);

                    entity.Property(e => e.Note)
                      .HasMaxLength(100)
                      .HasColumnName("NOTE")
                      .IsRequired(false);

                    entity.Property(e => e.Status)
                      .HasMaxLength(100)
                      .HasColumnName("STATUS")
                      .IsRequired(false);

                    entity.Property(e => e.DateCreated)
                     .HasColumnName("DATE_CREATED")
                     .IsRequired(false);

                    entity.Property(e => e.DateRecieved)
                     .HasColumnName("DATE_RECEIVED")
                     .IsRequired(false);

                    entity.Property(e => e.DateStarted)
                     .HasColumnName("DATE_STARTED")
                     .IsRequired(false);

                    entity.Property(e => e.DateCompleted)
                    .HasColumnName("DATE_COMPLETED")
                    .IsRequired(false);

                    entity.Property(e => e.DateReviewed)
                   .HasColumnName("DATE_REVIEWED")
                   .IsRequired(false);

                    entity.Property(e => e.DateUpdated)
                  .HasColumnName("DATE_UPDATED")
                  .IsRequired(false);
                    entity.Property(e => e.Owner)
                    .HasMaxLength(100)
                  .HasColumnName("OWNER")
                  .IsRequired(false);

                    entity.Property(e => e.NumSamples)
                 .HasColumnName("NUM_SAMPLES")
                  .IsRequired(false);


                    entity.HasOne(d => d.IdClienteNavigationPr)
                      .WithMany(p => p.ProyectoC)
                      .HasForeignKey(d => d.ID_Cliente)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__PROYECTO__ID_CLI__3C69FB99");


                    entity.HasOne(d => d.IdPlantillaNavigationPr)
                      .WithMany(p => p.ProyectoP)
                      .HasForeignKey(d => d.ID_TL)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__PROYECTO__ID_TL__3D5E1FD2");
                });

                modelBuilder.Entity<Muestra>(entity =>
                {
                    entity.HasKey(e => e.IdSample);

                    entity.ToTable("MUESTRA");

                    entity.Property(e => e.IdSample)
                        .HasColumnName("ID_SAMPLE");

                    entity.Property(e => e.IdPm)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_PM")
                        .IsFixedLength();

                    entity.Property(e => e.IdCliente)
                       .IsRequired()
                       .IsUnicode(false)
                       .HasColumnName("ID_CLIENTE")
                       .IsFixedLength();

                    entity.Property(e => e.IdLocation)
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnName("ID_LOCATION")
                        .IsFixedLength();

                    entity.Property(e => e.SampleNumber)
                        .HasColumnName("SAMPLE_NUMBER")
                        .HasMaxLength(100)
                        .IsRequired(false);


                    entity.Property(e => e.TextID)
                     .HasMaxLength(100)
                     .HasColumnName("TEXT_ID")
                     .IsRequired(false);

                    entity.Property(e => e.Status)
                      .HasMaxLength(100)
                      .HasColumnName("STATUS")
                      .IsRequired(false);

                    entity.Property(e => e.ChangedOn)
                      .HasColumnName("CHANGED_ON")
                      .IsRequired(false);

                    entity.Property(e => e.OriginalSample)
                      .HasColumnName("ORIGINAL_SAMPLE")
                      .IsRequired(false);

                    entity.Property(e => e.LoginDate)
                     .HasColumnName("LOGIN_DATE")
                     .IsRequired(false);

                    entity.Property(e => e.LoginBy)
                     .HasColumnName("LOGIN_BY")
                     .IsRequired(false);

                    entity.Property(e => e.SampleDate)
                     .HasColumnName("SAMPLED_DATE")
                     .IsRequired(false);

                    entity.Property(e => e.RecdDate)
                    .HasColumnName("RECD_DATE")
                    .IsRequired(false);

                    entity.Property(e => e.ReceivedBy)
                   .HasColumnName("RECEIVED_BY")
                   .IsRequired(false);
                    
                    entity.Property(e => e.DateStarted)
                  .HasColumnName("DATE_STARTED")
                  .IsRequired(false);

                    entity.Property(e => e.DueDate)
                   .HasColumnName("DUE_DATE")
                    .IsRequired(false);

                    entity.Property(e => e.DateCompleted)
                   .HasColumnName("DATE_COMPLETED")
                   .IsRequired(false);

                    entity.Property(e => e.DateReviewed)
                 .HasColumnName("DATE_REVIEWED")
                  .IsRequired(false);

                    entity.Property(e => e.PreBy)
                   .HasMaxLength(100)
                   .HasColumnName("PREP_BY")
                   .IsRequired(false);

                    entity.Property(e => e.Reviewer)
                   .HasMaxLength(100)
                   .HasColumnName("REVIEWER")
                   .IsRequired(false);

                    entity.Property(e => e.SamplingPoint)
                      .HasMaxLength(100)
                      .HasColumnName("SAMPLING_POINT")
                      .IsRequired(false);

                    entity.Property(e => e.SampleType)
                     .HasMaxLength(100)
                     .HasColumnName("SAMPLE_TYPE")
                     .IsRequired(false);

                    entity.Property(e => e.IdProject)
                    .HasColumnName("PROJECT")
                    .IsRequired()
                    .IsUnicode(false)
                    .IsFixedLength();

                    entity.Property(e => e.SampleName)
                  .HasMaxLength(100)
                  .HasColumnName("SAMPLE_NAME")
                  .IsRequired(false);

                    entity.Property(e => e.Location)
                  .HasMaxLength(100)
                  .HasColumnName("LOCATION")
                  .IsRequired(false);


                    entity.Property(e => e.Customer)
                  .HasMaxLength(100)
                  .HasColumnName("CUSTOMER")
                  .IsRequired(false);



                    entity.HasOne(d => d.IdClienteNavigation)
                      .WithMany(p => p.MuestraC)
                      .HasForeignKey(d => d.IdCliente)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__MUESTRA__ID_CLIE__59063A47");


                    entity.HasOne(d => d.IdProyectoNavigation)
                      .WithMany(p => p.MuestraPr)
                      .HasForeignKey(d => d.IdProject)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK__MUESTRA__PROJECT__5AEE82B9");

                    entity.HasOne(d => d.IdPuntoMuestraNavigation)
                     .WithMany(p => p.MuestraPm)
                     .HasForeignKey(d => d.IdPm)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK__MUESTRA__ID_PM__5812160E");

                    entity.HasOne(d => d.IdUbicacionNavigation)
                     .WithMany(p => p.MuestraU)
                     .HasForeignKey(d => d.IdLocation)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("FK__MUESTRA__ID_LOCA__59FA5E80");

                });


            modelBuilder.Entity<Resultado>(entity =>
            {
                entity.HasKey(e => e.IdResult);

                entity.ToTable("RESULTADO");

                entity.Property(e => e.IdResult)
                    .HasColumnName("ID_RESULT");

                entity.Property(e => e.IdSample)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("ID_SAMPLE")
                    .IsFixedLength();

                entity.Property(e => e.IdUnidad)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasColumnName("ID_UNIDAD")
                   .IsFixedLength();

                entity.Property(e => e.IdComponent)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("ID_COMPONENT")
                    .IsFixedLength();

                entity.Property(e => e.IdAnalysis)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasColumnName("ID_ANALISIS")
                   .IsFixedLength();

                entity.Property(e => e.SampleNumber)
                    .HasColumnName("SAMPLE_NUMBER")
                    .HasMaxLength(100)
                    .IsRequired(false);

                entity.Property(e => e.ResultNumber)
                 .HasColumnName("RESULT_NUMBER")
                 .IsRequired(false);

                entity.Property(e => e.OrderNum)
                  .HasColumnName("ORDER_NUM")
                  .IsRequired(false);

                entity.Property(e => e.AnalysisData)
                .HasMaxLength(100)
                  .HasColumnName("ANALISIS")
                  .IsRequired(false);

                entity.Property(e => e.NameComponent)
                .HasMaxLength(100)
                  .HasColumnName("NAME_COMPONENT")
                  .IsRequired(false);

                entity.Property(e => e.ReportedName)
                .HasMaxLength(100)
                 .HasColumnName("REPORTED_NAME")
                 .IsRequired(false);

                entity.Property(e => e.Status)
                 .HasColumnName("STATUS")
                 .IsRequired(false);

                entity.Property(e => e.Reportable)
                .HasMaxLength(100)
                 .HasColumnName("REPORTABLE")
                 .IsRequired(false);

                entity.Property(e => e.ChangedOn)
                .HasColumnName("CHANGED_ON")
                .IsRequired(false);

                entity.Property(e => e.Instrument)
                .HasMaxLength(100)
               .HasColumnName("INSTRUMENT")
               .IsRequired(false);


                entity.HasOne(d => d.IdAnalisisNavigationR)
                  .WithMany(p => p.ResultadosA)
                  .HasForeignKey(d => d.IdAnalysis)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_RESULTADO_ANALISIS");


                entity.HasOne(d => d.IdMuestraNavigationR)
                  .WithMany(p => p.ResultadosM)
                  .HasForeignKey(d => d.IdSample)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__RESULTADO__ID_SA__74AE54BC");

                entity.HasOne(d => d.IdUnidadNavigationR)
                 .WithMany(p => p.ResultadosU)
                 .HasForeignKey(d => d.IdUnidad)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_RESULTADO_UNIDAD");

            });

            OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }

    }
