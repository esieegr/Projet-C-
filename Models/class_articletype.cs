using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C_.Models
{
    public class class_articleType
    {

        private string nom = "Aucun";
        public int quantite { get; set; }
        //private TypeArticle type;
        private string[] types = { "alimentaire", "droguerie", "habillement", "loisir" };
        public float prix { get; set; }

        public enum TypeArticle
        {
            Alimentaire,
            Droguerie,
            Habillement,
            Loisir
        }

        public class_articleType() { }


        public void afficher()
        {
            Console.WriteLine("Type : " + type + " Nom : " + nom + " quantite : " + quantite);
        }

        public void ajouter()
        {
            quantite++;
        }
        public void retirer()
        {
            quantite--;
        }

        public void nouveau()
        {
            Console.WriteLine("Sélectionnez un type :");

            var values = Enum.GetValues(typeof(TypeArticle));

            int i = 0;
            foreach (TypeArticle t in values)
            {
                Console.WriteLine($"{i}) {t}");
                i++;
            }

            int answer = Convert.ToInt32(Console.ReadLine());

            type = (TypeArticle)answer;


            Console.WriteLine("Entrez le nom : ");

            nom = Console.ReadLine();

            Console.WriteLine("Entrez une quantite : ");
            quantite = Convert.ToInt32(Console.ReadLine());

        }

        public TypeArticle type { get; set; }


        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < types.Length)
                    return types[index];
                return "Invalide";
            }
            set
            {
                if (index >= 0 && index < types.Length)
                    types[index] = value;
            }
        }


        public class_articleType(string nom, float prix, int quantite, TypeArticle type)
        {
            this.nom = nom;
            this.prix = prix;
            this.quantite = quantite;
            this.type = type;
        }
    }
}
