using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_C_
{
    public partial class form_cr_offres : Form
    {
        form_menu m;
        public form_cr_offres(form_menu m)
        {
            InitializeComponent();

            this.m = m;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form_mon_offre Form = new form_mon_offre(this.m);

            // Ouvre la fenêtre
            Form.Show();  // non bloquant
        }
    }
}
