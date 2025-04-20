public class Jeu
{
    private bool partieEnCours = false;
    private Menu menu = new Menu();

    public void Lancer()
    {
        menu = new Menu();

        while (!partieEnCours)
        {
            int choix = menu.AfficherMenuPrincipal(); // On récupère la selection du joueur.

            switch (choix)
            {
                case 0: // Nouvelle Partie
                    NouvellePartie();
                    break;
                case 1: // Charger Partie
                    ChargerPartie();
                    break;
                case 2: // Règles du jeu
                    AfficherRegles();
                    break;
                case 3: // Quitter
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
