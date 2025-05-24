using System;
using System.Threading;

public class ModeUrgence
{
    private static Random rnd = new Random();
    // Table des Fleches et leurs symboles
    private static (ConsoleKey key, string symbole)[] Fleches = new[]
    {
        (ConsoleKey.UpArrow,    "↑"),
        (ConsoleKey.DownArrow,  "↓"),
        (ConsoleKey.LeftArrow,  "←"),
        (ConsoleKey.RightArrow, "→"),
    };

    public static void Lancer(Parcelle parcelle, int rounds = 10, int barLength = 20, int speedMs = 30)
    {
        int score = 0;

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("⚠️ Votre Parcelle se fait attaquer par des oiseaux ⚠️");
        Console.WriteLine("Préparez-vous au QTE !");
        Console.WriteLine("");
        Console.WriteLine("Appuyez sur une touche pour le lancer le QTE...");
        Console.ResetColor();
        Console.ReadKey();

        for (int round = 1; round <= rounds; round++)
        {
            // Choix aléatoire de la flèche à presser
            var (targetKey, symbole) = Fleches[rnd.Next(Fleches.Length)];
            bool success = false;

            for (int step = 0; step <= barLength; step++) // Animation de la barre
            {
                Console.Clear();
                Console.WriteLine($"⚠️  Mode Urgence  ⚠️{round}/{rounds}");
                Console.WriteLine($"Appuyez sur [{symbole}] !");
                // Barre visuelle : ■ plein → vidé vers la droite
                string bar = new string('■', barLength - step)
                           + new string(' ', step);
                Console.Write($"[{bar}]");

                var start = Environment.TickCount;
                while (Environment.TickCount - start < speedMs)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (key == targetKey)
                            success = true;
                        // Vider le buffer pour éviter multiple lectures
                        while (Console.KeyAvailable)
                            Console.ReadKey(true);
                        step = barLength + 1; // sortir de la boucle barre
                        break;
                    }
                    Thread.Sleep(10);
                }

                if (success) break; // on sort dès le succès
            }

            // Affichage du résultat du round
            Console.Clear();
            if (success)
            {
                Console.WriteLine($"✅ Succès !");
                score++;
            }
            else
            {
                Console.WriteLine($"❌ Échec…");
            }
            Thread.Sleep(500);
        }

        Console.Clear();
        Console.WriteLine($"🎯 Mode Urgence terminé : {score}/{rounds} réussites.");
        if (score < rounds / 2)
        {
            Console.WriteLine("Votre score est trop faible et les oiseaux ont mangé la moitié de vos récoltes...");
            for (int y = 0; y < parcelle.Hauteur; y++)
                for (int x = 0; x < parcelle.Largeur; x++)
                {
                    var plante = parcelle.MatriceEtat[y, x];
                    if (plante != null)
                    {
                        plante.Croissance /= 2;
                    }
                }
        }
        else
        {
            Console.WriteLine("Votre score est assez élévé ! Les oiseaux se sont enfuis devant votre puissance !");
        }
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }
}
