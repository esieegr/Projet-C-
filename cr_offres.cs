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
    public partial class cr_offres : Form
    {
        menu m;
        public cr_offres(menu m)
        {
            InitializeComponent();

            this.m = m;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mon_offre Form = new mon_offre(this.m);

            // Ouvre la fenêtre
            Form.Show();  // non bloquant
        }
    }
}
