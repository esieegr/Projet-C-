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
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        // BOUTON ADMINISTRATION
        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: ouvre la fenêtre d’admin, ou juste un message pour tester
            administration Form = new administration(this);

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
            inventaire Form = new inventaire(this);

            // Ouvre la fenêtre
            Form.Show();  // non bloquant
        }

        public string get_ComboBoxLogin() => (string)comboBox_login.SelectedItem;
        public void set_ComboBoxLogin(List<string> list)
        {
            comboBox_login.Items.Clear();

            comboBox_login.Items.AddRange(list.ToArray());

        }

        private void button_creer_offre_Click_1(object sender, EventArgs e)
        {
            cr_offres Form = new cr_offres(this);

            // Ouvre la fenêtre
            Form.Show();  // non bloquant
        }
    }
}
