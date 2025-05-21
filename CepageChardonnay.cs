public class CepageChardonnay : Plante
{
    public CepageChardonnay(string saisonActuelle)
        : base(saisonActuelle)
    {
        Nom = "Cépage Chardonnay";
        SolPreferee = "Calcaire";
        VitesseCroissance = 11;
        ProbaMaladie = 0.10;
        BesoinsEau = 65;
        BesoinsLumiere = 65;
        TemperaturePreferee = (10, 25);
        EsperanceDeVie = 110;
        ProductionPotentielle = 27;
        ConsommationEauHebdo = 30;

        SaisonsPlantation = new List<string>
        {
            "Printemps",
            "Automne"
        };

        SaisonMomentPlantation = saisonActuelle;
    }
}