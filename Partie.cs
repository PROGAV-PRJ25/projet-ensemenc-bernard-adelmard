public class Partie
{
    public Joueur Joueur { get; set; }
    public List<Parcelle> Parcelles { get; set; } = new(); // Liste des parcelles que le joueur détiens
    public int Semaine { get; set; } = 1; // Numéro de la semaine en cours
    public Parcelle? ParcelleEnCours { get; set; }
    public MenuChoix ChoixTypeParcelle { get; set; } = new MenuChoix(new List<string> { "Argileuse", "Graveleux", "Calcaire" }, @"Quel type de parcelle voulez-vous créer pour commencer la partie ?
    "); // Création du menu

    // Constructeur mis à jour pour nouvelle partie seulement
    public static Partie CreerNouvellePartie(Joueur joueur)
    {
        Partie p = new Partie();
        p.Joueur = joueur;

        int choix = p.ChoixTypeParcelle.Afficher();
        p.InitialiserParcelles(choix, "Parcelle 1");

        return p;
    }


    // Constructeur sans logique pour charger partie déjà commencée
    // Utilisé pour la désérialisation de la sauvegarde
    public Partie() { }


    // Méthodes
    private void InitialiserParcelles(int typeDeParcelle, string nomParcelle)
    {
        Parcelle? parcelle = null;

        int largeur = 0;
        int hauteur = 0;

        while (largeur < 3 || largeur > 10)
        {
            Console.Clear();
            Console.Write("Entrez la largeur de la parcelle (3 à 10) : ");
            int.TryParse(Console.ReadLine(), out largeur);
        }

        while (hauteur < 3 || hauteur > 10)
        {
            Console.Clear();
            Console.Write("Entrez la hauteur de la parcelle (3 à 10) : ");
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
            // Ajout de cépages de test dans la ligne 1
            if (parcelle.Hauteur > 1 && parcelle.Largeur >= 4)
            {
                parcelle.MatriceEtat[0, 0] = new CepageMerlot { Croissance = 25, Etat = Cepage.EtatCepage.Saine };
                parcelle.MatriceEtat[1, 1] = new CepageMerlot { Croissance = 50, Etat = Cepage.EtatCepage.Malade };
                parcelle.MatriceEtat[1, 2] = new CepageMerlot { Croissance = 100, Etat = Cepage.EtatCepage.Morte };
                parcelle.MatriceEtat[1, 3] = new CepageMerlot { Croissance = 10, Etat = Cepage.EtatCepage.Desechee };
            }

            Parcelles.Add(parcelle);
            ParcelleEnCours = parcelle;
        }
    }


    public void Suivant()
    {
        Semaine++;
    }

    public void Planter()
    { }
}

