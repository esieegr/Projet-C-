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
        public form_connexion()
        {
            InitializeComponent();
            ChargerUtilisateurs();
        }

        private void ChargerUtilisateurs()
        {
            using var db = new SchoolContext();
            var utilisateurs = db.Utilisateurs.ToList();
            
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
            // sel = utilisateur choisi dans ta liste/combobox
            var sel = (class_utilisateur)comboBox_utilisateurs.SelectedItem; // adapte au nom de ton contrôle
            if (sel != null)
            {
                var menu = new form_menu(sel);   // ⬅️ passe l’utilisateur
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur.");
            }

        }

        private void button_administration_Click(object sender, EventArgs e)
        {
            var formAdmin = new form_administration();
            formAdmin.Show();
        }
    }
}
