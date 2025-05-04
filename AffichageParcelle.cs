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

    public int? AfficherDetailRangee(int y)
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
                return colonneSelectionnee;
            }

            else if (key.Key == ConsoleKey.Backspace)
                choixFait = true; //Quitte AfficherDetailRangee et revient dans la boucle dans Jeu.cs
        }
        return null;
    }

    public int AfficherMenuAction(int x) // Méthode qui renvoi l'index de l'action, peut être changer pour renvoyer le string directement car la liste pourrait changer de taille
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
        return menuAction.Afficher();

    }
}