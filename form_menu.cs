using Projet_C_.Models;
using System;
using System.Windows.Forms;

namespace Projet_C_
{
    public partial class form_menu : Form
    {
        public class_utilisateur CurrentUser { get; private set; }

        private form_echange? formEchange;
        private form_inventaire? formInventaire;
        private form_cr_offres? formCreerOffre;

        public form_menu(class_utilisateur currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            
            this.Text = $"Menu - {CurrentUser.Pseudo}";
            
            // Attacher l'événement AVANT d'initialiser les onglets
            tabControl_menu.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            
            // Gérer la fermeture du formulaire
            this.FormClosing += Form_menu_FormClosing;
            
            // Initialiser les onglets et charger le premier
            InitialiserOnglets();
        }

        private void Form_menu_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Si l'utilisateur demande une déconnexion, ne pas fermer l'app
            if (this.DialogResult == DialogResult.Retry)
            {
                e.Cancel = false;
                return;
            }

            // Nettoyer les ressources
            try
            {
                formEchange?.Dispose();
                formInventaire?.Dispose();
                formCreerOffre?.Dispose();
                
                // Forcer la fermeture complète de l'application
                Application.Exit();
            }
            catch
            {
                // Ignorer les erreurs lors de la fermeture
            }
        }

        private async void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabControl_menu.SelectedTab == tabPage_echange)
            {
                if (formEchange == null)
                {
                    formEchange = new form_echange(this);
                    formEchange.TopLevel = false;
                    formEchange.FormBorderStyle = FormBorderStyle.None;
                    formEchange.Dock = DockStyle.Fill;
                    tabPage_echange.Controls.Add(formEchange);
                    formEchange.Show();
                }
                else
                {
                    // Rafraîchir la liste quand on revient sur l'onglet
                    await formEchange.RefreshFromExternalAsync();
                }
            }
            else if (tabControl_menu.SelectedTab == tabPage_inventaire)
            {
                if (formInventaire == null)
                {
                    formInventaire = new form_inventaire(this);
                    formInventaire.TopLevel = false;
                    formInventaire.FormBorderStyle = FormBorderStyle.None;
                    formInventaire.Dock = DockStyle.Fill;
                    tabPage_inventaire.Controls.Add(formInventaire);
                    formInventaire.Show();
                }
            }
            else if (tabControl_menu.SelectedTab == tabPage_creer_offre)
            {
                if (formCreerOffre == null)
                {
                    formCreerOffre = new form_cr_offres(this);
                    formCreerOffre.TopLevel = false;
                    formCreerOffre.FormBorderStyle = FormBorderStyle.None;
                    formCreerOffre.Dock = DockStyle.Fill;
                    tabPage_creer_offre.Controls.Add(formCreerOffre);
                    formCreerOffre.Show();
                }
                else
                {
                    // Rafraîchir le marché
                    await formCreerOffre.RefreshMarketAsync();
                }
            }
        }

        private void InitialiserOnglets()
        {
            // Charger le premier onglet manuellement
            if (tabControl_menu.TabPages.Count > 0)
            {
                tabControl_menu.SelectedIndex = 0;
                
                // Déclencher manuellement le chargement du premier onglet
                TabControl_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Déconnecte l'utilisateur actuel et retourne à l'écran de connexion
        /// </summary>
        public void Deconnexion()
        {
            var result = MessageBox.Show(
                $"Voulez-vous vraiment vous déconnecter ({CurrentUser.Pseudo}) ?",
                "Confirmation de déconnexion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Nettoyer les ressources
                formEchange?.Dispose();
                formInventaire?.Dispose();
                formCreerOffre?.Dispose();

                // Utiliser DialogResult.Retry comme signal de déconnexion
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        public form_menu() : this(new class_utilisateur { Id = 0, Pseudo = "Invité" })
        {
        }
    }
}
