using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DogParade.Models
{
    public partial class DogParadeDatabaseContext : DbContext
    {
        public DogParadeDatabaseContext()
        {
        }

        public DogParadeDatabaseContext(DbContextOptions<DogParadeDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dog> Dogs { get; set; }
        public virtual DbSet<Walker> Walkers { get; set; }
        public virtual DbSet<WalkingGroup> WalkingGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Dog>(entity =>
            {
                entity.HasKey(e => e.Did)
                    .HasName("PK__Dog__C03656501A760711");

                entity.ToTable("Dog");

                entity.Property(e => e.Did).HasColumnName("DId");

                entity.Property(e => e.Breed)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasMaxLength(3000);

                entity.HasOne(d => d.GroupNavigation)
                    .WithMany(p => p.DogsNavigation)
                    .HasForeignKey(d => d.Group)
                    .HasConstraintName("FK__Dog__Group__2A4B4B5E");
            });

            modelBuilder.Entity<Walker>(entity =>
            {
                entity.HasKey(e => e.Wid)
                    .HasName("PK__Walker__DB37653950E5ACFF");

                entity.ToTable("Walker");

                entity.Property(e => e.Wid).HasColumnName("WId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.GroupNavigation)
                    .WithMany(p => p.Walkers)
                    .HasForeignKey(d => d.Group)
                    .HasConstraintName("FK__Walker__Group__29572725");
            });

            modelBuilder.Entity<WalkingGroup>(entity =>
            {
                entity.HasKey(e => e.Gid)
                    .HasName("PK__WalkingG__C51F0F1E23FAB791");

                entity.ToTable("WalkingGroup");

                entity.Property(e => e.Gid)
                    .ValueGeneratedNever()
                    .HasColumnName("GId");

                entity.Property(e => e.DurationMins).HasColumnName("Duration(mins)");

                entity.Property(e => e.MeetupLocation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Meetup Location");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Dogs1)
                    .WithMany(p => p.WalkingGroups)
                    .HasForeignKey(d => d.Dogs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WalkingGro__Dogs__286302EC");

                entity.HasOne(d => d.WalkerNavigation)
                    .WithMany(p => p.WalkingGroups)
                    .HasForeignKey(d => d.Walker)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WalkingGr__Walke__276EDEB3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
