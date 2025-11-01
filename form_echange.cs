using System.ComponentModel;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Projet_C_.Data;

namespace Projet_C_
{
    public partial class form_echange : Form
    {
        // Contexte EF existant
        private readonly SchoolContext _db = new();

        // VM affichée dans la ListBox
        private sealed class EchangeVM
        {
            public int Id { get; init; }
            public string Proposant { get; init; } = "";
            public string Receveur { get; init; } = "";
            public string Offre { get; init; } = "";
            public string Demande { get; init; } = "";
            public override string ToString() =>
                $"#{Id} • {Proposant} ↔ {Receveur} • Offre: {Offre} • Demande: {Demande}";
        }

        // Binding
        private readonly BindingList<EchangeVM> _items = new();
        private readonly BindingSource _bs = new();

        private readonly form_menu m;

        public form_echange(form_menu m)
        {
            InitializeComponent();
            this.m = m;

            // Remplit le filtre (contrôles déjà dans ton Designer)
            listbox_type.Items.Clear();
            listbox_type.Items.Add("Utilisateur");
            listbox_type.Items.Add("Type");
            listbox_type.SelectedIndex = 0;

            // Binding propre
            _bs.DataSource = _items;
            listBox_offres.DataSource = _bs;

            // Recherche
            button_rechercher.Click += async (_, __) => await RefreshListAsync();
            listbox_type.SelectedIndexChanged += async (_, __) => await RefreshListAsync();

            // Détail (historique au double-clic)
            listBox_offres.DoubleClick += (_, __) => OuvrirDetail();

            // Actions (si les boutons existent dans le Designer)
            try { button_accepter.Click += async (_, __) => await AccepterAsync(); } catch { }
            try { button_refuser.Click += async (_, __) => await RefuserAsync(); } catch { }

            // Init explicite (pas besoin d'event Load)
            _ = InitAsync();
        }

        // ====== Init : setup + seed + premier refresh ======
        private async Task InitAsync()
        {
            try
            {
                await DbSetup.RunAsync(_db);        // colonnes/table/trigger si absents
                await DbSetup.SeedSampleAsync(_db); // jeu de données si BDD vide
                await RefreshListAsync();           // charge la liste
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init échanges a échoué : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====== Recherche & remplissage de la liste ======
        private async Task RefreshListAsync()
        {
            string? q = string.IsNullOrWhiteSpace(Rechercher.Text) ? null : Rechercher.Text.Trim();
            string filtre = listbox_type.SelectedItem?.ToString() ?? "Utilisateur";

            // Jointures sur tes tables existantes
            var query =
                from e in _db.Echanges
                join up in _db.Utilisateurs on e.utilisateur_proposant equals up.Id
                join ur in _db.Utilisateurs on e.utilisateur_receveur equals ur.Id
                join op in _db.Objets on e.objet_propose equals op.Id
                join od in _db.Objets on e.objet_demande equals od.Id
                select new EchangeVM
                {
                    Id = e.Id,
                    Proposant = up.Pseudo,
                    Receveur = ur.Pseudo,
                    Offre = op.Nom,
                    Demande = od.Nom
                };

            if (!string.IsNullOrEmpty(q))
            {
                if (filtre == "Utilisateur")
                {
                    query = query.Where(x =>
                        EF.Functions.Like(x.Proposant, $"%{q}%") ||
                        EF.Functions.Like(x.Receveur, $"%{q}%"));
                }
                else // "Type" => noms d'objets
                {
                    query = query.Where(x =>
                        EF.Functions.Like(x.Offre, $"%{q}%") ||
                        EF.Functions.Like(x.Demande, $"%{q}%"));
                }
            }

            var data = await query
                .OrderByDescending(x => x.Id)
                .Take(300)
                .AsNoTracking()
                .ToListAsync();

            _items.RaiseListChangedEvents = false;
            _items.Clear();
            foreach (var it in data) _items.Add(it);
            _items.RaiseListChangedEvents = true;
            _bs.ResetBindings(false);
        }

        // ====== Actions : accepter / refuser + log historique ======
        private EchangeVM? Current() => listBox_offres.SelectedItem as EchangeVM;

        private async Task AccepterAsync()
        {
            var vm = Current(); if (vm is null) { MessageBox.Show("Sélectionne un échange."); return; }
            await UpdateStatutAndLogAsync(vm.Id, "accepte", "Statut: accepté");
            await RefreshListAsync();
        }

        private async Task RefuserAsync()
        {
            var vm = Current(); if (vm is null) { MessageBox.Show("Sélectionne un échange."); return; }
            await UpdateStatutAndLogAsync(vm.Id, "refuse", "Statut: refusé");
            await RefreshListAsync();
        }

        private async Task UpdateStatutAndLogAsync(int echangeId, string statut, string note)
        {
            await _db.Database.ExecuteSqlRawAsync(
                "UPDATE Echanges SET statut = {0} WHERE Id = {1};", statut, echangeId);

            await _db.Database.ExecuteSqlRawAsync(@"
INSERT INTO EchangeEvents(EchangeId, Type, Contenu)
VALUES ({0}, 'statut', {1});", echangeId, note);
        }

        // ====== Détail : affiche l’historique (MessageBox) ======
        private async void OuvrirDetail()
        {
            var vm = Current(); if (vm is null) return;

            var lines = new List<string>();
            var conn = _db.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open) await conn.OpenAsync();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT CreatedAt, Type, Contenu
                                    FROM EchangeEvents
                                    WHERE EchangeId = @id
                                    ORDER BY datetime(CreatedAt) ASC;";
                var p = cmd.CreateParameter();
                p.ParameterName = "@id"; p.Value = vm.Id;
                cmd.Parameters.Add(p);

                using var rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    string created = rd.IsDBNull(0) ? "" : rd.GetString(0);
                    string type = rd.IsDBNull(1) ? "" : rd.GetString(1);
                    string text = rd.IsDBNull(2) ? "" : rd.GetString(2);
                    lines.Add($"{created} [{type}] {text}");
                }
            }

            var txt = lines.Count == 0 ? "Aucun événement" : string.Join(Environment.NewLine, lines);
            MessageBox.Show(txt, $"Historique échange #{vm.Id}",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Handlers auto-générés (laisse-les si le Designer y fait référence)
        private void Form1_Load(object sender, EventArgs e) { }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button_recherche_Click(object sender, EventArgs e) { }
        private void button_faire_offre_Click(object sender, EventArgs e) { }
        private void type_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button_rechercher_Click(object sender, EventArgs e) { }
    }
}
