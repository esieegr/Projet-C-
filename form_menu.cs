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
        public form_menu()
        {
            InitializeComponent();
        }

        // BOUTON ADMINISTRATION
        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: ouvre la fenêtre d’admin, ou juste un message pour tester
            form_administration Form = new form_administration(this);

            // Ouvre la fenêtre
            Form.Show();  // non bloquant
        }

        // BOUTON ECHANGE
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: ouvre la fenêtre d’admin, ou juste un message pour tester
            form_echange Form = new form_echange(this);

            // Ouvre la fenêtre
            Form.Show();  // non bloquant
        }

        // BOUTON INVENTAIRE
        private void button3_Click_1(object sender, EventArgs e)
        {
            // TODO: ouvre la fenêtre d’admin, ou juste un message pour tester
            form_inventaire Form = new form_inventaire(this);

            // Ouvre la fenêtre
            Form.Show();  // non bloquant
        }

        public class_utilisateur get_ComboBoxLogin() => (class_utilisateur)comboBox_login.SelectedItem;
        public void set_ComboBoxLogin(List<class_utilisateur> list)
        {
            comboBox_login.Items.Clear();
            foreach (class_utilisateur utilisateur in list)
            {
                comboBox_login.Items.Add(utilisateur);
            }
        }

        public void delete_ComboBoxLogin(class_utilisateur utilisateur)
        {
            comboBox_login.Items.Remove(utilisateur);

        }

        private void button_creer_offre_Click_1(object sender, EventArgs e)
        {
            form_cr_offres Form = new form_cr_offres(this);

            // Ouvre la fenêtre
            Form.Show();  // non bloquant
        }
    }
}
