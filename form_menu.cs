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
        private form_echange? formEchange;
        private form_inventaire? formInventaire;
        private form_cr_offres? formCreerOffre;

        public form_menu()
        {
            InitializeComponent();
            InitialiserOnglets();
        }

        private void InitialiserOnglets()
        {
            // Initialiser le form d'échange
            formEchange = new form_echange(this);
            formEchange.TopLevel = false;
            formEchange.FormBorderStyle = FormBorderStyle.None;
            formEchange.Dock = DockStyle.Fill;
            tabPage_echange.Controls.Add(formEchange);
            formEchange.Show();

            // Initialiser le form d'inventaire
            formInventaire = new form_inventaire(this);
            formInventaire.TopLevel = false;
            formInventaire.FormBorderStyle = FormBorderStyle.None;
            formInventaire.Dock = DockStyle.Fill;
            tabPage_inventaire.Controls.Add(formInventaire);
            formInventaire.Show();

            // Initialiser le form de création d'offre
            formCreerOffre = new form_cr_offres(this);
            formCreerOffre.TopLevel = false;
            formCreerOffre.FormBorderStyle = FormBorderStyle.None;
            formCreerOffre.Dock = DockStyle.Fill;
            tabPage_creer_offre.Controls.Add(formCreerOffre);
            formCreerOffre.Show();
        }



    }
}
