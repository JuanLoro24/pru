using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Negocio.Models;

namespace Negocio.Data;

public partial class LibreriaDataContext : DbContext
{
    public LibreriaDataContext()
    {
    }

    public LibreriaDataContext(DbContextOptions<LibreriaDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=LAPTOP-C4JE1T7I; Database=LibreriaData; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PK__Autores__F58AE909BD1F43BC");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libros__3214EC27A8AADA52");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Autor).WithMany(p => p.Libros)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Libros__AutorID__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
