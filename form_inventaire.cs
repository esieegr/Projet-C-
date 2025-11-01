using Microsoft.EntityFrameworkCore;
using Projet_C_.Data;
using Projet_C_.Models;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Projet_C_.Models.class_objet;

namespace Projet_C_
{
    public partial class form_inventaire : Form
    {
        private readonly form_menu _menu;                 // utilisateur courant
        private BindingList<class_objet> _draft = new();   // liste liée

        // Id de l'utilisateur connecté (exposé par form_menu)
        private int CurrentUserId => _menu?.CurrentUser?.Id ?? 0;

        public form_inventaire(form_menu m)
        {
            InitializeComponent();
            _menu = m;

            // Préparer les combos
            comboBox_etat.DataSource = Enum.GetValues(typeof(Etat));
            comboBox_type.Items.Clear();
            comboBox_type.Items.AddRange(new[] { "Livre", "Jeu", "Vêtement", "Outil" });
            comboBox_type.SelectedIndex = 0;
            comboBox_etat.SelectedIndex = 0;

            // Assure le schéma (colonnes) puis charge la liste
            this.Shown += async (_, __) =>
            {
                await EnsureSchemaAsync();
                LoadDraftFromDb();
            };
        }

        private async Task EnsureSchemaAsync()
        {
            try
            {
                using var db = new SchoolContext();
                await DbSetup.RunAsync(db);        // ⬅️ crée/complète les colonnes manquantes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init schéma objets a échoué : " + ex.Message);
            }
        }

        private void LoadDraftFromDb()
        {
            using var db = new SchoolContext();

            List<class_objet> objets = (CurrentUserId == 0)
                ? new List<class_objet>()
                : db.Objets
                    .AsNoTracking()
                    .Where(o => o.proprietaire_id == CurrentUserId)   // ⬅️ filtre propriétaire
                    .OrderBy(o => o.Nom)
                    .ToList();

            _draft = new BindingList<class_objet>(objets);

            listBox_marche.DataSource = null;
            listBox_marche.DisplayMember = "Nom";
            listBox_marche.ValueMember = "Id";
            listBox_marche.DataSource = _draft;
        }

        // BOUTON AJOUTER (mémoire uniquement)
        private void button_add_Click(object sender, EventArgs e)
        {
            var nom = textBox_recherche.Text.Trim();
            var typeObjet = comboBox_type.SelectedItem?.ToString() ?? "";
            var etat = (Etat)(comboBox_etat.SelectedItem ?? Etat.Bon);

            if (string.IsNullOrWhiteSpace(nom))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }
            if (CurrentUserId == 0)
            {
                MessageBox.Show("Utilisateur non identifié — impossible d'ajouter l'objet.");
                return;
            }

            _draft.Add(new class_objet
            {
                Nom = nom,
                type_objet = typeObjet,
                EtatObjet = etat,
                disponible = true,
                proprietaire_id = CurrentUserId     // ⬅️ attribuer le propriétaire dès l’ajout
            });

            textBox_recherche.Clear();
            textBox_recherche.Focus();
        }

        // BOUTON SAUVEGARDER
        private void button_sauvegarder_Click(object sender, EventArgs e)
        {
            if (CurrentUserId == 0)
            {
                MessageBox.Show("Utilisateur non identifié — sauvegarde impossible.");
                return;
            }

            using var db = new SchoolContext();

            // On ne travaille que sur l’inventaire de CE user
            var dbObjets = db.Objets
                             .Where(o => o.proprietaire_id == CurrentUserId)
                             .AsNoTracking()
                             .ToList();

            // S’assurer que tous les nouveaux ont bien le propriétaire défini
            foreach (var d in _draft.Where(d => d.Id == 0))
                d.proprietaire_id = CurrentUserId;

            // Nouveaux (Id == 0)
            var toAdd = _draft.Where(d => d.Id == 0).ToList();
            if (toAdd.Count > 0) db.Objets.AddRange(toAdd);

            // Supprimés (présents en DB mais plus dans le draft)
            var toDelete = dbObjets.Where(dbO => !_draft.Any(d => d.Id == dbO.Id)).ToList();
            if (toDelete.Count > 0) db.Objets.RemoveRange(toDelete);

            // Modifiés (même Id, champs différents)
            var toUpdate = _draft
                .Where(d => d.Id != 0
                         && dbObjets.Any(o => o.Id == d.Id &&
                               (o.Nom != d.Nom
                             || o.type_objet != d.type_objet
                             || o.EtatObjet != d.EtatObjet
                             || o.disponible != d.disponible
                             || (o.proprietaire_id ?? 0) != (d.proprietaire_id ?? 0))))
                .ToList(); // ⬅️ ToList() APRÈS le Where

            if (toUpdate.Count > 0) db.UpdateRange(toUpdate);

            db.SaveChanges();

            LoadDraftFromDb(); // recharge depuis la BDD
            MessageBox.Show("Inventaire sauvegardé.");
        }

        // BOUTON SUPPRIMER (mémoire uniquement)
        private void button_supprimer_objet_Click(object sender, EventArgs e)
        {
            if (listBox_marche.SelectedItem is not class_objet sel)
            {
                MessageBox.Show("Sélectionnez un objet.");
                return;
            }
            _draft.Remove(sel);
        }
    }
}
