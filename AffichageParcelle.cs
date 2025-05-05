using System.Threading;

public class AffichageParcelle
{
    private Parcelle parcelle { get; set; }
    public MenuChoix? menuAction { get; set; }
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
        bool afficherEtat = false;

        while (!choixFait)
        {
            Console.Clear();
            Console.WriteLine($"📍 Parcelle : {parcelle.Nom}");
            Console.WriteLine();

            for (int y = 0; y < lignes; y++)
            {
                for (int x = 0; x < colonnes; x++)
                {
                    var cepage = parcelle.MatriceEtat[y, x];
                    if (cepage == null)
                    {
                        Console.Write("🟫  ");
                    }
                    else if (afficherEtat) // Affiche l'état
                    {
                        switch (cepage.Etat)
                        {
                            case Cepage.EtatCepage.Saine:
                                Console.Write("✅  ");
                                break;
                            case Cepage.EtatCepage.Malade:
                                Console.Write("🦠  ");
                                break;
                            case Cepage.EtatCepage.Desechee:
                                Console.Write("💧  ");
                                break;
                            case Cepage.EtatCepage.Morte:
                                Console.Write("❌  ");
                                break;
                        }
                    }
                    else // Affiche le plant standard
                    {
                        Console.Write("🌱  ");
                    }
                }

                if (y == ligneSelectionnee)
                    Console.Write("←");

                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("↑ ↓ pour choisir un rang | Entrée pour valider");

            int attente = 0;
            while (attente < 10) // vérifie la touche toutes les 100ms pendant 1 seconde
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        ligneSelectionnee = (ligneSelectionnee - 1 + lignes) % lignes;
                        break;
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        ligneSelectionnee = (ligneSelectionnee + 1) % lignes;
                        break;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        choixFait = true;
                        break;
                    }
                }

                Thread.Sleep(100); // attend 100ms
                attente++;
            }

            afficherEtat = !afficherEtat; // alterne l'affichage à chaque seconde
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

    public string AfficherMenuAction(int x) // Méthode qui renvoi l'index de l'action, peut être changer pour renvoyer le string directement car la liste pourrait changer de taille
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
        return options[menuAction.Afficher()];

    }
}