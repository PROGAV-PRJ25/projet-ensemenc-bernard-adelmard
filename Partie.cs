public class Partie
{
    public Joueur Joueur { get; set; }
    public List<Parcelle> Parcelles { get; set; } = new();
    public int Semaine { get; set; } = 1;

    public Partie(Joueur joueur, List<Parcelle> parcelles)
    {
        Joueur = joueur;
        Parcelles = parcelles;
    }

    public void Suivant()
    {
        Semaine++;
        // tu pourras aussi appeler les méthodes de mise à jour des plantes ici
    }
}

