using System.IO;
using Newtonsoft.Json;
using System;

public static class SauvegardeManager
{
    private const string CheminFichier = "sauvegardes/partie1.json";

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


    public static Partie? Charger()
    {
        if (!File.Exists(CheminFichier))
        {
            Console.WriteLine("❌ Aucun fichier de sauvegarde trouvé.");
            return null;
        }

        var json = File.ReadAllText(CheminFichier);
        return JsonConvert.DeserializeObject<Partie>(json, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
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
