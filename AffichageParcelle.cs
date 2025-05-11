using System.Net.Sockets;
using System.Threading;

public class AffichageParcelle
{
    private Parcelle parcelle { get; set; }
    public MenuChoix? menuAction { get; set; }
    public AffichageParcelle(Parcelle parcelle)
    {
        this.parcelle = parcelle;
    }
    public int AfficherAvecCurseur(int ligneInitiale = 0)
    {
        int lignes = parcelle.Hauteur;
        int colonnes = parcelle.Largeur;
        int ligneSelectionnee = ligneInitiale;
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
                            case Plante.EtatPlante.Saine:
                                Console.Write("✅  ");
                                break;
                            case Plante.EtatPlante.Malade:
                                Console.Write("🦠  ");
                                break;
                            case Plante.EtatPlante.Desechee:
                                Console.Write("💧  ");
                                break;
                            case Plante.EtatPlante.Morte:
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

            // Affiche les infos à droite
            int xOffset = parcelle.Largeur * 4 + 7; // 4 caractères par cellule + marge
            AfficherInfosParcelle(xOffset, 2);

            int positionY = lignes * 2 + 4;

            if (lignes < 4)
                positionY = lignes * 2 + 7;
            
            Console.SetCursorPosition(0, positionY);
            Console.WriteLine("↑ ↓ pour choisir un rang | Entrée pour valider");

            Console.SetCursorPosition(0, positionY + 1);
            Console.WriteLine("Espace pour ouvrir le menu des actions");



            int attente = 0;
            bool toucheDetectee = false;

            while (attente < 10 && !toucheDetectee) // vérifie la touche toutes les 100ms pendant 1 seconde
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        ligneSelectionnee = (ligneSelectionnee - 1 + lignes) % lignes;
                        toucheDetectee = true;
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        ligneSelectionnee = (ligneSelectionnee + 1) % lignes;
                        toucheDetectee = true;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        choixFait = true;
                        toucheDetectee = true;
                    }
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        AfficherMenuActionGeneral();
                        toucheDetectee = true;
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
                    if (cepage.Etat == Plante.EtatPlante.Saine)
                        Console.Write("✅");
                    else if (cepage.Etat == Plante.EtatPlante.Malade)
                        Console.Write("🦠");
                    else if (cepage.Etat == Plante.EtatPlante.Desechee)
                        Console.Write("💧");
                    else if (cepage.Etat == Plante.EtatPlante.Morte)
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
                choixFait = true;
        }
        return null;
    }

    public string AfficherMenuActionDetail(int x) // Méthode qui renvoi le string de l'action
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

    public string AfficherMenuActionGeneral()
    {
        List<string> options = new()
        {
            "Planter",
            "Renomer la parcelle",
            "Voir toutes les parcelles",
            "Passer à la semaine suivante"
        };

        var menuActionGeneral = new MenuChoix(options, "Action générale :");
        return options[menuActionGeneral.Afficher()];
    }

    private void AfficherInfosParcelle(int x, int y)
    {

        Console.SetCursorPosition(x, y + 1);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("===== Statut de la parcelle =====");
        Console.ResetColor();

        Console.SetCursorPosition(x, y + 3);
        Console.WriteLine($"Sol : {parcelle.TypeSol}");

        Console.SetCursorPosition(x, y + 4);
        Console.WriteLine($"Fertilité : {parcelle.Fertilite}/100");

        Console.SetCursorPosition(x, y + 5);
        Console.WriteLine($"Humidité : {parcelle.Humidite}/100");

        Console.SetCursorPosition(x, y + 6);
        Console.WriteLine($"Ensoleillement : {parcelle.Ensoleillement}/100");

        Console.SetCursorPosition(x, y + 7);
        Console.WriteLine($"Température : {parcelle.Temperature}°C");

        Console.SetCursorPosition(x, y + 8);
        Console.WriteLine($"Parcelle Bio : {(parcelle.EstBio ? "Oui" : "Non")}");
    }

}