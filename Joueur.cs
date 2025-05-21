public class Joueur
{
    public string Nom { get; set; }
    public int Argent { get; set; } = 100; // monnaie de départ
    public int NombreDeRaisins { get; set; }
    public int ActionsDisponibles { get; set; } = 5;

    public Joueur(string nom)
    {
        Nom = nom;
    }

    public void Depenser(int montant)
    {
        Argent -= montant;
    }

    public void ReinitialiserActions()
    {
        ActionsDisponibles = 5;
    }

    public void UtiliserAction()
    {
        ActionsDisponibles--;
    }

    public void AjouterRaisins(int nbRaisins)
    {
        NombreDeRaisins += nbRaisins;
    }
}
