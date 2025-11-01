using Microsoft.EntityFrameworkCore;
using Projet_C_.Data;
using Projet_C_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Windows.Forms;

namespace Projet_C_
{
    /// <summary>
    /// Formulaire permettant à l'utilisateur de parcourir le marché et de créer des offres d'échange.
    /// </summary>
    public partial class form_cr_offres : Form
    {
        private readonly form_menu _menu;
        private readonly BindingList<MarketVM> _market = new();
        private bool _busy = false;

        /// <summary>
        /// Modèle d'affichage pour les objets du marché.
        /// </summary>
        private sealed class MarketVM
        {
            public int IdObjet { get; set; }
            public int ProprietaireId { get; set; }
            public string Proprietaire { get; set; } = "";
            public string Nom { get; set; } = "";
            public string Type { get; set; } = "";

            public string Label => 
                $"{(string.IsNullOrEmpty(Nom) ? "?" : Nom)} — par " +
                $"{(string.IsNullOrEmpty(Proprietaire) ? "?" : Proprietaire)} " +
                $"({(string.IsNullOrEmpty(Type) ? "?" : Type)})";
        }

        public form_cr_offres(form_menu m)
        {
            InitializeComponent();
            _menu = m;

            // Configuration de la ListBox
            listBox_marche.DataSource = _market;
            listBox_marche.DisplayMember = "Label";
            listBox_marche.ValueMember = "IdObjet";

            // Attachement des événements
            button_recherche.Click += button_recherche_Click;
            button_faire_offre.Click += button_faire_offre_Click;
        }

        private int CurrentUserId => _menu?.CurrentUser?.Id ?? 0;

        /// <summary>
        /// Actualise le marché en chargeant les objets disponibles des autres utilisateurs.
        /// </summary>
        public async System.Threading.Tasks.Task RefreshMarketAsync()
        {
            await LoadMarketAsync();
        }

        private async System.Threading.Tasks.Task LoadMarketAsync()
        {
            if (_busy)
                return;

            _busy = true;

            try
            {
                string searchQuery = textBox_recherche?.Text?.Trim() ?? "";
                int currentUserId = CurrentUserId;

                using (var db = new SchoolContext())
                {
                    await DbSetup.RunAsync(db);

                    using (DbConnection conn = db.Database.GetDbConnection())
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            await conn.OpenAsync();

                        var cmd = conn.CreateCommand();
                        cmd.CommandText = BuildMarketQuery(searchQuery);

                        var pUid = cmd.CreateParameter();
                        pUid.ParameterName = "@uid";
                        pUid.Value = currentUserId;
                        cmd.Parameters.Add(pUid);

                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            var pSearch = cmd.CreateParameter();
                            pSearch.ParameterName = "@q";
                            pSearch.Value = $"%{searchQuery}%";
                            cmd.Parameters.Add(pSearch);
                        }

                        var rows = await ExecuteQueryAsync(cmd);
                        UpdateMarketDisplay(rows);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement du marché : {ex.Message}", 
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _busy = false;
            }
        }

        private string BuildMarketQuery(string searchQuery)
        {
            string baseQuery = @"
SELECT o.Id, o.Nom, IFNULL(o.type_objet,'') AS Type, o.proprietaire_id,
       IFNULL((SELECT u.Pseudo FROM Utilisateurs u WHERE u.Id = o.proprietaire_id),'') AS Pseudo
FROM Objets o
WHERE IFNULL(o.disponible,1)=1
  AND o.proprietaire_id IS NOT NULL
  AND (@uid = 0 OR o.proprietaire_id <> @uid)";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                baseQuery += @"
  AND (o.Nom LIKE @q 
       OR IFNULL(o.type_objet,'') LIKE @q 
       OR IFNULL((SELECT u.Pseudo FROM Utilisateurs u WHERE u.Id=o.proprietaire_id),'') LIKE @q)";
            }

            baseQuery += "\nORDER BY o.Nom LIMIT 500;";
            return baseQuery;
        }

        private async System.Threading.Tasks.Task<List<MarketVM>> ExecuteQueryAsync(DbCommand cmd)
        {
            var rows = new List<MarketVM>();

            using (var rd = await cmd.ExecuteReaderAsync())
            {
                while (await rd.ReadAsync())
                {
                    rows.Add(new MarketVM
                    {
                        IdObjet = rd.GetInt32(0),
                        Nom = rd.IsDBNull(1) ? "" : rd.GetString(1),
                        Type = rd.IsDBNull(2) ? "" : rd.GetString(2),
                        ProprietaireId = rd.IsDBNull(3) ? 0 : rd.GetInt32(3),
                        Proprietaire = rd.IsDBNull(4) ? "" : rd.GetString(4)
                    });
                }
            }

            return rows;
        }

        private void UpdateMarketDisplay(List<MarketVM> rows)
        {
            _market.RaiseListChangedEvents = false;
            _market.Clear();

            foreach (var row in rows)
                _market.Add(row);

            _market.RaiseListChangedEvents = true;

            // Rebinder la ListBox pour garantir l'affichage
            listBox_marche.DataSource = null;
            listBox_marche.DataSource = _market;
            listBox_marche.DisplayMember = "Label";
            listBox_marche.ValueMember = "IdObjet";
        }

        private async void button_recherche_Click(object sender, EventArgs e)
        {
            await LoadMarketAsync();
        }

        private void button_faire_offre_Click(object sender, EventArgs e)
        {
            if (listBox_marche.SelectedItem is MarketVM vm)
            {
                MessageBox.Show($"Vous avez sélectionné : {vm.Label}", 
                    "Offre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // TODO: Implémenter la création d'offre
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un objet dans la liste.", 
                    "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
