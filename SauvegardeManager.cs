using System.IO;
using Newtonsoft.Json;
using System;

public static class SauvegardeManager
{
    public static void Sauvegarder(Partie partie, string nomSauvegarde)
    {
        string chemin = $"sauvegardes/{nomSauvegarde}.json";

        var json = JsonConvert.SerializeObject(partie, Formatting.Indented,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

        Directory.CreateDirectory("sauvegardes");
        File.WriteAllText(chemin, json);
        Console.WriteLine("✅ Partie sauvegardée !");
    }

    // Permet de lister les fichiers de la sauvegarde (nathan, claire, etc)
    public static List<string> ListerSauvegardesDisponibles()
    {
        var fichiers = Directory.GetFiles("sauvegardes", "*.json");

        return fichiers.Select(f => Path.GetFileNameWithoutExtension(f)).ToList();
    }


    public static Partie? Charger(string nomSauvegarde)
    {
        string chemin = $"sauvegardes/{nomSauvegarde}.json";

        if (!File.Exists(chemin))
        {
            Console.WriteLine("❌ Fichier introuvable.");
            return null;
        }

        string json = File.ReadAllText(chemin);
        return JsonConvert.DeserializeObject<Partie>(json, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
    }


}
