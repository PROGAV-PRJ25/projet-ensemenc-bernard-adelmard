public abstract class Parcelle
{
    public string Nom { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    public bool EstBio { get; set; } = false;

    protected Parcelle(string nom, int largeur, int hauteur)
    {
        Nom = nom;
        Largeur = largeur;
        Hauteur = hauteur;
    }

    public virtual void Afficher()
    {
        Console.WriteLine("🌱​");
    }
}