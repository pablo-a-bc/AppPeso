using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Datos.Datos
{
    public partial class app_pesoContext : DbContext
    {
        public app_pesoContext()
        {
        }

        public app_pesoContext(DbContextOptions<app_pesoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Datospeso> Datospesos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string projectBaseName = "ApiAppPeso";
                string basepath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(basepath+ "\\"+ projectBaseName + "\\Datos\\")
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Database");
                optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<Datospeso>(entity =>
            {
                entity.HasKey(e => e.IdDatoPeso)
                    .HasName("PRIMARY");

                entity.ToTable("datospeso");

                entity.HasIndex(e => e.UsuarioIdusuario, "FK__usuario");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora)
                    .HasColumnType("time")
                    .HasColumnName("hora");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.UsuarioIdusuario)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("usuario_idusuario");

                entity.HasOne(d => d.UsuarioIdusuarioNavigation)
                    .WithMany(p => p.Datospesos)
                    .HasForeignKey(d => d.UsuarioIdusuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__usuario");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRoles)
                    .HasName("PRIMARY");

                entity.ToTable("roles");

                entity.Property(e => e.IdRoles)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.FlagActivo).HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.IdToken)
                    .HasName("PRIMARY");

                entity.ToTable("tokens");

                entity.HasIndex(e => e.UsuarioIdusuario, "FK__usuariotokens");

                entity.Property(e => e.IdToken).HasColumnName("idToken");

                entity.Property(e => e.FechaE).HasColumnType("datetime");

                entity.Property(e => e.FechaGen).HasColumnType("datetime");

                entity.Property(e => e.FechaI).HasColumnType("datetime");

                entity.Property(e => e.Modulo).HasMaxLength(10);

                entity.Property(e => e.UsuarioIdusuario)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("usuario_idusuario");

                entity.HasOne(d => d.UsuarioIdusuarioNavigation)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UsuarioIdusuario)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__usuariotokens");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.RolesIdRoles, "FK_usuario_roles");

                entity.Property(e => e.IdUsuario).HasColumnType("bigint(20)");

                entity.Property(e => e.Apellido).HasMaxLength(85);

                entity.Property(e => e.Estatura).HasDefaultValueSql("'0'");

                entity.Property(e => e.FlagActivo)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Nombre).HasMaxLength(85);

                entity.Property(e => e.Password).HasMaxLength(1248);

                entity.Property(e => e.RolesIdRoles)
                    .HasColumnType("int(11)")
                    .HasColumnName("Roles_IdRoles");

                entity.HasOne(d => d.RolesIdRolesNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolesIdRoles)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_usuario_roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
