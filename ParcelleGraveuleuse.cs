public class ParcelleGraveleuse : Parcelle
{
    public ParcelleGraveleuse(string nom, int largeur, int hauteur)
        : base(nom, largeur, hauteur)
    {
        TypeSol = "Graveleux";
        BlocTerre = "🟧";
    }
}
