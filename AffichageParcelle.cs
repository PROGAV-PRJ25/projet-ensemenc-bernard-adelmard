using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

public class AffichageParcelle
{
    private Parcelle parcelle { get; set; }
    public MenuChoix? menuAction { get; set; }
    public Joueur? joueur { get; set; }
    public Partie? partie { get; set; }
    public Jeu? jeu { get; set; }
    public AffichageParcelle(Parcelle parcelle, Joueur joueur, Partie partie, Jeu jeu)
    {
        this.parcelle = parcelle;
        this.joueur = joueur;
        this.partie = partie;
        this.jeu = jeu;
    }

    public void AfficherParcelleCurseur(int ligneSelectionnee, int lignes, int colonnes, bool afficherEtat, bool planter, int[] caseSelectionnee)
    {
        Console.Clear();
        Console.WriteLine($"📍 Parcelle : {parcelle.Nom}        📅 Semaine : {partie!.Semaine}       🍇 Nombre de raisins : {joueur!.NombreDeRaisins}");
        Console.WriteLine();

        for (int y = 0; y < lignes; y++)
        {
            for (int x = 0; x < colonnes; x++)
            {
                string charDeSelection = "  ";
                var cepage = parcelle.MatriceEtat[y, x];
                if (cepage == null)
                {
                    if (planter && x == caseSelectionnee[0] && y == caseSelectionnee[1])
                        charDeSelection = "← ";
                    Console.Write($"{parcelle.BlocTerre}{charDeSelection}");
                }
                else if (afficherEtat) // Affiche l'état
                {
                    switch (cepage.Etat)
                    {
                        case Plante.EtatPlante.Saine:
                            if (planter && x == caseSelectionnee[0] && y == caseSelectionnee[1])
                                charDeSelection = "← ";
                            Console.Write($"✅{charDeSelection}");
                            break;
                        case Plante.EtatPlante.Malade:
                            if (planter && x == caseSelectionnee[0] && y == caseSelectionnee[1])
                                charDeSelection = "← ";
                            Console.Write($"🦠{charDeSelection}");
                            break;
                        case Plante.EtatPlante.Desechee:
                            if (planter && x == caseSelectionnee[0] && y == caseSelectionnee[1])
                                charDeSelection = "← ";
                            Console.Write($"💧{charDeSelection}");
                            break;
                        case Plante.EtatPlante.MaladeDesechee:
                            if (planter && x == caseSelectionnee[0] && y == caseSelectionnee[1])
                                charDeSelection = "← ";
                            Console.Write($"⚠️{charDeSelection}");
                            break;
                    }
                }
                else // Affiche le plant standard
                {
                    if (planter && x == caseSelectionnee[0] && y == caseSelectionnee[1])
                        charDeSelection = "← ";
                    Console.Write($"🌱{charDeSelection}");
                }
            }

            if (!planter && y == ligneSelectionnee)
                Console.Write("←");

            Console.WriteLine();
            Console.WriteLine();
        }

    }

