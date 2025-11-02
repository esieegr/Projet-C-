using Microsoft.EntityFrameworkCore;
using Projet_C_.Data;
using Projet_C_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_C_
{
    /// <summary>
    /// Formulaire permettant à l'utilisateur de sélectionner un objet de son inventaire
    /// pour le proposer en échange contre un objet du marché.
    /// </summary>
    public partial class form_mon_offre : Form
    {
        private readonly form_menu _menu;
        private readonly int _objetDemandId;
        private readonly string _objetDemandNom;
        private readonly int _proprietaireId;
        private readonly string _proprietaireNom;
        private readonly BindingList<ObjetVM> _mesObjets = new();

        private sealed class ObjetVM
        {
            public int Id { get; set; }
            public string Nom { get; set; } = "";
            public string Type { get; set; } = "";
            public string Description { get; set; } = "";

            public string Label => $"{Nom} ({Type})";
        }

        /// <summary>
        /// Initialise le formulaire de proposition d'échange.
        /// </summary>
        public form_mon_offre(form_menu menu, int objetDemandId, string objetDemandNom, 
            int proprietaireId, string proprietaireNom)
        {
            InitializeComponent();
            _menu = menu;
            _objetDemandId = objetDemandId;
            _objetDemandNom = objetDemandNom;
            _proprietaireId = proprietaireId;
            _proprietaireNom = proprietaireNom;

            // Affichage des informations
            label_objet_demande.Text = $"Objet demandé : {objetDemandNom} (de {proprietaireNom})";

            // Configuration de la ListBox
            listBox_mes_objets.DataSource = _mesObjets;
            listBox_mes_objets.DisplayMember = "Label";
            listBox_mes_objets.ValueMember = "Id";
            listBox_mes_objets.SelectionMode = SelectionMode.One;

            // Afficher la description lors de la sélection
            listBox_mes_objets.SelectedIndexChanged += ListBox_SelectedIndexChanged;

            // Événements
            button_proposer.Click += button_proposer_Click;
            button_annuler.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            button_rechercher.Click += async (s, e) => await ChargerMesObjetsAsync();
            textBox_recherche.TextChanged += async (s, e) => await ChargerMesObjetsAsync();

            // Chargement initial
            this.Load += async (s, e) => await ChargerMesObjetsAsync();
        }

        private void ListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listBox_mes_objets.SelectedItem is ObjetVM vm)
            {
                label_description.Text = string.IsNullOrWhiteSpace(vm.Description)
                    ? "Aucune description disponible"
                    : vm.Description;
            }
            else
            {
                label_description.Text = "Sélectionnez un objet pour voir sa description";
            }
        }

        private async Task ChargerMesObjetsAsync()
        {
            try
            {
                int currentUserId = _menu?.CurrentUser?.Id ?? 0;
                
                if (currentUserId == 0)
                {
                    MessageBox.Show("Erreur : utilisateur non identifié.", "Erreur", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                string searchQuery = textBox_recherche?.Text?.Trim() ?? "";

                using (var db = new SchoolContext())
                {
                    await DbSetup.RunAsync(db);

                    var tousObjets = await db.Objets
                        .Where(o => o.proprietaire_id == currentUserId)
                        .ToListAsync();

                    var objetsDisponibles = tousObjets.Where(o => o.disponible == true).ToList();

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        objetsDisponibles = objetsDisponibles
                            .Where(o => 
                                (o.Nom?.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (o.type_objet?.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ?? false))
                            .ToList();
                    }

                    var mesObjets = objetsDisponibles.OrderBy(o => o.Nom).ToList();

                    // Désactiver les événements pendant la mise à jour
                    listBox_mes_objets.SelectedIndexChanged -= ListBox_SelectedIndexChanged;
                    
                    _mesObjets.RaiseListChangedEvents = false;
                    _mesObjets.Clear();
                    
                    foreach (var obj in mesObjets)
                    {
                        _mesObjets.Add(new ObjetVM
                        {
                            Id = obj.Id,
                            Nom = obj.Nom,
                            Type = obj.type_objet ?? "",
                            Description = obj.Description ?? ""
                        });
                    }
                    
                    _mesObjets.RaiseListChangedEvents = true;

                    // Forcer le rafraîchissement du DataBinding
                    listBox_mes_objets.DataSource = null;
                    listBox_mes_objets.DataSource = _mesObjets;
                    listBox_mes_objets.DisplayMember = "Label";
                    listBox_mes_objets.ValueMember = "Id";

                    // Réactiver les événements
                    listBox_mes_objets.SelectedIndexChanged += ListBox_SelectedIndexChanged;

                    if (_mesObjets.Count == 0)
                    {
                        label_description.Text = string.IsNullOrEmpty(searchQuery)
                            ? "Vous n'avez aucun objet disponible.\n\nAllez dans l'onglet Inventaire pour ajouter des objets."
                            : $"Aucun objet trouvé pour '{searchQuery}'";
                        
                        button_proposer.Enabled = false;
                    }
                    else
                    {
                        button_proposer.Enabled = true;
                        
                        if (listBox_mes_objets.Items.Count > 0)
                        {
                            listBox_mes_objets.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement de l'inventaire : {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button_proposer.Enabled = false;
            }
        }

        private async void button_proposer_Click(object sender, EventArgs e)
        {
            if (_mesObjets.Count == 0)
            {
                MessageBox.Show(
                    "Vous n'avez aucun objet disponible pour proposer.\n\n" +
                    "Allez dans l'onglet Inventaire pour ajouter des objets.",
                    "Inventaire vide",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (listBox_mes_objets.SelectedItem is not ObjetVM selected)
            {
                MessageBox.Show("Veuillez sélectionner un objet à proposer.", 
                    "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmation = MessageBox.Show(
                $"Confirmer l'offre d'échange ?\n\n" +
                $"• Vous proposez : {selected.Nom}\n" +
                $"• En échange de : {_objetDemandNom}\n" +
                $"• Propriétaire : {_proprietaireNom}",
                "Confirmer l'offre",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation == DialogResult.Yes)
            {
                await CreerEchangeAsync(selected.Id);
            }
        }

        private async Task CreerEchangeAsync(int objetProposeId)
        {
            try
            {
                int currentUserId = _menu?.CurrentUser?.Id ?? 0;

                using (var db = new SchoolContext())
                {
                    await DbSetup.RunAsync(db);

                    var echange = new class_echange
                    {
                        utilisateur_proposant = currentUserId,
                        utilisateur_receveur = _proprietaireId,
                        objet_propose = objetProposeId,
                        objet_demande = _objetDemandId,
                        statut = "en_attente"
                    };

                    db.Echanges.Add(echange);
                    await db.SaveChangesAsync();

                    // Log de l'événement
                    try
                    {
                        await db.Database.ExecuteSqlRawAsync(@"
INSERT INTO EchangeEvents(EchangeId, Type, Contenu)
VALUES ({0}, 'system', 'Offre créée');", echange.Id);
                    }
                    catch
                    {
                        // Ignorer si la table EchangeEvents n'existe pas encore
                    }

                    MessageBox.Show(
                        "Offre créée avec succès !\n\nLe destinataire a été notifié.", 
                        "Succès", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création de l'offre : {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
