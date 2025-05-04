public class AffichageParcelle
{
    private Parcelle parcelle;
    public MenuChoix? menuAction;
    public AffichageParcelle(Parcelle parcelle)
    {
        this.parcelle = parcelle;
    }

    public int AfficherAvecCurseur()
    {
        int lignes = parcelle.Hauteur;
        int colonnes = parcelle.Largeur;
        int ligneSelectionnee = 0;
        bool choixFait = false;

        while (!choixFait)
        {
            Console.Clear();
            Console.WriteLine($"📍 Parcelle : {parcelle.Nom}");
            Console.WriteLine("━━━━━━━━━━━");

            for (int y = 0; y < lignes; y++)
            {
                for (int x = 0; x < colonnes; x++)
                {
                    var cepage = parcelle.MatriceEtat[y, x];
                    Console.Write(cepage == null ? "🟫" : "🌱");
                }

                if (y == ligneSelectionnee)
                    Console.Write(" ←");

                Console.WriteLine();
            }

            Console.WriteLine("━━━━━━━━━━━");
            Console.WriteLine("↑ ↓ pour choisir un rang | Entrée pour valider");

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
                ligneSelectionnee = (ligneSelectionnee - 1 + lignes) % lignes;
            else if (key.Key == ConsoleKey.DownArrow)
                ligneSelectionnee = (ligneSelectionnee + 1) % lignes;
            else if (key.Key == ConsoleKey.Enter)
                choixFait = true;

        }

        return ligneSelectionnee;
    }

    public void AfficherDetailRangee(int y)
    {
        Console.Clear();
        int[] tabPalier = new int[] { 100, 75, 50, 25, 0 };
        int colonneSelectionnee = 0;
        bool choixFait = false;

        while (!choixFait)
        {
            Console.Clear();

            foreach (int palier in tabPalier)
            {
                for (int i = 0; i < parcelle.Largeur; i++)
                {
                    var cepage = parcelle.MatriceEtat[y, i];
                    if (cepage != null && cepage.Croissance >= palier)
                    {
                        if (palier == 100)
                            Console.Write("🍇");
                        else
                            Console.Write("🌿");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }

            for (int i = 0; i < parcelle.Largeur; i++) Console.Write("🟫");
            Console.WriteLine();

            for (int i = 0; i < parcelle.Largeur; i++)
            {
                var cepage = parcelle.MatriceEtat[y, i];
                if (cepage != null)
                {
                    if (cepage.Etat == Cepage.EtatCepage.Saine)
                        Console.Write("✅");
                    else if (cepage.Etat == Cepage.EtatCepage.Malade)
                        Console.Write("🦠");
                    else if (cepage.Etat == Cepage.EtatCepage.Desechee)
                        Console.Write("💧");
                    else if (cepage.Etat == Cepage.EtatCepage.Morte)
                        Console.Write("❌");
                }
            }
            Console.WriteLine();

            for (int i = 0; i < parcelle.Largeur; i++)
            {
                Console.Write(i == colonneSelectionnee ? "↑ " : "  ");
            }
            Console.WriteLine();
            Console.WriteLine("← → pour changer de colonne | Backspace pour quitter");


            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow)
                colonneSelectionnee = (colonneSelectionnee - 1 + parcelle.Largeur) % parcelle.Largeur;
            else if (key.Key == ConsoleKey.RightArrow)
                colonneSelectionnee = (colonneSelectionnee + 1) % parcelle.Largeur;
            else if (key.Key == ConsoleKey.Enter)
            {
                AfficherMenuAction(colonneSelectionnee);
            }

            else if (key.Key == ConsoleKey.Backspace)
                choixFait = true; //Quitte AfficherDetailRangee et revient dans la boucle dans Jeu.cs
        }

    }

    void AfficherMenuAction(int x)
    {
        List<string> options = new List<string>
    {
        "Arroser",
        "Traiter",
        "Désherber",
        "Tuer la plante",
        "Récolter"
    };

        menuAction = new MenuChoix(options, "Choisissez une action sur la plante :");
        int action = menuAction.Afficher();

        // 
        switch (action)
        {
            case 0:
                Console.WriteLine("💧 Vous avez arrosé la plante.");
                break;
            case 1:
                Console.WriteLine("🧪 Traitement appliqué.");
                break;
            case 2:
                Console.WriteLine("🧹 Cepage désherbée.");
                break;
            case 3:
                Console.WriteLine("☠ La plante a été supprimée.");
                break;
            case 4:
                Console.WriteLine("🍇 Récolte effectuée !");
                break;
        }

        Console.WriteLine("\nAppuyez sur une touche pour revenir.");
        Console.ReadKey();
    }

}
