using Microsoft.VisualBasic.Devices;
using Microsoft.EntityFrameworkCore;
using Projet_C_.Data;

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

            using var db = new SchoolContext();
            {
                db.Database.EnsureCreated();
                // ou db.Database.Migrate(); si tu utilises les migrations
            }
            System.Diagnostics.Debug.WriteLine($"BDD SQLite => {db.GetDatabasePath()}");


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new form_menu());
        }
    }
}