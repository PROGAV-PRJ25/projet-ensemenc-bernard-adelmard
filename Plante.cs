public abstract class Plante
{
    // Caractéristique du cépage
    public string? Nom { get; set; }
    public Parcelle Parcelle { get; set; } = null!;
    private int semainesDesechees = 0;
    public int EsperanceDeVie { get; set; }
    public int Hydratation { get; set; } = 100;
    public int ConsommationEauHebdo { get; set; }
    public string SaisonMomentPlantation { get; set; }
    public bool EstDansSaisonPreferee
    {
        get
        {
            if (SaisonsPlantation.Contains(SaisonMomentPlantation))
                return true;
            else
                return false;
        }
    }
    public int VitesseCroissance { get; set; }
    public double ProbaMaladie { get; set; } = 0;
    public int ProductionPotentielle { get; set; }
    public enum EtatPlante { Saine, Malade, Morte, Desechee, MaladeDesechee }
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
    // On a ces besoins donc il faut en avoir  validé sinon la plante meurt
    public List<string> SaisonsPlantation { get; set; } // Inchangeable
    public string? SolPreferee { get; set; } // Inchangeable
    public int BesoinsEau { get; set; } // Changeable
    public int BesoinsLumiere { get; set; } // Inchangeable mais peut varier en fonction de la météo
    public (int Min, int Max) TemperaturePreferee { get; set; } // Inchangeable mais peut varier en fonction de la météo


    public Plante(string saisonActuelle)
    {
        SaisonsPlantation = new List<string>();
        SaisonMomentPlantation = saisonActuelle;
    }

    private static readonly Random rnd = new Random();

    // Méthodes
    public void AvancerSemaine(int ensoleillement, int temp)
    {
        if (Etat == EtatPlante.Morte)
            return;

        // Hydratation diminue chaque semaine
        Hydratation = Math.Max(0, Hydratation - ConsommationEauHebdo); // Max pour éviter les valeurs négatives
        MettreAJourEtatHydratation();

        if (!VerifierSurvie(ensoleillement, temp))
        {
            Etat = EtatPlante.Morte;
            return;
        }

        // Maladie
        if (rnd.NextDouble() < ProbaMaladie)
            if (Etat == EtatPlante.Desechee)
                Etat = EtatPlante.MaladeDesechee;
            else
                Etat = EtatPlante.Malade;

        // Croissance
        int bonus = CalculerBonusCroissance(ensoleillement, temp);
        Croissance = Math.Min(100, Croissance + VitesseCroissance + bonus); // Min pour pas dépasser 100

        // Vieillissement
        EsperanceDeVie--;
        if (EsperanceDeVie <= 0)
            Etat = EtatPlante.Morte;
    }

    private bool VerifierSurvie(int ensoleillement, int temp)
    {
        int satisfaits = 0;

        // 1. Non malade
        if (Etat != EtatPlante.Malade) satisfaits++;

        // 2. Non desséché
        if (Etat != EtatPlante.Desechee) satisfaits++;

        // 3. Ensoleillement
        if (ensoleillement >= BesoinsLumiere) satisfaits++;

        // 4. Sol préféré
        if (Parcelle != null && SolPreferee == Parcelle.TypeSol) satisfaits++;

        // 5. Saison de plantation
        if (EstDansSaisonPreferee)
            satisfaits++;

        // 6. Température
        if (temp >= TemperaturePreferee.Min && temp <= TemperaturePreferee.Max)
            satisfaits++;

        //
        return satisfaits >= 2;
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

        bonus += EstDansSaisonPreferee ? +2 : -2;

        if (Etat == EtatPlante.Malade || Etat == EtatPlante.Desechee)
            bonus /= 2;

        return bonus;
    }
    public void Arroser(int quantite = 70)
    {
        Hydratation = Math.Min(100, Hydratation + quantite);
        MettreAJourEtatHydratation();
    }

    private void MettreAJourEtatHydratation()
    {
        if (Hydratation < BesoinsEau)
        {
            // Plante desséchée
            Etat = EtatPlante.Desechee;
            semainesDesechees++;

            // 3 semaines consécutives en desséché → mort
            if (semainesDesechees >= 3)
                Etat = EtatPlante.Morte;
        }
        else
        {
            // Hydratation OK → on réinitialise le compteur
            semainesDesechees = 0;
            if (Etat == EtatPlante.MaladeDesechee)
                Etat = EtatPlante.Malade;
            else if (Etat != EtatPlante.Malade)
                Etat = EtatPlante.Saine;
        }
    }
}
