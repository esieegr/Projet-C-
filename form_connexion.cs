using Projet_C_.Data;
using Projet_C_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_C_
{
    public partial class form_connexion : Form
    {
        /// <summary>
        /// Utilisateur connecté après validation
        /// </summary>
        public class_utilisateur? UtilisateurConnecte { get; private set; }

        public form_connexion()
        {
            InitializeComponent();
            ChargerUtilisateurs();
        }

        private void ChargerUtilisateurs()
        {
            using var db = new SchoolContext();
            var utilisateurs = db.Utilisateurs.OrderBy(u => u.Pseudo).ToList();
            
            comboBox_utilisateurs.Items.Clear();
            foreach (var utilisateur in utilisateurs)
            {
                comboBox_utilisateurs.Items.Add(utilisateur);
            }

            if (comboBox_utilisateurs.Items.Count > 0)
            {
                comboBox_utilisateurs.SelectedIndex = 0;
            }
        }

        private void button_connexion_Click(object sender, EventArgs e)
        {
            // Récupérer l'utilisateur sélectionné
            var sel = comboBox_utilisateurs.SelectedItem as class_utilisateur;
            
            if (sel != null)
            {
                // Stocker l'utilisateur connecté
                UtilisateurConnecte = sel;
                
                // Indiquer que la connexion est réussie
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur.", 
                    "Sélection requise", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
            }
        }

        private void button_administration_Click(object sender, EventArgs e)
        {
            // Créer le formulaire d'administration sans référence au menu
            var formAdmin = new form_administration();
            
            // Afficher en mode modal
            var result = formAdmin.ShowDialog();
            
            // ✅ TOUJOURS recharger la liste après fermeture (OK ou Cancel)
            ChargerUtilisateurs();
            
            // Message optionnel si l'utilisateur a sauvegardé
            if (result == DialogResult.OK)
            {
                MessageBox.Show("Liste des utilisateurs mise à jour.", 
                    "Mise à jour", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
        }
    }
}
