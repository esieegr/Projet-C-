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
    public partial class form_menu : Form
    {
        public class_utilisateur CurrentUser { get; }

        private form_echange? formEchange;
        private form_inventaire? formInventaire;
        private form_cr_offres? formCreerOffre;

        public form_menu(class_utilisateur currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;

            InitialiserOnglets();

            // Événement de changement d'onglet
            tabControl_menu.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private async void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Index 2 = "Créer une offre"
            if (tabControl_menu.SelectedIndex == 2 && formCreerOffre != null)
            {
                await formCreerOffre.RefreshMarketAsync();
            }
        }

        private void InitialiserOnglets()
        {
            // Echange
            formEchange = new form_echange(this);
            formEchange.TopLevel = false;
            formEchange.FormBorderStyle = FormBorderStyle.None;
            formEchange.Dock = DockStyle.Fill;
            tabPage_echange.Controls.Add(formEchange);
            formEchange.Show();

            // Inventaire
            formInventaire = new form_inventaire(this);
            formInventaire.TopLevel = false;
            formInventaire.FormBorderStyle = FormBorderStyle.None;
            formInventaire.Dock = DockStyle.Fill;
            tabPage_inventaire.Controls.Add(formInventaire);
            formInventaire.Show();

            // Créer offre
            formCreerOffre = new form_cr_offres(this);
            formCreerOffre.TopLevel = false;
            formCreerOffre.FormBorderStyle = FormBorderStyle.None;
            formCreerOffre.Dock = DockStyle.Fill;
            tabPage_creer_offre.Controls.Add(formCreerOffre);
            formCreerOffre.Show();
        }

        public form_menu() : this(new class_utilisateur { Id = 0, Pseudo = "Invité" }) { }
    }
}
