public class Partie
{
    public Joueur Joueur { get; set; }
    public List<Parcelle> Parcelles { get; set; } = new(); // Liste des parcelles que le joueur détiens
    public int Semaine { get; set; } = 1; // Numéro de la semaine en cours
    public Parcelle? ParcelleEnCours { get; set; }

    // Constructeur
    public Partie(Joueur joueur)
    {
        Joueur = joueur;
        InitialiserParcelles();

    }

    // Méthodes
    private void InitialiserParcelles()
    {
        var parcelle1 = new ParcelleArgileuse("Parcelle 1", 5, 4);
        Parcelles.Add(parcelle1);
        ParcelleEnCours = parcelle1;

        var affichage = new AffichageParcelle(ParcelleEnCours!);
        int rang = affichage.AfficherAvecCurseur();

        Console.WriteLine($"\nRang sélectionné : {rang + 1}");
    }




    public void Suivant()
    {
        Semaine++;
        // tu pourras aussi appeler les méthodes de mise à jour des plantes ici
    }
}

