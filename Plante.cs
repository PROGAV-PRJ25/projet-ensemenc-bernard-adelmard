using System.Runtime.InteropServices;

public abstract class Plante
{
    // Caractéristique du cépage
    public string? Nom { get; set; }
    public Parcelle Parcelle { get; set; } = null!;
    public int EsperanceDeVie { get; set; }
    public int Hydratation { get; private set; } = 100;
    public int ConsommationEauHebdo { get; set; }
    public string SaisonMomentPlantation { get; set; }
    public int VitesseCroissance { get; set; }
    public double ProbaMaladie { get; set; } = 0;
    public int ProductionPotentielle { get; set; }
    public enum EtatPlante { Saine, Malade, Morte, Desechee }
    public EtatPlante Etat { get; set; } = EtatPlante.Saine;
    public bool EstSain
    {
        get
        {
            return Etat == EtatPlante.Saine;
        }
    }

    public int Croissance { get; set; } = 0;

    //Besoins du cépage
    // On a 7 besoins donc il faut en avoir 4 validé sinon la plante meurt
    public List<string> SaisonsPlantation { get; set; }
    public string? SolPreferee { get; set; }
    public int EspacementOptimal { get; set; }
    public int BesoinsEau { get; set; }
    public int BesoinsLumiere { get; set; }
    public (int Min, int Max) TemperaturePreferee { get; set; }
    // Pas malade

    public Plante(string saisonActuelle)
    {
        SaisonsPlantation = new List<string>();
        SaisonMomentPlantation = saisonActuelle;
    }

    private static readonly Random rnd = new Random();

    // Méthodes
    public void AvancerSemaine(int lumiere, int temp)
    {
        if (Etat == EtatPlante.Morte) return;

        // Hydratation diminue chaque semaine
        Hydratation = Math.Max(0, Hydratation - ConsommationEauHebdo); // Max pour éviter les valeurs négatives
        if (Hydratation < BesoinsEau)
        {
            Etat = EtatPlante.Desechee;
        }

        // Maladie
        if (Etat != EtatPlante.Malade && rnd.NextDouble() < ProbaMaladie)
        {
            Etat = EtatPlante.Malade;
        }

        // Croissance
        int bonus = CalculerBonusCroissance(lumiere, temp);
        Croissance = Math.Min(100, Croissance + VitesseCroissance + bonus); // Min pour pas dépasser 100
        EsperanceDeVie--;
        if (EsperanceDeVie <= 0) Etat = EtatPlante.Morte;
    }

    protected virtual int CalculerBonusCroissance(int lumiere, int temp)
    {
        int bonus = 0;

        if (Hydratation >= 90) bonus += 2;
        else bonus -= 1;

        if (lumiere >= BesoinsLumiere) bonus += 2;
        else bonus -= 1;

        if (temp >= TemperaturePreferee.Min && temp <= TemperaturePreferee.Max)
            bonus += 2;
        else
            bonus -= 1;

        if (SolPreferee == Parcelle.TypeSol)
            bonus += 2;
        else
            bonus -= 2;

        foreach (var saison in SaisonsPlantation)
        {
            if (SaisonMomentPlantation == saison)
            {
                bonus += 2;
            }
            else
                bonus -= 2;
        }

        if (Etat == EtatPlante.Malade || Etat == EtatPlante.Desechee)
            bonus /= 2;

        return bonus;
    }
    private bool VerifierBesoins(int lumiere, int temp)
    {
        bool eauOK = Hydratation >= BesoinsEau;
        bool lumOK = lumiere >= BesoinsLumiere;
        bool tempOK = temp >= TemperaturePreferee.Min &&
                          temp <= TemperaturePreferee.Max;

        return eauOK && lumOK && tempOK; // Return true si en vie, sinon false
    }
}
