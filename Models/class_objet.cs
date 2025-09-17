using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C_.Models
{
    public class class_objet
    {
        public string nom { get; set; } = "";
        public string type_objet { get; set; } = "";
        static int static_id_objet = 0;
        public int Id { get; set; }
        enum etat
        {
            bon, mauvais, usage
        }
        bool disponible;

        public class_objet() { }

        public class_objet(string nom, string type_objet, bool disponible)
        {
            this.nom = nom;
            this.type_objet = type_objet;
            Id = static_id_objet++;
            this.disponible = disponible;
        }
    }
}
