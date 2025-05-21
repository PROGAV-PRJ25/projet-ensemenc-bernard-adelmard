public class CepageSyrah : Plante
{
    public CepageSyrah(string saisonActuelle)
        : base(saisonActuelle)
    {
        Nom = "Cépage Syrah";
        SolPreferee = "Graveleux";
        VitesseCroissance = 14;
        ProbaMaladie = 0.07;
        BesoinsEau = 55;
        BesoinsLumiere = 80;
        TemperaturePreferee = (18, 35);
        EsperanceDeVie = 100;
        ProductionPotentielle = 32;
        ConsommationEauHebdo = 35;

        SaisonsPlantation = new List<string>
        {
            "Été",
            "Automne"
        };

        SaisonMomentPlantation = saisonActuelle;
    }
}