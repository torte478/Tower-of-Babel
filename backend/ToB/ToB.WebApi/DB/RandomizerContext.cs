using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ToB.WebApi.DB
{
    public partial class RandomizerContext : DbContext
    {
        public RandomizerContext()
        {
        }

        public RandomizerContext(DbContextOptions<RandomizerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Registry> Registries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Registry>(entity =>
            {
                entity.ToTable("registry");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Parent).HasColumnName("parent");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("label");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
