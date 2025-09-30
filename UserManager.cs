using Projet_C_.Models;

namespace Projet_C_
{
    public static class UserManager
    {
        private static class_utilisateur? _utilisateurConnecte;

        public static class_utilisateur? UtilisateurConnecte
        {
            get { return _utilisateurConnecte; }
        }

        public static bool EstConnecte => _utilisateurConnecte != null;

        public static void ConnecterUtilisateur(class_utilisateur utilisateur)
        {
            _utilisateurConnecte = utilisateur;
        }

        public static void DeconnecterUtilisateur()
        {
            _utilisateurConnecte = null;
        }
    }
}
