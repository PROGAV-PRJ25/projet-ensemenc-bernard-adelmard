public class AffichageParcelle
{
    private Parcelle parcelle;

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
                    Console.Write("🟫"); // à personnaliser plus tard
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
}