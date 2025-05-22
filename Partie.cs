public class Partie
{
    public Joueur? Joueur { get; set; }
    public List<Parcelle> Parcelles { get; set; } = new(); // Liste des parcelles que le joueur détiens
    public int Semaine { get; set; } = 1; // Numéro de la semaine en cours
    public GestionSaisons gestionSaisons = null!;
    public string SaisonActuelle { get; set; } = "";
    public Parcelle? ParcelleEnCours { get; set; }
    public MenuChoix ChoixTypeParcelle { get; set; } = new MenuChoix(new List<string> { "Argileuse", "Graveleux", "Calcaire" }, @"Quel type de parcelle voulez-vous créer pour commencer la partie ?
    "); // Création du menu

    private static readonly Random rnd = new Random();

    // Constructeur mis à jour pour nouvelle partie seulement
    public static Partie CreerNouvellePartie(Joueur joueur)
    {
        var p = new Partie();
        p.Joueur = joueur;

        // Initialise le gestionnaire avec 13 semaines/saison
        p.gestionSaisons = new GestionSaisons(13);
        // 2) Tire et stocke la saison de départ
        p.SaisonActuelle = p.gestionSaisons.SaisonDeDepart;
        Console.WriteLine($"Saison de départ : {p.SaisonActuelle}");

        int choix = p.ChoixTypeParcelle.Afficher();
        p.InitialiserParcelles(choix);
        p.AppliquerMeteo();
        return p;
    }

    public Partie() { }

    // Méthodes
    private void InitialiserParcelles(int typeDeParcelle)
    {
        Parcelle? parcelle = null;

        string nomParcelle = "";
        while (string.IsNullOrWhiteSpace(nomParcelle) || nomParcelle.Length > 30)
        {
            Console.Clear();
            Console.WriteLine("Comment s'appelle cette parcelle ? (max 30 caractères)");
            nomParcelle = Console.ReadLine()!;

            if (nomParcelle.Length > 30)
            {
                Console.WriteLine("❌ Le nom est trop long. Appuyez sur une touche pour réessayer.");
                Console.ReadKey();
            }
        }

        int largeur = 0;
        int hauteur = 0;

        while (largeur < 4 || largeur > 10)
        {
            Console.Clear();
            Console.Write("Entrez la largeur de la parcelle (4 à 10) : ");
            int.TryParse(Console.ReadLine(), out largeur);
        }

        while (hauteur < 4 || hauteur > 10)
        {
            Console.Clear();
            Console.Write("Entrez la hauteur de la parcelle (4 à 10) : ");
            int.TryParse(Console.ReadLine(), out hauteur);
        }

        if (typeDeParcelle == 0) // Argileuse
            parcelle = new ParcelleArgileuse(nomParcelle, largeur, hauteur);
        else if (typeDeParcelle == 1)
            parcelle = new ParcelleGraveleuse(nomParcelle, largeur, hauteur);
        else if (typeDeParcelle == 2)
            parcelle = new ParcelleCalcaire(nomParcelle, largeur, hauteur);

        if (parcelle != null)
        {
            Parcelles.Add(parcelle);
            ParcelleEnCours = parcelle;
        }
    }
    public void SemaineSuivante()
    {
        Semaine++;
        SaisonActuelle = gestionSaisons.GetSaison(Semaine);
        ParcelleEnCours!.ReinitialiserActions();
        foreach (var parcelle in Parcelles)
        {
            int hauteur = parcelle.Hauteur;
            int largeur = parcelle.Largeur;
            AppliquerMeteo();

            // Parcours ligne par ligne
            for (int y = 0; y < hauteur; y++)
                for (int x = 0; x < largeur; x++)
                {
                    var plante = parcelle.MatriceEtat[y, x];
                    if (plante == null)
                        continue;

                    if (parcelle.Pluie) // S'il pleut alors on arrose un peu
                        plante.Hydratation = Math.Min(100, plante.Hydratation + 20);

                    plante.AvancerSemaine(parcelle.Ensoleillement, parcelle.Temperature);

                    // Si elle est morte, on la retire de la grille
                    if (plante.Etat == Plante.EtatPlante.Morte)
                        parcelle.MatriceEtat[y, x] = null;
                }
        }
    }
    public void Planter(Plante plante, int ligne, int colonne)
    {
        if (ParcelleEnCours != null)
        {
            if (ParcelleEnCours.MatriceEtat[ligne, colonne] == null)
            {
                ParcelleEnCours.MatriceEtat[ligne, colonne] = plante;
                plante.Parcelle = ParcelleEnCours;
                ParcelleEnCours!.UtiliserAction();
                Console.WriteLine($"🌱 {plante.Nom} planté en ({ligne + 1}, {colonne + 1}) !");
            }
            else
            {
                Console.WriteLine("❌ Emplacement déjà occupé.");
            }
        }
    }

    public void ToutRécolter(Joueur joueur, Parcelle parcelle)
    {
        for (int y = 0; y < parcelle.Hauteur; y++)
        {
            for (int x = 0; x < parcelle.Largeur; x++)
            {
                var plante = parcelle.MatriceEtat[y, x];
                if (plante != null && plante.Croissance >= 100)
                {
                    plante.Recolter(joueur, plante);
                }
            }
        }
    }

    public void AppliquerMeteo()
    {
        foreach (var parcelle in Parcelles)
        {
            // 1) Pluie à 30%
            parcelle.Pluie = rnd.NextDouble() < 0.3;

            // 2) Ensoleillement
            int ensoleillement = parcelle.Pluie
                ? 0
                : rnd.Next(30, 101);

            // 3) Température selon saison
            int tempMin, tempMax;
            switch (SaisonActuelle)
            {
                case "Été":
                    tempMin = 20; tempMax = 37; break;
                case "Hiver":
                    tempMin = -4; tempMax = 10; break;
                case "Printemps":
                    tempMin = 8; tempMax = 20; break;
                case "Automne":
                    tempMin = 10; tempMax = 18; break;
                default:
                    tempMin = 5; tempMax = 25; break;
            }
            int temperature = rnd.Next(tempMin, tempMax + 1);

            // 4) Stocke pour l’affichage
            parcelle.Ensoleillement = ensoleillement;
            parcelle.Temperature = temperature;
        }
    }
}

