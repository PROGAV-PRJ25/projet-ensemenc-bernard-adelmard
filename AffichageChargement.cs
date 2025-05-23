using System;
using System.Threading;

public class AffichageChargement
{
    private static readonly Random rnd = new Random();

    public static void Afficher(int semaineActuelle, Parcelle parcelle)
    {
        string caractere = "|";
        string ligne = "|";

        for (int i = 0; i <= 40; i++)
        {
            Console.Clear();
            Console.WriteLine($"⏳ Passage à la semaine {semaineActuelle + 1}…\n");
            Console.WriteLine();
            Console.WriteLine($"{ligne}");
            ligne += caractere;
            Thread.Sleep(40);
        }

        int proba = 26;
        if (rnd.Next(0, 101) < proba)
        {
            ModeUrgence.Lancer(parcelle);
        }

        Console.WriteLine("\n✔️ Semaine passée !");
        Thread.Sleep(1000); // Petite pause avant de revenir au jeu
    }
}