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

        Console.Write("Entrez votre nom : ");
        string nomSauvegarde = Console.ReadLine()?.Trim() ?? "joueur";

        Joueur joueur = new Joueur(nomSauvegarde);
        Partie partieEnCours = Partie.CreerNouvellePartie(joueur); //seulement pour nouvelle partie

        // Boucle principale du jeu
        bool enJeu = true;

        int dernierRang = 0;

        while (enJeu)
        {
            var affichage = new AffichageParcelle(partieEnCours.ParcelleEnCours!);
            int rang = affichage.AfficherAvecCurseur(dernierRang);
            dernierRang = rang; // Pour revenir au rang avant d'être entré dans le mennu
            int? colonneSelectionnee = affichage.AfficherDetailRangee(rang);

            if (colonneSelectionnee != null)
            {
                string action = affichage.AfficherMenuActionDetail(colonneSelectionnee.Value);
                Console.WriteLine($"Action {action} sélectionnée");
                Console.ReadKey();
            }


            // SAUVEGARDE de la partie
            SauvegardeManager.Sauvegarder(partieEnCours, nomSauvegarde);

        }
    }

    private void ChargerPartie()
    {
        Console.Clear();
        Console.WriteLine("\n=== Chargement de la Partie ===\n");

        var nomsSauvegardes = SauvegardeManager.ListerSauvegardesDisponibles();

        if (nomsSauvegardes.Count == 0)
        {
            Console.WriteLine("Aucune sauvegarde disponible.");
            Console.ReadKey();
            return;
        }

        MenuChoix menuSauvegardes = new MenuChoix(nomsSauvegardes, "Choisissez une sauvegarde :");
        int index = menuSauvegardes.Afficher();
        string nomSauvegardeChoisi = nomsSauvegardes[index];

        var partieChargee = SauvegardeManager.Charger(nomSauvegardeChoisi);

        if (partieChargee != null)
        {
            isPartieEnCours = true;

            int dernierRang = 0;
            while (true)
            {
                var affichage = new AffichageParcelle(partieChargee.ParcelleEnCours!);
                int rang = affichage.AfficherAvecCurseur(dernierRang);
                dernierRang = rang;
                int? colonneSelectionnee = affichage.AfficherDetailRangee(rang);

                if (colonneSelectionnee != null)
                {
                    string action = affichage.AfficherMenuActionDetail(colonneSelectionnee.Value);
                    Console.WriteLine($"Action {action} sélectionnée");
                    Console.ReadKey();
                }

                SauvegardeManager.Sauvegarder(partieChargee, nomSauvegardeChoisi);
            }
        }
        else
        {
            Console.WriteLine("Erreur lors du chargement.");
            Console.ReadKey();
        }
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
