public class Joueur
{
    public string Nom { get; set; }
    public int NombreDeRaisins { get; set; } = 0;

    public Joueur(string nom)
    {
        Nom = nom;
    }

    public void Depenser(int montant)
    {
        NombreDeRaisins -= montant;
    }


    public void AjouterRaisins(int nbRaisins)
    {
        NombreDeRaisins += nbRaisins;
    }
}
