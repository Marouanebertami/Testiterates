using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Test_iterates.Entities
{
    public partial class BreweryWholesaleContext : DbContext
    {
        public BreweryWholesaleContext()
        {
        }

        public BreweryWholesaleContext(DbContextOptions<BreweryWholesaleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Beer> Beers { get; set; } = null!;
        public virtual DbSet<Brewery> Breweries { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<Wholesaler> Wholesalers { get; set; } = null!;
        public virtual DbSet<WholesalerBeerStock> WholesalerBeerStocks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MarouaneBertami;Database=BreweryWholesale;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>(entity =>
            {
                entity.ToTable("BEER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AlcoholContent)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("ALCOHOL_CONTENT");

                entity.Property(e => e.IdBrewery).HasColumnName("ID_BREWERY");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("PRICE");

                entity.HasOne(d => d.IdBreweryNavigation)
                    .WithMany(p => p.Beers)
                    .HasForeignKey(d => d.IdBrewery)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BEER__ID_BREWERY__76969D2E");
            });

            modelBuilder.Entity<Brewery>(entity =>
            {
                entity.ToTable("BREWERY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("CLIENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.NameComplete)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME_COMPLETE");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("REQUEST");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("DISCOUNT");

                entity.Property(e => e.IdBeer).HasColumnName("ID_BEER");

                entity.Property(e => e.IdClient).HasColumnName("ID_CLIENT");

                entity.Property(e => e.IdWholesaler).HasColumnName("ID_WHOLESALER");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("UNIT_PRICE");

                entity.HasOne(d => d.IdBeerNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdBeer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__REQUEST__ID_BEER__7E37BEF6");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__REQUEST__ID_CLIE__7F2BE32F");

                entity.HasOne(d => d.IdWholesalerNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdWholesaler)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__REQUEST__ID_WHOL__7D439ABD");
            });

            modelBuilder.Entity<Wholesaler>(entity =>
            {
                entity.ToTable("WHOLESALER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<WholesalerBeerStock>(entity =>
            {
                entity.ToTable("WHOLESALER_BEER_STOCK");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdBeer).HasColumnName("ID_BEER");

                entity.Property(e => e.IdWholesaler).HasColumnName("ID_WHOLESALER");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdBeerNavigation)
                    .WithMany(p => p.WholesalerBeerStocks)
                    .HasForeignKey(d => d.IdBeer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WHOLESALE__ID_BE__778AC167");

                entity.HasOne(d => d.IdWholesalerNavigation)
                    .WithMany(p => p.WholesalerBeerStocks)
                    .HasForeignKey(d => d.IdWholesaler)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WHOLESALE__ID_WH__787EE5A0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
