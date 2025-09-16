using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Devices;
using System.IO;

namespace Projet_C_
{
    public class SchoolContext : DbContext
    {
        // Tes tables (DbSet)
        //public DbSet<Student> Students { get; set; }
        //public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fichier SQLite stocké à la racine du projet
                var dbPath = Path.Combine(AppContext.BaseDirectory, "school.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Donne des noms fixes aux tables
            //modelBuilder.Entity<Student>().ToTable("Students");
            //modelBuilder.Entity<Course>().ToTable("Courses");
        }
    }
}
