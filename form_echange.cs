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
            public int ProposantId { get; init; }  // ✅ AJOUT pour vérification
            public int ReceveurId { get; init; }   // ✅ AJOUT pour vérification
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

            // Actions
            button_accepter.Click += async (_, __) => await AccepterAsync();
            button_refuser.Click += async (_, __) => await RefuserAsync();
            button_faire_offre.Click += button_faire_offre_Click;

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

        // ====== Méthode publique pour rafraîchir depuis l'extérieur ======
        public async Task RefreshFromExternalAsync()
        {
            await RefreshListAsync();
        }

        // ====== Recherche & remplissage de la liste ======
        private async Task RefreshListAsync()
        {
            string? q = string.IsNullOrWhiteSpace(Rechercher.Text) ? null : Rechercher.Text.Trim();
            string filtre = listbox_type.SelectedItem?.ToString() ?? "Utilisateur";

            int currentUserId = m?.CurrentUser?.Id ?? 0;
            if (currentUserId == 0)
            {
                _items.Clear();
                return;
            }

            // Jointures sur tes tables existantes
            var query =
                from e in _db.Echanges
                join up in _db.Utilisateurs on e.utilisateur_proposant equals up.Id
                join ur in _db.Utilisateurs on e.utilisateur_receveur equals ur.Id
                join op in _db.Objets on e.objet_propose equals op.Id
                join od in _db.Objets on e.objet_demande equals od.Id
                where e.statut != "refuse"  // ✅ EXCLUSION DES OFFRES REFUSÉES
                   && (e.utilisateur_proposant == currentUserId || e.utilisateur_receveur == currentUserId) // ✅ FILTRE PAR UTILISATEUR
                select new EchangeVM
                {
                    Id = e.Id,
                    Proposant = up.Pseudo,
                    Receveur = ur.Pseudo,
                    Offre = op.Nom,
                    Demande = od.Nom,
                    ProposantId = e.utilisateur_proposant,  // ✅ AJOUT
                    ReceveurId = e.utilisateur_receveur      // ✅ AJOUT
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

            // Mettre à jour le compteur d'offres si le label existe
            try
            {
                label_nbr_offree.Text = $"Offres actives : {_items.Count}";
            }
            catch { }
        }

        // ====== Actions : accepter / refuser + log historique ======
        private EchangeVM? Current() => listBox_offres.SelectedItem as EchangeVM;

        private async Task AccepterAsync()
        {
            var vm = Current();
            if (vm is null)
            {
                MessageBox.Show("Veuillez sélectionner un échange.", "Sélection requise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Accepter l'échange :\n• Proposant : {vm.Proposant}\n• Offre : {vm.Offre}\n• Demande : {vm.Demande}",
                "Confirmer l'acceptation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                await UpdateStatutAndLogAsync(vm.Id, "accepte", "Statut: accepté");
                MessageBox.Show("Échange accepté avec succès !", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await RefreshListAsync();
            }
        }

        private async Task RefuserAsync()
        {
            var vm = Current();
            if (vm is null)
            {
                MessageBox.Show("Veuillez sélectionner un échange.", "Sélection requise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Refuser l'échange :\n• Proposant : {vm.Proposant}\n• Offre : {vm.Offre}\n• Demande : {vm.Demande}\n\nCette action masquera l'offre de la liste.",
                "Confirmer le refus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                await UpdateStatutAndLogAsync(vm.Id, "refuse", "Statut: refusé");
                MessageBox.Show("Échange refusé. L'offre a été retirée de la liste.", "Refus enregistré",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await RefreshListAsync();
            }
        }

        private async Task UpdateStatutAndLogAsync(int echangeId, string statut, string note)
        {
            await _db.Database.ExecuteSqlRawAsync(
                "UPDATE Echanges SET statut = {0} WHERE Id = {1};", statut, echangeId);

            await _db.Database.ExecuteSqlRawAsync(@"
INSERT INTO EchangeEvents(EchangeId, Type, Contenu)
VALUES ({0}, 'statut', {1});", echangeId, note);
        }

        // ====== Contre-offre : permet de faire une contre-proposition ======
        private async void button_faire_offre_Click(object sender, EventArgs e)
        {
            var vm = Current();
            if (vm is null)
            {
                MessageBox.Show("Veuillez sélectionner un échange pour faire une contre-offre.",
                    "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int currentUserId = m?.CurrentUser?.Id ?? 0;

            // ✅ VÉRIFICATION : Empêcher de faire une contre-offre à soi-même
            if (vm.ProposantId == currentUserId && vm.ReceveurId == currentUserId)
            {
                MessageBox.Show("Vous ne pouvez pas faire une contre-offre à vous-même.",
                    "Action non autorisée",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ✅ VÉRIFICATION : On ne peut faire une contre-offre que si on est le receveur
            if (vm.ReceveurId != currentUserId)
            {
                MessageBox.Show("Vous ne pouvez faire une contre-offre que pour les offres que vous avez reçues.",
                    "Action non autorisée",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Demander confirmation pour la contre-offre
            var confirmation = MessageBox.Show(
                $"Faire une contre-offre refusera automatiquement l'offre actuelle.\n\n" +
                $"Offre actuelle :\n" +
                $"• De : {vm.Proposant}\n" +
                $"• Offre : {vm.Offre}\n" +
                $"• Contre : {vm.Demande}\n\n" +
                $"Voulez-vous continuer ?",
                "Confirmer la contre-offre",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
                return;

            // Récupérer les détails de l'échange pour faire une contre-offre
            using (var db = new SchoolContext())
            {
                var echange = db.Echanges.FirstOrDefault(e => e.Id == vm.Id);
                if (echange == null)
                {
                    MessageBox.Show("Échange introuvable.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ouvrir form_mon_offre pour faire une contre-offre
                var formMonOffre = new form_mon_offre(
                    m,
                    echange.objet_propose,
                    vm.Offre,
                    echange.utilisateur_proposant,
                    vm.Proposant
                );

                if (formMonOffre.ShowDialog() == DialogResult.OK)
                {
                    // ✅ REFUSER AUTOMATIQUEMENT L'OFFRE PRÉCÉDENTE
                    await UpdateStatutAndLogAsync(vm.Id, "refuse", "Statut: refusé (contre-offre créée)");
                    
                    MessageBox.Show(
                        "Contre-offre créée avec succès !\nL'offre précédente a été refusée.",
                        "Succès",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await RefreshListAsync();
                }
            }
        }

        // ====== Détail : affiche l'historique (MessageBox) ======
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
        private void type_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button_rechercher_Click(object sender, EventArgs e) { }
    }
}
