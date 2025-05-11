public static class CataloguePlantes
{
    public static List<Plante> GetToutes()
    {
        return new List<Plante>
        {
            new CepageMerlot(),
            new CepageCabernetSauvignon()
        };
    }
}
