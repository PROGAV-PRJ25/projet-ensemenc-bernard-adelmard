public class CepageCabernetSauvignon : Plante
{
    public CepageCabernetSauvignon(string saisonActuelle)
    : base(saisonActuelle)
    {
        Nom = "Cepage Cabernet Sauvignon";
        SolPreferee = "Graveleux";
        VitesseCroissance = 13;
        ProbaMaladie = 0.08;
        BesoinsEau = 60;
        BesoinsLumiere = 60;
        TemperaturePreferee = (14, 32);
        EsperanceDeVie = 120;
        ProductionPotentielle = 25;
        ConsommationEauHebdo = 35;
        SaisonsPlantation = new List<string>
        {
            "Printemps",
            "Automne"
        };
        SaisonMomentPlantation = saisonActuelle;
    }
}
