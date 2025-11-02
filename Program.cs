using Microsoft.VisualBasic.Devices;
using Microsoft.EntityFrameworkCore;
using Projet_C_.Data;
using Projet_C_.Models;
using System;
using System.Windows.Forms;

namespace Projet_C_
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitialiserBaseDeDonnees();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            bool continuerApp = true;

            while (continuerApp)
            {
                // Afficher le formulaire de connexion
                using (var formConnexion = new form_connexion())
                {
                    if (formConnexion.ShowDialog() == DialogResult.OK)
                    {
                        class_utilisateur? utilisateurConnecte = formConnexion.UtilisateurConnecte;

                        if (utilisateurConnecte != null)
                        {
                            // Afficher le menu principal
                            using (var formMenu = new form_menu(utilisateurConnecte))
                            {
                                var resultat = formMenu.ShowDialog();

                                // Si DialogResult.Retry, c'est une déconnexion -> reboucler
                                // Sinon, quitter l'application
                                continuerApp = (resultat == DialogResult.Retry);
                            }
                        }
                        else
                        {
                            continuerApp = false;
                        }
                    }
                    else
                    {
                        // L'utilisateur a annulé la connexion
                        continuerApp = false;
                    }
                }
            }
        }

        private static void InitialiserBaseDeDonnees()
        {
            using var db = new SchoolContext();
            db.Database.EnsureCreated();
            // ou db.Database.Migrate(); si tu utilises les migrations
            System.Diagnostics.Debug.WriteLine($"BDD SQLite => {db.GetDatabasePath()}");
        }
    }
}