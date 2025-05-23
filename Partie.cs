using System.Runtime.InteropServices;

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

    public int CoutSurface = 5;

    private static readonly Random rnd = new Random();

    // Constructeur pour nouvelle partie seulement
    public static Partie CreerNouvellePartie(Joueur joueur)
    {
        var p = new Partie();
        p.Joueur = joueur;

        // Initialise le gestionnaire avec 13 semaines/saison
        p.gestionSaisons = new GestionSaisons(13);
        // Tire et stocke la saison de départ
        p.SaisonActuelle = p.gestionSaisons.SaisonDeDepart;
        Console.WriteLine($"Saison de départ : {p.SaisonActuelle}");

        int choix = p.ChoixTypeParcelle.Afficher();
        p.InitialiserParcelles(choix);
        p.AppliquerMeteo();
        return p;
    }

    public Partie() { }

    // Méthodes
    private Parcelle InitialiserParcelles(int typeDeParcelle)
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
        else
            parcelle = new ParcelleCalcaire(nomParcelle, largeur, hauteur);

        if (parcelle != null)
        {
            Parcelles.Add(parcelle);
            ParcelleEnCours = parcelle;
        }
        return parcelle!;

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
    private void AcheterParcelle(Joueur joueur)
    {
        Console.Clear();
        Console.WriteLine("== Achat d'une nouvelle parcelle ==");
        // Choix du type de sol
        int type = new MenuChoix(
            new List<string> { "Argileuse", "Graveleux", "Calcaire" }, "Type de sol :").Afficher();

        // Nom de la parcelle
        string nom;
        do
        {
            Console.Clear();
            Console.Write("Nom de la parcelle (max 30 chars) : ");
            nom = Console.ReadLine()?.Trim() ?? "";
        } while (string.IsNullOrWhiteSpace(nom) || nom.Length > 30);

        // 3) Dimensions
        int largeur, hauteur;
        do
        {
            Console.Write("Largeur (4–10) : ");
        } while (!int.TryParse(Console.ReadLine(), out largeur) || largeur < 4 || largeur > 10);
        do
        {
            Console.Write("Hauteur (4–10) : ");
        } while (!int.TryParse(Console.ReadLine(), out hauteur) || hauteur < 4 || hauteur > 10);

        // 4) Calcul du coût
        int cout = largeur * hauteur * CoutSurface;
        if (joueur.NombreDeRaisins < cout)
        {
            Console.WriteLine($"❌ Il vous faut {cout} NombreDeRaisins, vous n'en avez que {joueur.NombreDeRaisins}.");
            Console.ReadKey();
            return;
        }

        // 5) Débit et création
        joueur.NombreDeRaisins -= cout;
        Parcelle nouvelle;
        switch (type)
        {
            case 0:
                nouvelle = new ParcelleArgileuse(nom, largeur, hauteur);
                break;
            case 1:
                nouvelle = new ParcelleGraveleuse(nom, largeur, hauteur);
                break;
            default:
                nouvelle = new ParcelleCalcaire(nom, largeur, hauteur);
                break;
        }

        Parcelles.Add(nouvelle);
        ParcelleEnCours = nouvelle;

        Console.WriteLine($"✅ Parcelle achetée pour {cout} NombreDeRaisins !");
        Console.ReadKey();
    }

    public void AfficherToutesLesParcelles(Joueur joueur)
    {
        var options = new List<string>();
        foreach (var parcelle in Parcelles)
        {
            options.Add(
                $"{parcelle.BlocTerre} {parcelle.Nom}  –  Sol : {parcelle.TypeSol}  –  Taille : {parcelle.Largeur}×{parcelle.Hauteur}"
            );
        }
        options.Add("Acheter une parcelle");

        var menu = new MenuChoix(
            options,
            "Sélectionnez une parcelle ou achetez-en une nouvelle :"
        );
        int choix = menu.Afficher();

        if (choix < Parcelles.Count)
            ParcelleEnCours = Parcelles[choix];
        else
            AcheterParcelle(joueur);
    }
}

