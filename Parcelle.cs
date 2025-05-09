public abstract class Parcelle
{
    public string Nom { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    public Cepage?[,] MatriceEtat { get; set; }
    public bool EstBio { get; set; } = false;
    public string? TypeSol { get; set; }
    public int Fertilite { get; set; } // 0 à 100
    public int Humidite { get; set; } // 0 à 100
    public int Ensoleillement { get; set; } // 0 à 100 (Ensoleillement actuelle, qui changera via la météo)
    public int Temperature { get; set; } // température actuelle
    //public List<MauvaiseHerbe> MauvaisesHerbes { get; set; } = new(); // en cas de futur ajout

    protected Parcelle(string nom, int largeur, int hauteur, string typeSol)
    {
        Nom = nom;
        Largeur = largeur;
        Hauteur = hauteur;
        MatriceEtat = new Cepage?[hauteur, largeur]; // chaque case = un plant ou vide
    }
}