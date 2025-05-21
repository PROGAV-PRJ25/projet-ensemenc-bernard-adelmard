public class CepageMerlot : Plante
{
    public CepageMerlot(string saisonActuelle)
        : base(saisonActuelle)
    {
        Nom = "Cépage Merlot";
        SolPreferee = "Argileux";
        VitesseCroissance = 15;
        ProbaMaladie = 0.12;
        BesoinsEau = 70;
        BesoinsLumiere = 50;
        TemperaturePreferee = (15, 30);
        EsperanceDeVie = 100;
        ProductionPotentielle = 30;
        ConsommationEauHebdo = 40;
        SaisonsPlantation = new List<string>
        {
            "Printemps",
            "Été"
        };

        SaisonMomentPlantation = saisonActuelle;
    }
}
