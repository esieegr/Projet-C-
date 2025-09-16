using Microsoft.VisualBasic.Devices;
using Microsoft.EntityFrameworkCore;

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
            db.Database.Migrate(); // crée la DB + applique les migrations


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new menu());
        }
    }
}