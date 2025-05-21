public class ParcelleCalcaire : Parcelle
{
    public ParcelleCalcaire(string nom, int largeur, int hauteur)
        : base(nom, largeur, hauteur)
    {
        TypeSol = "Calcaire";
        BlocTerre = "🟨";
    }
}
