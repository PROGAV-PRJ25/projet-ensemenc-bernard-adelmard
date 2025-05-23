public class Joueur
{
    public string Nom { get; set; }
    public int NombreDeRaisins { get; set; } = 0;

    public Joueur(string nom)
    {
        Nom = nom;
    }
}
