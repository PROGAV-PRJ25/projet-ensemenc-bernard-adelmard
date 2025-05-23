using Newtonsoft.Json;

public abstract class Parcelle
{
    public string Nom { get; set; }
    public int Largeur { get; set; }
    public int Hauteur { get; set; }
    [JsonIgnore]
    public Plante?[,] MatriceEtat { get; set; }
    
    [JsonProperty("MatriceEtat")]
    private Plante?[][] MatriceEtat_Proxy
    {
        // conversion multidim → jagged
        get
        {
            var rows = new Plante?[Hauteur][];
            for (int y = 0; y < Hauteur; y++)
            {
                rows[y] = new Plante?[Largeur];
                for (int x = 0; x < Largeur; x++)
                    rows[y][x] = MatriceEtat[y, x];
            }
            return rows;
        }
        // conversion jagged → multidim
        set
        {
            MatriceEtat = new Plante?[Hauteur, Largeur];
            for (int y = 0; y < Hauteur && y < value.Length; y++)
                for (int x = 0; x < Largeur && x < value[y].Length; x++)
                    MatriceEtat[y, x] = value[y][x];
        }
    }
    // …

    public string? TypeSol { get; set; }
    public int Ensoleillement { get; set; } // 0 à 100 (Ensoleillement actuelle, qui changera via la météo)
    public int Temperature { get; set; } // température actuelle
    public bool Pluie { get; set; } //Pluie ou pas
    public string BlocTerre { get; set; } = "  ";
    public int NombreActionDispo { get; set; } = 3;
    public int BonusAction { get; set; } = 0;
    private const int BaseActions = 3;

    protected Parcelle(string nom, int largeur, int hauteur)
    {
        Nom = nom;
        Largeur = largeur;
        Hauteur = hauteur;
        MatriceEtat = new Plante?[hauteur, largeur]; // chaque case = un plant ou vide
    }

    public void ReinitialiserActions()
    {
        {
            int comptePlantes = 0;
            for (int y = 0; y < Hauteur; y++)
            {
                for (int x = 0; x < Largeur; x++)
                {
                    if (MatriceEtat[y, x] != null)
                        comptePlantes++;
                }
            }

            BonusAction = comptePlantes;
            NombreActionDispo = BaseActions + BonusAction;
        }
    }

    public void UtiliserAction()
    {
        NombreActionDispo--;
    }
}