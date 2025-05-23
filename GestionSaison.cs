public class GestionSaisons
{
    // 0=Printemps,1=Été,2=Automne,3=Hiver
    private int startIndex;
    private int dureeParSaison;

    public GestionSaisons(int DureeParSaison = 13)
    {
        dureeParSaison = Math.Max(1, DureeParSaison);
        startIndex = new Random().Next(0, 4);
    }

    public string SaisonDeDepart
    {
        get
        {
            switch (startIndex)
            {
                case 0:
                    return "Printemps";
                case 1:
                    return "Été";
                case 2:
                    return "Automne";
                case 3:
                    return "Hiver";
                default: throw new InvalidOperationException("Index de saison invalide");
            }
        }
    }

    public string GetSaison(int semaine)
    {
        int blocs = (semaine - 1) / dureeParSaison;
        int idxCyclique = (startIndex + blocs) % 4;

        switch (idxCyclique)
        {
            case 0:
                return "Printemps";
            case 1:
                return "Été";
            case 2:
                return "Automne";
            case 3:
                return "Hiver";
            default: throw new InvalidOperationException("Index de saison invalide");
        }
    }
}
