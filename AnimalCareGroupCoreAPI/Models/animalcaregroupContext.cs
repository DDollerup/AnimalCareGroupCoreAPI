using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AnimalCareGroupCoreAPI.Models
{
    public partial class animalcaregroupContext : DbContext
    {
        public animalcaregroupContext()
        {
        }

        public animalcaregroupContext(DbContextOptions<animalcaregroupContext> options)
            : base(options)
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<AdoptSection> AdoptSections { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Help> Helps { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<DBChanges> DBChanges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("datasource=animalcaregroup.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<About>(entity =>
            {
                entity.Property(e => e.Content1)
                    .HasColumnType("ntext")
                    .HasColumnName("Content_1");

                entity.Property(e => e.Content2)
                    .HasColumnType("ntext")
                    .HasColumnName("Content_2");

                entity.Property(e => e.Content3)
                    .HasColumnType("ntext")
                    .HasColumnName("Content_3");

                entity.Property(e => e.Title1)
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("Title_1");

                entity.Property(e => e.Title2)
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("Title_2");

                entity.Property(e => e.Title3)
                    .HasColumnType("nvarchar(255)")
                    .HasColumnName("Title_3");
            });

            modelBuilder.Entity<AdoptSection>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Image).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Title).HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.Property(e => e.Age).HasColumnType("ntext");

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.DaysInCare).HasColumnType("int");

                entity.Property(e => e.Details).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Image).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Sex).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Title).HasColumnType("nvarchar(255)");

                entity.Property(e => e.DateAdded).HasColumnType("nvarchar(255)");

                entity.Property(e => e.DateChanged).HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<Help>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.Image).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Title).HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.Property(e => e.Email).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Name).HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<Sysdiagram>(entity =>
            {
                entity.HasKey(e => e.DiagramId);

                entity.ToTable("sysdiagrams");

                entity.HasIndex(e => new { e.PrincipalId, e.Name }, "sysdiagrams_UK_principal_name")
                    .IsUnique();

                entity.Property(e => e.DiagramId).HasColumnName("diagram_id");

                entity.Property(e => e.Definition)
                    .HasColumnType("image")
                    .HasColumnName("definition");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nvarchar(128)")
                    .HasColumnName("name");

                entity.Property(e => e.PrincipalId)
                    .HasColumnType("int")
                    .HasColumnName("principal_id");

                entity.Property(e => e.Version)
                    .HasColumnType("int")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.Extra).HasColumnType("ntext");

                entity.Property(e => e.Image).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Title).HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<DBChanges>(entity =>
            {
                entity.Property(e => e.Resource).HasColumnType("ntext");

                entity.Property(e => e.DateChanged).HasColumnType("ntext");

                entity.Property(e => e.ResourceId).HasColumnType("ntext");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
