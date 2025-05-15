using System;
using System.Threading;

public static class AffichageChargement
{

    public static void Afficher(int semaineActuelle, int dureeTotaleMs = 2000)
    {
        Console.Clear();
        Console.WriteLine($"⏳ Passage à la semaine {semaineActuelle + 1}…\n");

        Console.WriteLine("\n✔️ Semaine mise à jour !");
        Thread.Sleep(800); // Petite pause avant de revenir au jeu
    }
}