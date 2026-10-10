using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace mi_primer_api.Models;

public partial class DbEmpresaContext : DbContext
{
    public DbEmpresaContext()
    {
    }

    public DbEmpresaContext(DbContextOptions<DbEmpresaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;user=root;database=db_empresa", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdClientes).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.IdClientes)
                .HasColumnType("int(11)")
                .HasColumnName("id_clientes");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(45)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleados).HasName("PRIMARY");

            entity.ToTable("empleados");

            entity.HasIndex(e => e.IdPuestos, "fk_puestos_idx");

            entity.Property(e => e.IdEmpleados)
                .HasColumnType("int(11)")
                .HasColumnName("id_empleados");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.IdPuestos)
                .HasColumnType("int(11)")
                .HasColumnName("id_puestos");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdPuestosNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPuestos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_puestos");
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.IdPuestos).HasName("PRIMARY");

            entity.ToTable("puestos");

            entity.Property(e => e.IdPuestos)
                .HasColumnType("int(11)")
                .HasColumnName("id_puestos");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
