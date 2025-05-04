public class Jeu
{
    private bool isPartieEnCours = false;
    private MenuChoix menuPrincipal;

    // Méthodes
    public Jeu()
    {
        // Création du menu avec les options et le titre
        string titre = @"
====================================
      🍇 Le Jeu Viticole 🍇       
====================================
Utilisez les flèches ↑ ↓ pour naviguer, Entrée pour valider.

";
        List<string> options = new List<string>
            {
                "Nouvelle Partie",
                "Charger une Partie",
                "Règles du jeu",
                "Quitter"
            };

        menuPrincipal = new MenuChoix(options, titre);
    }

    public void Lancer()
    {
        while (!isPartieEnCours)
        {
            // Ici on utilise la méthode de la classe menu pour afficher. Cette méthode renvoie un int qui est le choix du joueur.
            int choix = menuPrincipal.Afficher();

            switch (choix)
            {
                case 0:
                    NouvellePartie();
                    break;
                case 1:
                    ChargerPartie();
                    break;
                case 2:
                    AfficherRegles();
                    break;
                case 3:
                    Quitter();
                    break;
                default:
                    Console.WriteLine("\nChoix invalide. Veuillez réessayer.\n");
                    break;
            }
        }
    }

    private void NouvellePartie()
    {
        Console.Clear();
        Console.WriteLine("\n=== Nouvelle Partie ===\n");
        Console.WriteLine("Partie initialisée avec succès !\n");
        isPartieEnCours = true;

        Joueur joueur = new Joueur("Nathan");
        Partie partieEnCours = new Partie(joueur);

        // Boucle principale du jeu
        bool enJeu = true;
        while (enJeu)
        {
            var affichage = new AffichageParcelle(partieEnCours.ParcelleEnCours!);
            int rang = affichage.AfficherAvecCurseur();
            int? colonneSelectionnee = affichage.AfficherDetailRangee(rang); // Backspace dans cette méthode = retour
            if (colonneSelectionnee != null)
            {
                int action = affichage.AfficherMenuAction(colonneSelectionnee.Value);
                Console.WriteLine($"Action {action} séléctionnée");
                Console.ReadKey();
            }

        }
    }

    private void ChargerPartie()
    {
        Console.Clear();
        Console.WriteLine("\n=== Charger Partie ===\n");
        Console.WriteLine("Fonctionnalité à venir.\n");
    }

    private void AfficherRegles()
    {
        Console.Clear();
        Console.WriteLine("\n=== Règles du Jeu ===\n");
        Console.ReadKey();
    }

    private void Quitter()
    {
        Console.WriteLine("\nMerci d'avoir joué. À bientôt !\n");
        Environment.Exit(0);
    }

}
