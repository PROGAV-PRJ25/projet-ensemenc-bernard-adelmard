public static class CataloguePlantes
{
    public static List<Plante> GetToutes(string saisonActuelle)
    {
        return new List<Plante>
        {
            new CepageMerlot(saisonActuelle),
            new CepageCabernetSauvignon(saisonActuelle),
            new CepageChardonnay(saisonActuelle),
            new CepagePinotNoir(saisonActuelle),
            new CepageSyrah(saisonActuelle)
        };
    }
}
