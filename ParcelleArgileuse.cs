public class ParcelleArgileuse : Parcelle
{
    public ParcelleArgileuse(string nom, int largeur, int hauteur)
        : base(nom, largeur, hauteur, "argileux")
    {
        TypeSol = "Argileux";

        // Paramètres typiques d'un sol argileux
        Fertilite = 80;
        Humidite = 75;          
        Ensoleillement = 50;    
        Temperature = 18;       
    }
}
