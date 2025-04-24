public class Joueur
{
    public string Nom { get; set; }
    public int Argent { get; set; } = 100; // monnaie de départ
    public int ActionsDisponibles { get; set; } = 2;

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
        ActionsDisponibles = 2;
    }

    public void UtiliserAction()
    {
        ActionsDisponibles--;
    }
}
