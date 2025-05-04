public class AffichageParcelle
{
    private Parcelle parcelle;

    // Constructeur
    public AffichageParcelle(Parcelle parcelle)
    {
        this.parcelle = parcelle;
    }

    // Méthodes
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

        // Affichage du sol
        for (int i = 0; i < parcelle.Largeur; i++)
        {
            Console.Write("🟫");
        }
        Console.WriteLine();

        // Affichage de l'état du cépage
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
            else
            {
                Console.Write("  ");
            }
        }
        Console.WriteLine();
    }
}
