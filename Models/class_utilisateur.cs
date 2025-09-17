using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C_.Models
{
    public class class_utilisateur
    {
        public int Id { get; set; }
        static int static_id_Utilisateur = 0;
        public string nom { get; set; } = "";
        public string prenom { get; set; } = "";
        public string pseudo { get; set; } = "";
        public List<class_objet> liste_objets = new List<class_objet>();
        public int points = 0;

        public class_utilisateur(string nom, string prenom, string pseudo)
        {
            Id = static_id_Utilisateur++;
            this.nom = nom;
            this.prenom = prenom;
            this.pseudo = pseudo;

        }
        public override string ToString() => pseudo;

    }
}
