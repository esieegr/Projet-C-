using Projet_C_.Models;
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
using Projet_C_.Data;          // SchoolContext
using Projet_C_.Models;
using Microsoft.EntityFrameworkCore;        // class_utilisateur

namespace Projet_C_
{
    public partial class form_administration : Form
    {
        form_menu m;
        public form_administration(form_menu m)
        {
            InitializeComponent();

            this.m = m;

            ReloadListFromDb();

        }

        // BOUTON AJOUT D'UTILISATEUR
        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new SchoolContext())
            {
                // Création de l’utilisateur depuis les champs
                class_utilisateur utilisateur = new class_utilisateur(
                    textBox_nom.Text,
                    textBox_prenom.Text,
                    textBox_pseudo.Text
                );

                // Ajout en BDD
                db.Utilisateurs.Add(utilisateur);
                db.SaveChanges();

                ReloadListFromDb();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            class_utilisateur user = (class_utilisateur)listBox_utilisateurs.SelectedItem;


            using (var db = new SchoolContext())
            {
                var u = db.Utilisateurs.FirstOrDefault(x => x.Id == user.Id);
                if (u != null)
                {
                    db.Utilisateurs.Remove(u);
                    db.SaveChanges();
                }
            }

            ReloadListFromDb();
        }

        private void ReloadListFromDb()
        {
            using var db = new SchoolContext();
            var users = db.Utilisateurs
                          .AsNoTracking()
                          .OrderBy(u => u.pseudo)
                          .ToList();

            listBox_utilisateurs.DataSource = null;
            listBox_utilisateurs.DisplayMember = "pseudo";
            listBox_utilisateurs.ValueMember = "Id";
            listBox_utilisateurs.DataSource = users;
        }

        private void button3_Click(object sender, EventArgs e)
        {


            using var db = new SchoolContext();
            var users = db.Utilisateurs
                          .AsNoTracking()
                          .OrderBy(u => u.pseudo)
                          .ToList();

            // méthode exposée par le menu pour alimenter sa ComboBox
            m.set_ComboBoxLogin(users);
        }

    }
}
