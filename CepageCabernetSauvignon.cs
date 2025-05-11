public class CepageCabernetSauvignon : Plante
{
    public CepageCabernetSauvignon()
    {
        Nom = "Cepage Cabernet Sauvignon";
        SolPreferee = "graveleux";
        VitesseCroissance = 8;
        BesoinsEau = 60;
        BesoinsLumiere = 60;
        TemperaturePreferee = (14, 32);
        EsperanceDeVie = 120;
        ProductionPotentielle = 25;
    }
    public override void Pousser()
    {
    }

    public override void VerifierEtat(int eauReçue, int lumiereReçue, int temperatureActuelle)
    {

    }
}
