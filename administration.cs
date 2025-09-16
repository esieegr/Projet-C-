using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_C_
{
    public partial class administration : Form
    {
        menu m;
        public administration(menu m)
        {
            InitializeComponent();

            this.m = m;
        }

        // BOUTON AJOUT D'UTILISATEUR
        private void button1_Click(object sender, EventArgs e)
        {
            listBox_utilisateurs.Items.Add(textBox_nom_utilisateur.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox_utilisateurs.Items.Remove(listBox_utilisateurs.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> utilisateurs = listBox_utilisateurs.Items.Cast<string>().ToList();

            m.set_ComboBoxLogin(utilisateurs);
        }
    }
}
