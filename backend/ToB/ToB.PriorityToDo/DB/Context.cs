using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ToB.PriorityToDo.DB
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Objective> Objectives { get; set; }
        public virtual DbSet<ObjectiveProject> ObjectiveProjects { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Objective>(entity =>
            {
                entity.ToTable("objective");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("text");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<ObjectiveProject>(entity =>
            {
                entity.ToTable("objective_project");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Objective).HasColumnName("objective");

                entity.Property(e => e.Project).HasColumnName("project");

                entity.HasOne(d => d.ObjectiveNavigation)
                    .WithMany(p => p.ObjectiveProjects)
                    .HasForeignKey(d => d.Objective)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("foreign_objective");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.ObjectiveProjects)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("foreign_projcet");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.Parent).HasColumnName("parent");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
