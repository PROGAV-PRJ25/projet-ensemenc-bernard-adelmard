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
============================================================
                  🍇 Le Jeu Viticole 🍇       
============================================================
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
        Console.WriteLine("\n=== Règles du Jeu Viticole ===\n");
        Console.WriteLine("🎯 Objectif :");
        Console.WriteLine("  • Faire grandir vos cépages, récolter du raisin et acheter de nouvelles parcelles.");
        Console.WriteLine();
        Console.WriteLine("⌛ Déroulement :");
        Console.WriteLine("  • Chaque semaine :");
        Console.WriteLine("    – Météo aléatoire (pluie / ensoleillement / température).");
        Console.WriteLine("    – Vos plantes consomment de l’eau et poussent selon leurs besoins.");
        Console.WriteLine("    – Vos plantes ont des bonus positifs ou négatifs en fonctions da la satisfaction de chaque besoins.");
        Console.WriteLine("    – Vous disposez d'un nombre limité d'actions.");
        Console.WriteLine();
        Console.WriteLine("🛠️ Actions disponibles :");
        Console.WriteLine("  • Planter   : Choisissez un cépage et un emplacement vide.");
        Console.WriteLine("  • Arroser   : +70% hydratation (+ 20% si pluie).");
        Console.WriteLine("  • Traiter   : Soigne une plante malade.");
        Console.WriteLine("  • Récolter  : Si croissance = 100%, vous gagnez des raisins.");
        Console.WriteLine("  • Tout récolter : Récolte globale de la parcelle.");
        Console.WriteLine("  • Acheter   : Dépensez des raisins pour une nouvelle parcelle.");
        Console.WriteLine();
        Console.WriteLine("🌦️ Saison & météo :");
        Console.WriteLine("  • 1 saison = 13 semaines (Printemps, Été, Automne, Hiver).");
        Console.WriteLine("  • Vos cépages ont une fenêtre optimale de plantation.");
        Console.WriteLine();
        Console.WriteLine("⚠️ Mode Urgence (attaques d’oiseaux) :");
        Console.WriteLine("  • QTE rapide à réussir sous peine de perdre 50% de vos récoltes.");
        Console.WriteLine();
        Console.WriteLine("💀​ Conditions de survie d'une plante :");
        Console.WriteLine("  • Si 50% de ses besoins ne sont pas respectés, la planate meurt.");
        Console.WriteLine("  • Si la plante est malade ou déséchée depuis 3 jours consécutifs, elle meurt.");
        Console.WriteLine();
        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
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
                AffichageChargement.Afficher(partie.Semaine, partie.ParcelleEnCours!);
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
            if (rang == -1)
            {
                string actionGen = affichage.AfficherMenuActionGeneral();
                if (actionGen == "Voir toutes les parcelles (Acheter)")
                {
                    partie.AfficherToutesLesParcelles(joueur);
                    continue;
                }
                if (actionGen == "Passer à la semaine suivante")
                {
                    isChargement = true;
                    continue;
                }
                if (actionGen == "Tout récolter")
                {
                    partie.ToutRécolter(joueur, partie.ParcelleEnCours!);
                    continue;
                }
                if (actionGen == "Sauvegarder la partie")
                {
                    SauvegardeManager.Sauvegarder(partie, nomSauvegarde);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Partie sauvegardée avec succès !");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    continue;
                }
                if (actionGen == "Retour au menu principal")
                {
                    enJeu = false;
                    isPartieEnCours = false;
                    return;
                }
                if (actionGen == "Planter")
                    {
                        affichage.AfficherPlantage();
                        continue;
                    }
                rang = 0;
            }
            dernierRang = rang;

            if (isChargement)
                continue;

            int? col = affichage.AfficherDetailRangee(rang);
            if (col is int c)
            {
                string action = affichage.AfficherMenuActionDetail(c);
                // Récupère la plante sélectionnée
                var plante = partie.ParcelleEnCours!.MatriceEtat[rang, c];
                switch (action)
                {
                    case "Arroser":
                        if (plante != null)
                            plante.Arroser(partie.ParcelleEnCours);
                        break;

                    case "Traiter":
                        if (plante != null)
                            plante.Traiter(partie.ParcelleEnCours);
                        break;
                    case "Récolter":
                        if (plante != null)
                        {
                            plante.Recolter(joueur, plante);
                        }

                        break;
                }
            }
        }
    }
}