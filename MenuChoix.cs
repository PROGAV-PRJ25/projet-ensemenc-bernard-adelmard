public class MenuChoix
{
    private List<string> options;
    private string? titre;

    public MenuChoix(List<string> options, string? titre = null)
    {
        this.options = options;
        this.titre = titre;
    }

    public int Afficher()
    {
        int selection = 0;
        bool choixFait = false;

        while (!choixFait)
        {
            Console.Clear();
            AfficherTitre();

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selection)
                    Console.WriteLine($"> {options[i]} <");
                else
                    Console.WriteLine($"  {options[i]}");
            }

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
                selection = (selection - 1 + options.Count) % options.Count;
            else if (key.Key == ConsoleKey.DownArrow)
                selection = (selection + 1) % options.Count;
            else if (key.Key == ConsoleKey.Enter)
                choixFait = true;
        }

        return selection;
    }

    private void AfficherTitre()
    {
        if (!string.IsNullOrWhiteSpace(titre))
        {
            Console.WriteLine(titre);
        }

        Console.WriteLine("Utilisez les flèches ↑ ↓ pour naviguer, Entrée pour valider.\n");
    }
}
