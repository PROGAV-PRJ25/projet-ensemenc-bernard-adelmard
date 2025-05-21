public class ParcelleArgileuse : Parcelle
{
    public ParcelleArgileuse(string nom, int largeur, int hauteur)
        : base(nom, largeur, hauteur)
    {
        TypeSol = "Argileux";
        BlocTerre = "🟫";
    }
}
