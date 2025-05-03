public abstract class Cepage
{
    // Nom de la plante
    public string? Nom { get; set; }

    // Type : "annuelle", "vivace", etc.
    public string? Type { get; set; }

    // Saisons de semis possibles (ex : ["Printemps", "Automne"])
    public List<string>? SaisonsSemis { get; set; }

    // Type de sol préféré (ex : "graveleux", "argileux")
    public string? TerrainPrefere { get; set; }

    // Espacement recommandé entre les plantes (en mètres)
    public float Espacement { get; set; }

    // Espace nécessaire pour bien se développer (en m²)
    public float EspaceNecessaire { get; set; }

    // Vitesse de croissance (exprimée en points/semaine)
    public int VitesseCroissance { get; set; }

    // Besoins en eau (0 à 100)
    public int BesoinsEau { get; set; }

    // Besoins en lumière (0 à 100)
    public int BesoinsLumiere { get; set; }

    // Température idéale (intervalle min-max)
    public (int Min, int Max) TemperaturePreferee { get; set; }

    // Liste des maladies possibles
    //public List<Maladie> MaladiesPossibles { get; set; }

    // Espérance de vie en semaines
    public int EsperanceDeVie { get; set; }

    // Production possible (grappes, fruits, etc.), sur toute sa vie
    public int ProductionPotentielle { get; set; }

    // État de la plante
    public enum EtatCepage
    {
        Saine,
        Malade,
        Morte,
        Desechee
    }

    public EtatCepage Etat { get; set; } = EtatCepage.Saine;


    // Pourcentage de croissance actuelle
    public int Croissance { get; set; } = 0;

    // Méthodes
    public abstract void Pousser();

    public abstract void VerifierEtat(int eauReçue, int lumiereReçue, int temperatureActuelle);
}
