using Microsoft.EntityFrameworkCore;
using Projet_C_.Data;
using Projet_C_.Models;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Projet_C_.Models.class_objet;

namespace Projet_C_
{
    public partial class form_inventaire : Form
    {
        private readonly form_menu _menu;
        private BindingList<class_objet> _draft = new();

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

            // Attacher l'événement pour le bouton Disponible/Indisponible
            button4.Click += button_toggle_disponibilite_Click;

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
                await DbSetup.RunAsync(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init schéma objets a échoué : " + ex.Message);
            }
        }

        private void LoadDraftFromDb()
        {
            try
            {
                using var db = new SchoolContext();

                List<class_objet> objets = (CurrentUserId == 0)
                    ? new List<class_objet>()
                    : db.Objets
                        .AsNoTracking()
                        .Where(o => o.proprietaire_id == CurrentUserId)
                        .OrderBy(o => o.Nom)
                        .ToList();

                // ✅ DÉSACTIVER l'événement pendant la mise à jour
                listBox_marche.SelectedIndexChanged -= ListBox_SelectedIndexChanged;

                _draft = new BindingList<class_objet>(objets);

                // ✅ FORCER le rafraîchissement du DataBinding
                listBox_marche.DataSource = null;
                listBox_marche.DataSource = _draft;
                listBox_marche.DisplayMember = null;
                listBox_marche.ValueMember = "Id";
                
                listBox_marche.Format -= ListBox_Format; // Éviter les doublons
                listBox_marche.Format += ListBox_Format;

                // ✅ RÉACTIVER l'événement
                listBox_marche.SelectedIndexChanged += ListBox_SelectedIndexChanged;

                // ✅ Sélectionner le premier élément si disponible
                if (listBox_marche.Items.Count > 0)
                {
                    listBox_marche.SelectedIndex = 0;
                }
                
                System.Diagnostics.Debug.WriteLine($"DEBUG: Objets chargés = {objets.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NOUVELLE MÉTHODE : Gestionnaire de format séparé
        private void ListBox_Format(object? sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is class_objet obj)
            {
                e.Value = $"{obj.Nom} {(obj.disponible ? "✓" : "✗")}";
            }
        }

        private void ListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listBox_marche.SelectedItem is class_objet obj)
            {
                textBox_recherche.Text = obj.Nom;
                textBox2.Text = obj.Description ?? "";
                
                int typeIndex = comboBox_type.Items.IndexOf(obj.type_objet);
                if (typeIndex >= 0)
                    comboBox_type.SelectedIndex = typeIndex;
                
                comboBox_etat.SelectedItem = obj.EtatObjet;

                var labelDispo = Controls.Find("label_disponibilite", true).FirstOrDefault() as Label
                    ?? Controls.Find("label5", true).FirstOrDefault() as Label;
                
                if (labelDispo != null)
                {
                    labelDispo.Text = obj.disponible ? "✓ Disponible" : "✗ Indisponible";
                    labelDispo.ForeColor = obj.disponible ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                }
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            var nom = textBox_recherche.Text.Trim();
            var description = textBox2.Text.Trim();
            var typeObjet = comboBox_type.SelectedItem?.ToString() ?? "";
            var etat = (Etat)(comboBox_etat.SelectedItem ?? Etat.Bon);

            if (string.IsNullOrWhiteSpace(nom))
            {
                MessageBox.Show("Le nom est obligatoire.", "Validation", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (CurrentUserId == 0)
            {
                MessageBox.Show("Utilisateur non identifié — impossible d'ajouter l'objet.", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _draft.Add(new class_objet
            {
                Nom = nom,
                Description = description,
                type_objet = typeObjet,
                EtatObjet = etat,
                disponible = true,
                proprietaire_id = CurrentUserId
            });

            textBox_recherche.Clear();
            textBox2.Clear();
            textBox_recherche.Focus();
            
            MessageBox.Show($"Objet '{nom}' ajouté !\n\nN'oubliez pas de sauvegarder.", 
                "Ajout réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_sauvegarder_Click(object sender, EventArgs e)
        {
            if (CurrentUserId == 0)
            {
                MessageBox.Show("Utilisateur non identifié — sauvegarde impossible.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using var db = new SchoolContext();

                var dbObjets = db.Objets
                                 .Where(o => o.proprietaire_id == CurrentUserId)
                                 .AsNoTracking()
                                 .ToList();

                foreach (var d in _draft.Where(d => d.Id == 0))
                    d.proprietaire_id = CurrentUserId;

                // Nouveaux (Id == 0)
                var toAdd = _draft.Where(d => d.Id == 0).ToList();
                if (toAdd.Count > 0) 
                {
                    db.Objets.AddRange(toAdd);
                    System.Diagnostics.Debug.WriteLine($"DEBUG: Ajout de {toAdd.Count} objet(s)");
                }

                // Supprimés (présents en DB mais plus dans le draft)
                var toDelete = dbObjets.Where(dbO => !_draft.Any(d => d.Id == dbO.Id)).ToList();
                if (toDelete.Count > 0)
                {
                    db.Objets.RemoveRange(toDelete);
                    System.Diagnostics.Debug.WriteLine($"DEBUG: Suppression de {toDelete.Count} objet(s)");
                }

                // Modifiés (même Id, champs différents)
                var toUpdate = _draft
                    .Where(d => d.Id != 0
                             && dbObjets.Any(o => o.Id == d.Id &&
                                   (o.Nom != d.Nom
                                 || o.Description != d.Description
                                 || o.type_objet != d.type_objet
                                 || o.EtatObjet != d.EtatObjet
                                 || o.disponible != d.disponible
                                 || (o.proprietaire_id ?? 0) != (d.proprietaire_id ?? 0))))
                    .ToList();

                if (toUpdate.Count > 0)
                {
                    db.UpdateRange(toUpdate);
                    System.Diagnostics.Debug.WriteLine($"DEBUG: Mise à jour de {toUpdate.Count} objet(s)");
                }

                int changes = db.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"DEBUG: {changes} changement(s) sauvegardé(s)");

                LoadDraftFromDb(); // ✅ Recharger APRÈS la sauvegarde
                
                MessageBox.Show(
                    $"Inventaire sauvegardé avec succès !\n\n" +
                    $"• {toAdd.Count} ajout(s)\n" +
                    $"• {toUpdate.Count} modification(s)\n" +
                    $"• {toDelete.Count} suppression(s)", 
                    "Sauvegarde", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde : {ex.Message}\n\n{ex.InnerException?.Message}", 
                    "Erreur", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                
                System.Diagnostics.Debug.WriteLine($"ERREUR: {ex}");
            }
        }

        private void button_supprimer_objet_Click(object sender, EventArgs e)
        {
            if (listBox_marche.SelectedItem is not class_objet sel)
            {
                MessageBox.Show("Sélectionnez un objet.", "Sélection requise",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer '{sel.Nom}' ?\n\nN'oubliez pas de sauvegarder après.",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _draft.Remove(sel);
                MessageBox.Show("Objet supprimé de la liste.\n\nSauvegardez pour confirmer la suppression.",
                    "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_toggle_disponibilite_Click(object sender, EventArgs e)
        {
            if (listBox_marche.SelectedItem is not class_objet sel)
            {
                MessageBox.Show("Veuillez sélectionner un objet dans la liste.", 
                    "Sélection requise", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            sel.disponible = !sel.disponible;

            string statut = sel.disponible ? "disponible" : "indisponible";
            MessageBox.Show($"L'objet '{sel.Nom}' est maintenant {statut}.\n\nN'oubliez pas de sauvegarder pour enregistrer les modifications.", 
                "Disponibilité modifiée", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);

            int selectedIndex = listBox_marche.SelectedIndex;
            
            // ✅ Rafraîchir proprement
            listBox_marche.DataSource = null;
            listBox_marche.DataSource = _draft;
            listBox_marche.DisplayMember = null;
            listBox_marche.ValueMember = "Id";
            listBox_marche.Format -= ListBox_Format;
            listBox_marche.Format += ListBox_Format;
            
            if (selectedIndex >= 0 && selectedIndex < listBox_marche.Items.Count)
                listBox_marche.SelectedIndex = selectedIndex;
        }

        // ✅ NOUVELLE MÉTHODE : Recharger l'inventaire depuis l'extérieur
        public void RefreshInventory()
        {
            LoadDraftFromDb();
        }
    }
}
