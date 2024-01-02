using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GCP.OAuth.Models
{
    public partial class MyTestDBContext : DbContext
    {
        public MyTestDBContext()
        {
        }

        public MyTestDBContext(DbContextOptions<MyTestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sale> Sales { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Pk)
                    .HasName("PK__Sale__321507E76B68BD5E");

                entity.ToTable("Sale");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Item).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
