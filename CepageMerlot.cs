public class CepageMerlot : Cepage
{
    public CepageMerlot()
    {
        Nom = "Merlot";
        SolPreferee = "argileux";
        VitesseCroissance = 10;
        BesoinsEau = 70;
        BesoinsLumiere = 50;
        TemperaturePreferee = (15, 30);
        EsperanceDeVie = 100;
        ProductionPotentielle = 30;
    }

    public override void Pousser()
    {
    }

    public override void VerifierEtat(int eauReçue, int lumiereReçue, int temperatureActuelle)
    {

    }
}
