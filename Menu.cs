public abstract class Menu
{
    protected string[] options = Array.Empty<string>();

    protected virtual void AfficherOptions(int selection) // Affiche toutes les options avec l'option séléctionnée en évidence
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selection)
                Console.WriteLine($"> {options[i]} <");
            else
                Console.WriteLine($"  {options[i]}");
        }
    }

    public virtual int Afficher() // Méthode qui permet de naviguer dans le menu et retourne le choix du joueur
    {
        int selection = 0;
        bool choixFait = false;

        while (!choixFait)
        {
            Console.Clear();
            AfficherTitre(); // Affichage d'un bandeau

            AfficherOptions(selection); // Affichage des options

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
                selection = (selection - 1 + options.Length) % options.Length;
            else if (key.Key == ConsoleKey.DownArrow)
                selection = (selection + 1) % options.Length;
            else if (key.Key == ConsoleKey.Enter)
                choixFait = true;
        }

        return selection;
    }

    protected virtual void AfficherTitre()
    {
        // Peut être personnalisé dans les sous-classes
    }
}
