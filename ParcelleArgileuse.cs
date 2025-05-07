public class ParcelleArgileuse : Parcelle
{
    public ParcelleArgileuse(string nom, int largeur, int hauteur)
        : base(nom, largeur, hauteur, "argileux")
    {
        // Paramètres typiques d'un sol argileux
        Fertilite = 80;         // Très fertile
        Humidite = 75;          // Forte rétention d’eau
        Ensoleillement = 50;    // Drainage lent = ensoleillement souvent réduit
        Temperature = 18;       // Température modérée (sol plus lent à chauffer)
    }
}
