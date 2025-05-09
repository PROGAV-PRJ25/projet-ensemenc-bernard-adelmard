public abstract class Cepage
{
    // Caractéristique du cépage
    public string? Nom { get; set; }
    public int EsperanceDeVie { get; set; }
    public int VitesseCroissance { get; set; }
    public List<Maladie> MaladiesPossibles { get; set; }
    public int ProductionPotentielle { get; set; }
    public enum EtatCepage
    {
        Saine,
        Malade,
        Morte,
        Desechee
    }
    public EtatCepage Etat { get; set; } = EtatCepage.Saine;
    public bool EstSain
    {
        get
        {
            return Etat == EtatCepage.Saine;
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

    public Cepage() // Constructeur pour éviter de rendre nullable les propriétés
    {
        SaisonsPlantation = new List<string>();
        MaladiesPossibles = new List<Maladie>();
    }


    // Méthodes
    public abstract void Pousser();

    public abstract void VerifierEtat(int eauReçue, int lumiereReçue, int temperatureActuelle);
}
