using Projet_C_.Data;
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
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using static Projet_C_.Models.class_objet;

namespace Projet_C_
{
    public partial class form_inventaire : Form
    {
        private readonly form_menu _menu; // si besoin de notifier le menu après save (optionnel)
        private BindingList<class_objet> _draft = new();

        public form_inventaire(form_menu m)
        {
            InitializeComponent();
            _menu = m;

            // Préparer les combos
            comboBox_etat.DataSource = Enum.GetValues(typeof(Etat));
            comboBox_type.Items.Clear();
            comboBox_type.Items.AddRange(new[] { "Livre", "Jeu", "Vêtement", "Outil" });
            comboBox_type.SelectedIndex = 0;
            comboBox_etat.SelectedIndex = 0;

            Load += (_, __) => LoadDraftFromDb();
        }

        private void LoadDraftFromDb()
        {
            using var db = new SchoolContext();
            var objets = db.Objets
                           .AsNoTracking()
                           .OrderBy(o => o.Nom)
                           .ToList();

            _draft = new BindingList<class_objet>(objets);

            listBox_inventaire.DataSource = null;
            listBox_inventaire.DisplayMember = "Nom";
            listBox_inventaire.ValueMember = "Id";
            listBox_inventaire.DataSource = _draft;
        }

        // BOUTON AJOUTER (mémoire uniquement)
        private void button_add_Click(object sender, EventArgs e)
        {
            var nom = textBox1.Text.Trim();
            var typeObjet = comboBox_type.SelectedItem?.ToString() ?? "";
            var etat = (Etat)(comboBox_etat.SelectedItem ?? Etat.Bon);
            var dispo = comboBox_etat.SelectedItem;

            if (string.IsNullOrWhiteSpace(nom))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }

            _draft.Add(new class_objet
            {
                Nom = nom,
                type_objet = typeObjet,
                EtatObjet = etat,
                disponible = true
            });

            // reset
            textBox1.Clear();
            //comboBox_etat.Select = false;
            textBox1.Focus();
        }

        

        private void button_sauvegarder_Click(object sender, EventArgs e)
        {
            using var db = new SchoolContext();

            var dbObjets = db.Objets.AsNoTracking().ToList();

            // Nouveaux (Id == 0)
            var toAdd = _draft.Where(d => d.Id == 0).ToList();
            if (toAdd.Count > 0) db.Objets.AddRange(toAdd);

            // Supprimés (présents en DB mais plus dans le draft)
            var toDelete = dbObjets.Where(dbO => !_draft.Any(d => d.Id == dbO.Id)).ToList();
            if (toDelete.Count > 0) db.Objets.RemoveRange(toDelete);

            // Modifiés (même Id, champs différents)
            var toUpdate = _draft.Where(d =>
                d.Id != 0 &&
                dbObjets.Any(o => o.Id == d.Id &&
                                 (o.Nom != d.Nom ||
                                  o.type_objet != d.type_objet ||
                                  o.EtatObjet != d.EtatObjet ||
                                  o.disponible != d.disponible))
            ).ToList();

            if (toUpdate.Count > 0) db.UpdateRange(toUpdate);

            db.SaveChanges();

            LoadDraftFromDb(); // recharge depuis la BDD (Id auto etc.)
            MessageBox.Show("Inventaire sauvegardé.");
        }

        // BOUTON SUPPRIMER (mémoire uniquement)
        private void button_supprimer_objet_Click(object sender, EventArgs e)
        {
            if (listBox_inventaire.SelectedItem is not class_objet sel)
            {
                MessageBox.Show("Sélectionnez un objet.");
                return;
            }
            _draft.Remove(sel);
        }
    }
}
