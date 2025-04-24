public class Partie
{
    public Joueur Joueur { get; set; }
    public List<Parcelle> Parcelles { get; set; } = new();
    public int Semaine { get; set; } = 1;

    public Partie(Joueur joueur)
    {
        Joueur = joueur;
        var parcelle1 = new ParcelleArgileuse("Parcelle 1", 5, 4);
        Parcelles.Add(parcelle1);
        parcelle1.Afficher();

    }

    // A mettre autre part mais je ne sais pas encore où
    private void AfficherParcelleAvecSelection()
    {
        var affichage = new AffichageParcelle(parcelleEnCours);
        int rang = affichage.AfficherAvecCurseur();

        Console.WriteLine($"\nRang sélectionné : {rang + 1}");
    }


    public void Suivant()
    {
        Semaine++;
        // tu pourras aussi appeler les méthodes de mise à jour des plantes ici
    }
}

