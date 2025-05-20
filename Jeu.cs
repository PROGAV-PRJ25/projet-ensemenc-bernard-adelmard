public class Jeu
{
    private bool isPartieEnCours = false;
    public bool enJeu = false;
    public bool isChargement = false;
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
        isPartieEnCours = true;

        string nomSauvegarde;

        do
        {
            Console.Write("Entrez le nom de votre partie : ");
            nomSauvegarde = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(nomSauvegarde) || nomSauvegarde.Length > 30)
            {
                Console.WriteLine("⛔ Nom invalide. Veuillez entrer un nom non vide et de 30 caractères maximum.\n");
            }

        } while (string.IsNullOrWhiteSpace(nomSauvegarde) || nomSauvegarde.Length > 30);

        Joueur joueur = new Joueur(nomSauvegarde);
        Partie partieEnCours = Partie.CreerNouvellePartie(joueur); //seulement pour nouvelle partie

        // Boucle principale du jeu
        BoucleJeu(partieEnCours, joueur, nomSauvegarde);
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

            partieChargee.gestionSaisons = new GestionSaisons(13);
            partieChargee.SaisonActuelle = partieChargee.gestionSaisons.GetSaison(partieChargee.Semaine);

            isPartieEnCours = true;
            BoucleJeu(partieChargee, partieChargee.Joueur!, nomSauvegardeChoisi);
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

    private void BoucleJeu(Partie partie, Joueur joueur, string nomSauvegarde)
    {
        enJeu = true;
        int dernierRang = 0;

        while (enJeu)
        {
            if (isChargement)
            {
                AffichageChargement.Afficher(partie.Semaine);
                partie.SemaineSuivante();
                isChargement = false;
                continue;
            }

            //Interface de la parcelle
            var affichage = new AffichageParcelle(partie.ParcelleEnCours!,
                                                  joueur,
                                                  partie,
                                                  this);

            int rang = affichage.Afficher(dernierRang);
            dernierRang = rang;

            if (isChargement)
                continue;

            int? col = affichage.AfficherDetailRangee(rang);
            if (col is int c)
            {
                string action = affichage.AfficherMenuActionDetail(c);
                switch (action)
                {
                    case "Arroser":
                        // Récupère la plante sélectionnée
                        var plante = partie.ParcelleEnCours!.MatriceEtat[rang, c];
                        if (plante != null && joueur.ActionsDisponibles > 0)
                        {
                            plante.Arroser();               
                            joueur.UtiliserAction();
                            Console.WriteLine("💧 Vous avez arrosé la plante !");
                        }
                        else
                        {
                            Console.WriteLine("❌ Impossible d'arroser (plus d'actions ou pas de plante).");
                        }
                        Console.ReadKey();
                        break;

                    case "Traiter":
                        // Mettre état sur Saine
                        break;
                }
            }
            //SauvegardeManager.Sauvegarder(partie, nomSauvegarde);
        }
    }
}
