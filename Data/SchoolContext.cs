using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Devices;
using Projet_C_.Models;
using System.IO;

namespace Projet_C_.Data
{
    public class SchoolContext : DbContext
    {
        // Tes tables (DbSet)
        public DbSet<Models.class_utilisateur> Utilisateurs { get; set; }
        public DbSet<Models.class_echange> Echanges { get; set; }
        public DbSet<Models.class_objet> Objets { get; set; }

        public string GetDatabasePath()
        {
            return Database.GetDbConnection().DataSource;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fichier SQLite stocké à la racine du projet
                var dbPath = Path.Combine(AppContext.BaseDirectory, "app.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Donne des noms fixes aux tables
            modelBuilder.Entity<class_utilisateur>().ToTable("Utilisateurs");
            modelBuilder.Entity<class_echange>().ToTable("Echanges");
            
            // ✅ Configuration des Objets
            modelBuilder.Entity<class_objet>(entity =>
            {
                entity.ToTable("Objets");
                entity.HasKey(o => o.Id);
                
                // Mapper l'enum Etat
                entity.Property(o => o.EtatObjet)
                    .HasConversion<int>()
                    .HasColumnName("EtatObjet");
                
                // ✅ Ignorer les propriétés héritées non utilisées
                entity.Ignore(o => o.quantite);
                entity.Ignore(o => o.prix);
                entity.Ignore(o => o.type);
                
                // Propriétés utilisées
                entity.Property(o => o.Nom).IsRequired();
                entity.Property(o => o.type_objet);
                entity.Property(o => o.Description);
                entity.Property(o => o.disponible).HasDefaultValue(true);
                entity.Property(o => o.proprietaire_id);
            });
        }
    }
}
