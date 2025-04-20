public class Jeu
{
    private bool partieEnCours = false;
    private MenuPrincipal menuPrincipal;

    // Méthodes
    public Jeu()
    {
        menuPrincipal = new MenuPrincipal();
    }

    public void Lancer()
    {
        while (!partieEnCours)
        {
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

        BouclePrincipale();
    }

    private void NouvellePartie()
    {
        Console.Clear();
        Console.WriteLine("\n=== Nouvelle Partie ===\n");
        Console.WriteLine("Partie initialisée avec succès !\n");
        partieEnCours = true;
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

    private void BouclePrincipale()
    {
        Console.WriteLine("\nLe jeu commence ! (Boucle principale à implémenter)\n");
    }
}

