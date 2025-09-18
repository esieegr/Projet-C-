using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C_.Models
{
    public class class_utilisateur
    {
        public int Id { get; set; }           // PK EF
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Pseudo { get; set; } = "";

        public override string ToString() => Pseudo;
    }

}
