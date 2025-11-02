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

            // Événements
            button_proposer.Click += button_proposer_Click;
            button_annuler.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Chargement initial
            this.Load += async (s, e) => await ChargerMesObjetsAsync();
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

                using (var db = new SchoolContext())
                {
                    await DbSetup.RunAsync(db);

                    var mesObjets = await db.Objets
                        .Where(o => o.proprietaire_id == currentUserId && o.disponible)
                        .OrderBy(o => o.Nom)
                        .ToListAsync();

                    _mesObjets.Clear();
                    foreach (var obj in mesObjets)
                    {
                        _mesObjets.Add(new ObjetVM
                        {
                            Id = obj.Id,
                            Nom = obj.Nom,
                            Type = obj.type_objet ?? ""
                        });
                    }

                    if (_mesObjets.Count == 0)
                    {
                        MessageBox.Show("Vous n'avez aucun objet disponible pour proposer.", 
                            "Inventaire vide", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Sélectionner automatiquement le premier élément
                        listBox_mes_objets.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement de l'inventaire : {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private async void button_proposer_Click(object sender, EventArgs e)
        {
            // Vérifier la sélection et récupérer l'objet en une seule fois
            if (listBox_mes_objets.SelectedItem is not ObjetVM selected)
            {
                MessageBox.Show("Veuillez sélectionner un objet à proposer.", 
                    "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CreerEchangeAsync(selected.Id);
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

                    MessageBox.Show("Offre créée avec succès !", 
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
