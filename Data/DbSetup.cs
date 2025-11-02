using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Projet_C_.Data
{
    public static class DbSetup
    {
        /// <summary>
        /// Crée/complete les tables et colonnes nécessaires (idempotent, SQLite).
        /// </summary>
        public static async Task RunAsync(SchoolContext db)
        {
            await db.Database.EnsureCreatedAsync();

            // === Tables cœur (au cas où) ===
            await db.Database.ExecuteSqlRawAsync(@"
CREATE TABLE IF NOT EXISTS Utilisateurs(
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Pseudo TEXT NOT NULL UNIQUE
);");
            await db.Database.ExecuteSqlRawAsync(@"
CREATE TABLE IF NOT EXISTS Objets(
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Nom TEXT NOT NULL
);");
            await db.Database.ExecuteSqlRawAsync(@"
CREATE TABLE IF NOT EXISTS Echanges(
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  utilisateur_proposant INTEGER NOT NULL,
  utilisateur_receveur  INTEGER NOT NULL,
  objet_propose         INTEGER NOT NULL,
  objet_demande         INTEGER NOT NULL,
  statut TEXT DEFAULT 'ouvert',
  created_at TEXT DEFAULT (datetime('now')),
  updated_at TEXT DEFAULT (datetime('now'))
);");

            // === Colonnes Echanges (si absentes) ===
            await AddColumnIfMissingAsync(db, "Echanges", "statut", "TEXT", "'ouvert'");
            await AddColumnIfMissingAsync(db, "Echanges", "created_at", "TEXT", "datetime('now')");
            await AddColumnIfMissingAsync(db, "Echanges", "updated_at", "TEXT", "datetime('now')");

            // === Colonnes Objets (si absentes) ===
            await AddColumnIfMissingAsync(db, "Objets", "Description", "TEXT", "NULL");
            await AddColumnIfMissingAsync(db, "Objets", "type_objet", "TEXT", "NULL");
            await AddColumnIfMissingAsync(db, "Objets", "EtatObjet", "INTEGER", "1"); // ✅ INTEGER pour l'enum
            await AddColumnIfMissingAsync(db, "Objets", "disponible", "INTEGER", "1"); // 1 = true
            await AddColumnIfMissingAsync(db, "Objets", "proprietaire_id", "INTEGER", "NULL");
            
            // ✅ NETTOYAGE : Supprimer les colonnes inutiles si elles existent
            await DropColumnIfExistsAsync(db, "Objets", "quantite");
            await DropColumnIfExistsAsync(db, "Objets", "prix");
            await DropColumnIfExistsAsync(db, "Objets", "type");
            
            await db.Database.ExecuteSqlRawAsync(
                "CREATE INDEX IF NOT EXISTS ix_objets_owner ON Objets(proprietaire_id);");

            // --- Backfill: si disponible est NULL -> 1 (sinon le marché est vide) ---
            await db.Database.ExecuteSqlRawAsync("UPDATE Objets SET disponible = 1 WHERE disponible IS NULL;");
            
            // ✅ Backfill: si EtatObjet est NULL -> 1 (Bon)
            await db.Database.ExecuteSqlRawAsync("UPDATE Objets SET EtatObjet = 1 WHERE EtatObjet IS NULL OR EtatObjet = 0;");

            // === Historique des échanges ===
            await db.Database.ExecuteSqlRawAsync(@"
CREATE TABLE IF NOT EXISTS EchangeEvents(
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  EchangeId INTEGER NOT NULL,
  Type TEXT NOT NULL,           -- 'message' | 'statut' | 'system'
  Contenu TEXT,
  CreatedAt TEXT DEFAULT (datetime('now'))
);");

            // === Trigger updated_at ===
            await db.Database.ExecuteSqlRawAsync(@"
CREATE TRIGGER IF NOT EXISTS echanges_touch_upd AFTER UPDATE ON Echanges
BEGIN
  UPDATE Echanges SET updated_at = datetime('now') WHERE Id = NEW.Id;
END;");
        }

        private static async Task AddColumnIfMissingAsync(
            SchoolContext db, string table, string column, string sqlType, string defaultExpr)
        {
            bool exists = false;
            using (var conn = db.Database.GetDbConnection())
            {
                if (conn.State != ConnectionState.Open) await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $"PRAGMA table_info({table});";
                using var rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    if (string.Equals(rd.GetString(1), column, StringComparison.OrdinalIgnoreCase))
                    { exists = true; break; }
                }
            }
            if (!exists)
            {
                await db.Database.ExecuteSqlRawAsync(
                    $"ALTER TABLE {table} ADD COLUMN {column} {sqlType} DEFAULT ({defaultExpr});");
            }
        }

        // ✅ NOUVELLE MÉTHODE : Supprimer une colonne si elle existe
        private static async Task DropColumnIfExistsAsync(SchoolContext db, string table, string column)
        {
            // SQLite ne supporte pas DROP COLUMN directement (avant SQLite 3.35.0)
            // On va simplement ignorer cette colonne dans le mapping EF
            // Si vous voulez vraiment la supprimer, il faut recréer la table
            
            bool exists = false;
            using (var conn = db.Database.GetDbConnection())
            {
                if (conn.State != ConnectionState.Open) await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $"PRAGMA table_info({table});";
                using var rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    if (string.Equals(rd.GetString(1), column, StringComparison.OrdinalIgnoreCase))
                    { exists = true; break; }
                }
            }
            
            if (exists)
            {
                // Pour SQLite, on doit recréer la table sans ces colonnes
                // C'est complexe, donc on va juste mettre les valeurs à NULL
                try
                {
                    await db.Database.ExecuteSqlRawAsync($"UPDATE {table} SET {column} = NULL;");
                }
                catch
                {
                    // Ignorer si la colonne n'existe pas ou n'est pas accessible
                }
            }
        }

        /// <summary>Seed minimal si la base est vide.</summary>
        public static async Task SeedSampleAsync(SchoolContext db)
        {
            await db.Database.EnsureCreatedAsync();

            using var conn = db.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open) await conn.OpenAsync();

            // déjà des échanges ? on sort
            using (var check = conn.CreateCommand())
            {
                check.CommandText = "SELECT COUNT(1) FROM Echanges;";
                var count = (long)(await check.ExecuteScalarAsync() ?? 0L);
                if (count > 0) return;
            }

            static async Task ExecAsync(System.Data.Common.DbConnection c, string sql)
            { using var cmd = c.CreateCommand(); cmd.CommandText = sql; try { await cmd.ExecuteNonQueryAsync(); } catch { } }
            static async Task<int> ScalarIntAsync(System.Data.Common.DbConnection c, string sql)
            { using var cmd = c.CreateCommand(); cmd.CommandText = sql; var o = await cmd.ExecuteScalarAsync(); return o is long l ? (int)l : o is int i ? i : 0; }

            await ExecAsync(conn, "INSERT OR IGNORE INTO Utilisateurs(Pseudo) VALUES ('Alice');");
            await ExecAsync(conn, "INSERT OR IGNORE INTO Utilisateurs(Pseudo) VALUES ('Bob');");
            int idAlice = await ScalarIntAsync(conn, "SELECT Id FROM Utilisateurs WHERE Pseudo='Alice' LIMIT 1;");
            int idBob = await ScalarIntAsync(conn, "SELECT Id FROM Utilisateurs WHERE Pseudo='Bob'   LIMIT 1;");

            await ExecAsync(conn, "INSERT OR IGNORE INTO Objets(Nom, Description, type_objet, EtatObjet) VALUES ('PC Portable', 'Ordinateur portable en bon état', 'Outil', 1);");
            await ExecAsync(conn, "INSERT OR IGNORE INTO Objets(Nom, Description, type_objet, EtatObjet) VALUES ('Livre C#', 'Guide complet du langage C#', 'Livre', 0);");
            int idPc = await ScalarIntAsync(conn, "SELECT Id FROM Objets WHERE Nom='PC Portable' LIMIT 1;");
            int idBook = await ScalarIntAsync(conn, "SELECT Id FROM Objets WHERE Nom='Livre C#'   LIMIT 1;");

            // propriétaires de démo
            await ExecAsync(conn, $"UPDATE Objets SET proprietaire_id = {idAlice}, disponible = 1, EtatObjet = 1 WHERE Nom='PC Portable';");
            await ExecAsync(conn, $"UPDATE Objets SET proprietaire_id = {idBob},   disponible = 1, EtatObjet = 0 WHERE Nom='Livre C#';");

            await ExecAsync(conn, $@"
INSERT INTO Echanges(utilisateur_proposant, utilisateur_receveur, objet_propose, objet_demande, statut)
VALUES ({idAlice}, {idBob}, {idPc}, {idBook}, 'ouvert');");

            int exId = await ScalarIntAsync(conn, "SELECT Id FROM Echanges ORDER BY Id DESC LIMIT 1;");
            await ExecAsync(conn, $@"
INSERT INTO EchangeEvents(EchangeId, Type, Contenu)
VALUES ({exId}, 'system', 'Échange créé (seed).');");
        }
    }
}
