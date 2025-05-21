public class CepagePinotNoir : Plante
{
    public CepagePinotNoir(string saisonActuelle)
        : base(saisonActuelle)
    {
        Nom = "Cépage Pinot Noir";
        SolPreferee = "Calcaire";
        VitesseCroissance = 9;
        ProbaMaladie = 0.15;
        BesoinsEau = 75;
        BesoinsLumiere = 50;
        TemperaturePreferee = (12, 22);
        EsperanceDeVie = 90;
        ProductionPotentielle = 22;
        ConsommationEauHebdo = 45;

        SaisonsPlantation = new List<string>
        {
            "Printemps",
            "Été"
        };

        SaisonMomentPlantation = saisonActuelle;
    }
}