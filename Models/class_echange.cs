using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C_.Models
{
    public class class_echange : class_articleType
    {
        public int Id { get; set; }
        public int utilisateur_proposant { get; set; }
        public int utilisateur_receveur { get; set; }
        public int objet_propose { get; set; }
        public int objet_demande { get; set; }
        enum etat_echange
        {
            accepte, refuse, en_attente
        }
    

    void accepter()
    {

    }

    void refuser()
    {
        
    }
    }
}
