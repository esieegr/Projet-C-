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
    public partial class form_mon_offre: Form
    {
        form_menu m;
        public form_mon_offre(form_menu m)
        {
            InitializeComponent();
            this.m = m;
        }
    }
}
