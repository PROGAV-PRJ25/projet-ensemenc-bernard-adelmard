public class Partie
{
    public Joueur Joueur { get; set; }
    public List<Parcelle> Parcelles { get; set; } = new(); // Liste des parcelles que le joueur détiens
    public int Semaine { get; set; } = 1; // Numéro de la semaine en cours
    public Parcelle? ParcelleEnCours { get; set; }
    public MenuChoix ChoixTypeParcelle { get; set; } = new MenuChoix(new List<string> { "Argileuse", "Graveleux", "Calcaire" }, @"Quel type de parcelle voulez-vous créer pour commencer la partie ?
    "); // Création du menu

    // Constructeur
    public Partie(Joueur joueur)
    {
        Joueur = joueur;
        int choix = ChoixTypeParcelle.Afficher();
        InitialiserParcelles(choix, "Parcelle 1");
    }

    // Méthodes
    private void InitialiserParcelles(int typeDeParcelle, string nomParcelle)
    {
        Parcelle? parcelle = null; // On lui donne la valeur null sinon Parcelle.Add n'est pas content

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

        if (typeDeParcelle == 0) // Argileuse = 0, Graveleux = 1, Calcaire = 2
        {
            parcelle = new ParcelleArgileuse(nomParcelle, largeur, hauteur);
        }
        else if (typeDeParcelle == 1)
        {

        }
        else if (typeDeParcelle == 2)
        {
            // A faire
        }

        Parcelles.Add(parcelle!);
        ParcelleEnCours = parcelle;

        var affichage = new AffichageParcelle(ParcelleEnCours!);
        int rang = affichage.AfficherAvecCurseur();

        Console.WriteLine($"\nRang sélectionné : {rang + 1}");
    }

    public void Suivant()
    {
        Semaine++;
    }
}