    public void AfficherPlantage()
    {
        List<Plante> plantesDisponibles = CataloguePlantes.GetToutes(partie!.SaisonActuelle);

        int lignes = parcelle.Hauteur;
        int colonnes = parcelle.Largeur;
        int[] caseSelectionnee = new int[] { 0, 0 };
        bool choixFait = false;
        bool afficherEtat = false;
        int? indexPlanteSelectionnee = null;

        while (!choixFait)
        {
            AfficherParcelleCurseur(
                caseSelectionnee[1],
                lignes,
                colonnes,
                afficherEtat,
                planter: true,
                caseSelectionnee
            );

            int xOffset = parcelle.Largeur * 4 + 7;
            AfficherPlantesDisponibles(xOffset, 2, plantesDisponibles, indexPlanteSelectionnee);

            int positionY = lignes * 2 + (lignes < 4 ? 7 : 4);
            Console.SetCursorPosition(0, positionY);
            Console.WriteLine("← ↑ ↓ → pour se déplacer | Entrée pour planter");
            Console.SetCursorPosition(0, positionY + 1);
            Console.WriteLine("1 à 5 pour choisir la plante | Retour : Backspace");

            int attente = 0;
            bool toucheDetectee = false;

            while (attente < 10 && !toucheDetectee)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            caseSelectionnee[1] = (caseSelectionnee[1] - 1 + lignes) % lignes;
                            break;
                        case ConsoleKey.DownArrow:
                            caseSelectionnee[1] = (caseSelectionnee[1] + 1) % lignes;
                            break;
                        case ConsoleKey.LeftArrow:
                            caseSelectionnee[0] = (caseSelectionnee[0] - 1 + colonnes) % colonnes;
                            break;
                        case ConsoleKey.RightArrow:
                            caseSelectionnee[0] = (caseSelectionnee[0] + 1) % colonnes;
                            break;
                        case ConsoleKey.Backspace:
                            return;
                        case ConsoleKey.Enter:
                            if (indexPlanteSelectionnee != null)
                            {
                                var plante = plantesDisponibles[indexPlanteSelectionnee.Value];
                                partie!.Planter(plante, caseSelectionnee[1], caseSelectionnee[0]);
                                Console.WriteLine("Appuyez sur une touche pour continuer...");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, positionY + 3);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("❌ Vous devez d'abord sélectionner une plante (1-9) avant d'appuyer sur Entrée.");
                                Console.ResetColor();
                                Console.ReadKey();
                            }
                            break;
                        default:
                            if (key.Key >= ConsoleKey.D1 && key.Key <= ConsoleKey.D9)
                            {
                                int idx = key.Key - ConsoleKey.D1;
                                if (idx < plantesDisponibles.Count)
                                    indexPlanteSelectionnee = idx;
                            }
                            break;
                    }
                    toucheDetectee = true;
                }
                Thread.Sleep(100);
                attente++;
            }

            afficherEtat = !afficherEtat;
        }
    }

    public int Afficher(int ligneInitiale = 0)
    {
        int lignes = parcelle.Hauteur;
        int colonnes = parcelle.Largeur;
        int ligneSelectionnee = ligneInitiale;
        bool choixFait = false;
        bool afficherEtat = false;
        bool planter = false;
        int[] caseSelectionnee = [0, 0];

        while (!choixFait)
        {
            AfficherParcelleCurseur(ligneSelectionnee, lignes, colonnes, afficherEtat, planter, caseSelectionnee);

            // Affiche les infos à droite
            int xOffset = parcelle.Largeur * 4 + 7; // 4 caractères par cellule + marge
            AfficherInfosParcelle(xOffset, 2);

            int positionY = lignes * 2 + 5;

            if (lignes < 4)
                positionY = lignes * 2 + 7;

            Console.SetCursorPosition(0, positionY);
            Console.WriteLine("↑ ↓ pour choisir un rang | Entrée pour valider");

            Console.SetCursorPosition(0, positionY + 1);
            Console.WriteLine("Espace pour ouvrir le menu des actions");
            Console.WriteLine("");

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
                        toucheDetectee = true;
                        return -1;
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

            for (int i = 0; i < parcelle.Largeur; i++) Console.Write($"{parcelle.BlocTerre}");
            Console.WriteLine();
            for (int i = 0; i < parcelle.Largeur; i++)
            {
                var cepage = parcelle.MatriceEtat[y, i];
                if (cepage != null)
                {
                    switch (cepage.Etat)
                    {
                        case Plante.EtatPlante.Saine: Console.Write("✅"); break;
                        case Plante.EtatPlante.Malade: Console.Write("🦠"); break;
                        case Plante.EtatPlante.MaladeDesechee: Console.Write("⚠️"); break;
                        case Plante.EtatPlante.Desechee: Console.Write("💧"); break;
                        case Plante.EtatPlante.Morte: Console.Write("❌"); break;
                    }
                }
                else
                {
                    Console.Write("  ");
                }
            }
            Console.WriteLine();

            for (int i = 0; i < parcelle.Largeur; i++)
            {
                Console.Write(i == colonneSelectionnee ? "↑ " : "  ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("← → pour changer de colonne | Backspace pour quitter");
            Console.WriteLine("Entrée pour choisir une action sur la plante sélectionnée.");

            int x = parcelle.Largeur + 10;
            var selected = parcelle.MatriceEtat[y, colonneSelectionnee];

            if (selected != null)
            {
                Console.SetCursorPosition(x, 0);
                Console.WriteLine($"Type de plante: {selected!.Nom}");

                Console.SetCursorPosition(x, 1);
                Console.WriteLine($"Croissance: {selected!.Croissance}%");

                Console.SetCursorPosition(x, 2);
                Console.WriteLine($"État : {selected!.Etat}");

                Console.SetCursorPosition(x, 3);
                Console.WriteLine($"Hydratation de {selected.Hydratation}%  {(selected.Hydratation > selected.BesoinsEau ? ": ✅" : " : ❌")} ");

                Console.SetCursorPosition(x, 4);
                if (selected.BesoinsLumiere < parcelle.Ensoleillement)
                    Console.WriteLine("Ensoleillement:      : ✅");
                else
                    Console.WriteLine("Ensoleillement:      : ❌");

                Console.SetCursorPosition(x, 5);
                if (selected.TemperaturePreferee.Min < parcelle.Temperature && selected.TemperaturePreferee.Max > parcelle.Temperature)
                    Console.WriteLine("Température:         : ✅");
                else
                    Console.WriteLine("Température:         : ❌");


                Console.SetCursorPosition(x, 6);
                if (selected.SolPreferee == parcelle.TypeSol)
                    Console.WriteLine("Sol préféré          : ✅");
                else
                    Console.WriteLine("Sol préféré          : ❌");

                Console.SetCursorPosition(x, 7);
                if (selected.EstDansSaisonPreferee)
                    Console.WriteLine("Saison de plantation : ✅");
                else
                    Console.WriteLine("Saison de plantation : ❌");
            }

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

    public string AfficherMenuActionDetail() // Méthode qui renvoi le string de l'action
    {
        List<string> options = new List<string>
    {
        "Arroser",
        "Traiter",
        "Récolter"
    };

        menuAction = new MenuChoix(options, "Choisissez une action sur la plante :");
        return options[menuAction.Afficher()];
    }
    public string AfficherMenuActionGeneral()
    {
        var options = new List<string>
    {
        "Planter",
        "Tout récolter",
        "Voir toutes les parcelles (Acheter)",
        "Sauvegarder la partie",
        "Passer à la semaine suivante",
        "Retour au menu principal"
    };

        var menu = new MenuChoix(options, @"Action générale :
        ");
        int choix = menu.Afficher();      // ← UNE seule lecture

        switch (choix)
        {
            case 0: // Planter
                return "Planter";
            case 1: // Tout récolter
                return "Tout récolter";
            case 2: // Voir toutes les parcelles
                return "Voir toutes les parcelles (Acheter)";
            case 3: // save
                return "Sauvegarder la partie";
            case 4: // Passer à la semaine suivante
                return "Passer à la semaine suivante";
            case 5:
                return "Retour au menu principal";

            default:
                return "";
        }
    }

    private void AfficherInfosParcelle(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("===== Statut de la parcelle =====");
        Console.ResetColor();

        Console.SetCursorPosition(x, y + 2);
        Console.WriteLine($"Sol : {parcelle.TypeSol}");

        Console.SetCursorPosition(x, y + 3);
        Console.WriteLine($"Ensoleillement : {parcelle.Ensoleillement}/100");

        Console.SetCursorPosition(x, y + 4);
        Console.WriteLine($"Température : {parcelle.Temperature}°C");

        Console.SetCursorPosition(x, y + 5);
        Console.WriteLine($"Pluie cette semaine : {(parcelle.Pluie ? "Oui" : "Non")}");

        Console.SetCursorPosition(x, y + 6);
        Console.WriteLine($"Saison actuelle: {partie!.SaisonActuelle}");

        Console.SetCursorPosition(x, y + 7);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Nombre d'action disponible : {parcelle!.NombreActionDispo}");
        Console.ResetColor();
    }

    private void AfficherPlantesDisponibles(int x, int y, List<Plante> plantes, int? selection)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🌱 Plantes disponibles :");
        Console.ResetColor();

        for (int i = 0; i < plantes.Count; i++)
        {
            Console.SetCursorPosition(x, y + 2 + i);
            if (selection == i)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine($"{i + 1}. {plantes[i].Nom}".PadRight(30));
            Console.ResetColor();
        }
    }
}

