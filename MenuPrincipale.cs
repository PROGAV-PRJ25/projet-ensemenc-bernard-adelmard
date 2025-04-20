public class MenuPrincipal : Menu
{
    public MenuPrincipal()
    {
        options = new string[]
        {
            "Nouvelle Partie",
            "Charger une Partie",
            "Règles du jeu",
            "Quitter"
        };
    }

    protected override void AfficherTitre()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("      🍇 Le Jeu Viticole 🍇        ");
        Console.WriteLine("====================================\n");
        Console.WriteLine("Utilisez les flèches ↑ ↓ pour naviguer, Entrée pour valider.\n");
    }
}
