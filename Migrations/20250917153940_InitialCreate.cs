using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_C_.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Echanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    utilisateur_proposant = table.Column<int>(type: "INTEGER", nullable: false),
                    utilisateur_receveur = table.Column<int>(type: "INTEGER", nullable: false),
                    objet_propose = table.Column<int>(type: "INTEGER", nullable: false),
                    objet_demande = table.Column<int>(type: "INTEGER", nullable: false),
                    quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    prix = table.Column<float>(type: "REAL", nullable: false),
                    type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Echanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: false),
                    type_objet = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: false),
                    prenom = table.Column<string>(type: "TEXT", nullable: false),
                    pseudo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Echanges");

            migrationBuilder.DropTable(
                name: "Objets");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
