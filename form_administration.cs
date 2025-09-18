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
        private readonly form_menu m;

        // Liste de travail en mémoire (bindée à la ListBox)
        private BindingList<class_utilisateur> _draft = new();

        public form_administration(form_menu m)
        {
            InitializeComponent();
            this.m = m;

            // charge la BDD -> draft -> ListBox
            Load += (_, __) => LoadDraftFromDb();
        }

        // Charge depuis la DB dans la liste draft et bind à la ListBox
        private void LoadDraftFromDb()
        {
            using var db = new SchoolContext();
            var users = db.Utilisateurs
                          .AsNoTracking()
                          .OrderBy(u => u.Pseudo)   // Propriété publique !
                          .ToList();

            _draft = new BindingList<class_utilisateur>(users);

            listBox_utilisateurs.DataSource = null;
            listBox_utilisateurs.DisplayMember = "pseudo";
            listBox_utilisateurs.ValueMember = "Id";
            listBox_utilisateurs.DataSource = _draft;
        }

        // BOUTON AJOUT D'UTILISATEUR
        private void button1_Click(object sender, EventArgs e)
        {
            var u = new class_utilisateur
            {
                Nom = textBox_nom.Text.Trim(),
                Prenom = textBox_prenom.Text.Trim(),
                Pseudo = textBox_pseudo.Text.Trim()
            };

            if (string.IsNullOrWhiteSpace(u.Pseudo))
            {
                MessageBox.Show("Le pseudo est obligatoire.");
                return;
            }

            // Pas de DB ici : on modifie juste la draft (la ListBox se met à jour automatiquement)
            _draft.Add(u);

            // (optionnel) vider les champs
            textBox_nom.Clear();
            textBox_prenom.Clear();
            textBox_pseudo.Clear();
            textBox_nom.Focus();
        }

        // BOUTON SUPPRIMER UTILISATEUR
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox_utilisateurs.SelectedItem is not class_utilisateur sel)
            {
                MessageBox.Show("Sélectionnez un utilisateur.");
                return;
            }

            _draft.Remove(sel); // pas de DB ici
        }

        // BOUTON SAUVEGARDER
        private void button3_Click(object sender, EventArgs e)
        {
            using var db = new SchoolContext();

            var dbUsers = db.Utilisateurs.AsNoTracking().ToList();

            // A ajouter : ceux qui n'ont pas encore d'Id (nouveaux)
            var toAdd = _draft.Where(d => d.Id == 0).ToList();
            if (toAdd.Count > 0) db.Utilisateurs.AddRange(toAdd);

            // A supprimer : présents en DB mais plus dans la draft
            var toDelete = dbUsers.Where(dbU => !_draft.Any(d => d.Id == dbU.Id)).ToList();
            if (toDelete.Count > 0) db.Utilisateurs.RemoveRange(toDelete);

            // A mettre à jour : même Id, mais champs modifiés
            var toUpdate = _draft.Where(d =>
                d.Id != 0 &&
                dbUsers.Any(u => u.Id == d.Id &&
                                (u.Nom != d.Nom ||
                                 u.Prenom != d.Prenom ||
                                 u.Pseudo != d.Pseudo))
            ).ToList();

            if (toUpdate.Count > 0) db.UpdateRange(toUpdate);

            db.SaveChanges();

            // Recharger depuis la DB pour récupérer les Id auto et rafraîchir l’UI + menu
            LoadDraftFromDb();

            var allAfterSave = db.Utilisateurs.AsNoTracking().OrderBy(u => u.Pseudo).ToList();
            m.set_ComboBoxLogin(allAfterSave); // met à jour la ComboBox du menu

            MessageBox.Show("Modifications sauvegardées.");
        }
    }


}
