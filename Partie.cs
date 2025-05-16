public class Partie
{
    public Joueur? Joueur { get; set; }
    public List<Parcelle> Parcelles { get; set; } = new(); // Liste des parcelles que le joueur détiens
    public int Semaine { get; set; } = 1; // Numéro de la semaine en cours
    private GestionSaisons gestionSaisons = null!;

    public string SaisonActuelle { get; private set; } = "";
    public Parcelle? ParcelleEnCours { get; set; }
    public MenuChoix ChoixTypeParcelle { get; set; } = new MenuChoix(new List<string> { "Argileuse", "Graveleux", "Calcaire" }, @"Quel type de parcelle voulez-vous créer pour commencer la partie ?
    "); // Création du menu

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
        return p;
    }


    // Constructeur sans logique pour charger partie déjà commencée
    // Utilisé pour la désérialisation de la sauvegarde
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
        { }
        //parcelle = new ParcelleGraveleuse(nomParcelle, largeur, hauteur); // à créer
        else if (typeDeParcelle == 2)
        { }
        // parcelle = new ParcelleCalcaire(nomParcelle, largeur, hauteur); // à créer

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
    }
    public void Planter(Plante plante, int ligne, int colonne)
    {
        if (ParcelleEnCours != null)
        {
            if (ParcelleEnCours.MatriceEtat[ligne, colonne] == null)
            {
                ParcelleEnCours.MatriceEtat[ligne, colonne] = plante;
                Joueur!.UtiliserAction();
                Console.WriteLine($"🌱 {plante.Nom} planté en ({ligne + 1}, {colonne + 1}) !");
            }
            else
            {
                Console.WriteLine("❌ Emplacement déjà occupé.");
            }
        }
    }
}

