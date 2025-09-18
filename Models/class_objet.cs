using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C_.Models
{
    public class class_objet : class_articleType
    {
        public string Nom { get; set; } = "";
        public string type_objet { get; set; } = "";
        static int static_id_objet = 0;
        public Etat EtatObjet { get; set; } = new Etat();
        public int Id { get; set; }

        public enum Etat
        {
            Bon, Neuf, Usage
        }
        public bool disponible;

        public class_objet() { }

        public class_objet(string nom, string type_objet, bool disponible)
        {
            this.Nom = nom;
            this.type_objet = type_objet;
            Id = static_id_objet++;
            this.disponible = disponible;
        }
    }
}
