using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProdavnicaWeb.Models
{
    public partial class ProdavnicaWebContext : DbContext
    {
        public ProdavnicaWebContext()
        {
        }

        public ProdavnicaWebContext(DbContextOptions<ProdavnicaWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kategorija> Kategorije { get; set; }
        public virtual DbSet<Kupac> Kupci { get; set; }
        public virtual DbSet<Porudzbina> Porudzbine { get; set; }
        public virtual DbSet<Proizvod> Proizvodi { get; set; }
        public virtual DbSet<Stavka> Stavke { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kupac>(entity =>
            {
                entity.Property(e => e.KupacId).ValueGeneratedNever();

                entity.Property(e => e.Drzava).HasDefaultValueSql("('Srbija')");

                entity.Property(e => e.Grad).HasDefaultValueSql("('Loznica')");
            });

            modelBuilder.Entity<Porudzbina>(entity =>
            {
                entity.Property(e => e.DatumKupovine).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Kupac)
                    .WithMany(p => p.Porudzbine)
                    .HasForeignKey(d => d.KupacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Porudzbin__Kupac__60A75C0F");
            });

            modelBuilder.Entity<Proizvod>(entity =>
            {
                entity.HasOne(d => d.Kategorija)
                    .WithMany(p => p.Proizvod)
                    .HasForeignKey(d => d.KategorijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proizvod__Katego__66603565");
            });

            modelBuilder.Entity<Stavka>(entity =>
            {
                entity.HasOne(d => d.Porudzbina)
                    .WithMany(p => p.Stavke)
                    .HasForeignKey(d => d.PorudzbinaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stavka__Porudzbi__693CA210");

                entity.HasOne(d => d.Proizvod)
                    .WithMany(p => p.Stavke)
                    .HasForeignKey(d => d.ProizvodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stavka__Proizvod__6A30C649");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
