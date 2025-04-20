public class Menu
{
    private string[] optionsPrincipales = { "Nouvelle Partie", "Charger une Partie", "Règles du jeu", "Quitter" };

    public int AfficherMenuPrincipal()
    {
        int selection = 0; // Selection actuelle dans le menu, pour mettre en évidence
        bool choixFait = false;

        while (!choixFait)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J"); // Suprime l'historique de la console
            Console.WriteLine("====================================");
            Console.WriteLine("      🍇 Le Jeu Viticole 🍇        ");
            Console.WriteLine("====================================\n");
            Console.WriteLine("Utilisez les flèches ↑ ↓ pour naviguer, Entrée pour valider.\n");

            for (int i = 0; i < optionsPrincipales.Length; i++) // Affihage des différentes options du menu
            {
                if (i == selection)
                    Console.WriteLine($"> {optionsPrincipales[i]} <");
                else
                    Console.WriteLine($"  {optionsPrincipales[i]}");
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
                selection = (selection - 1 + optionsPrincipales.Length) % optionsPrincipales.Length;
            else if (key.Key == ConsoleKey.DownArrow)
                selection = (selection + 1) % optionsPrincipales.Length;
            else if (key.Key == ConsoleKey.Enter)
                choixFait = true;
        }

        return selection; 
    }
}

