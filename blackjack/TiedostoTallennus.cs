using System;
using System.IO;

namespace blackjack;

public static class ScoreManager
{
    private const string FilePath = "scores.txt";

    public static void AskToSaveScore(Player player)
    {
        if (player.Raha <= 0)
        {
            return;
        }

        Console.Write("Haluatko tallentaa tuloksen? (k/e): ");
        string? answer = Console.ReadLine()?.Trim().ToLower();

        if (answer == "k")
        {
            Console.Write("Anna nimi: ");
            string? name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
            {
                File.AppendAllText(FilePath, $"{name};{player.Raha}{Environment.NewLine}");
                Console.WriteLine("Tulos tallennettu.");
            }
        }
    }

    public static void AskToShowScores()
    {
        Console.Write("Haluatko nähdä tallennetut tulokset? (k/e): ");
        string? answer = Console.ReadLine()?.Trim().ToLower();

        if (answer != "k")
        {
            return;
        }

        if (!File.Exists(FilePath))
        {
            Console.WriteLine("Ei tallennettuja tuloksia.");
            return;
        }

        Console.WriteLine("--- Tallennetut tulokset ---");

        foreach (string line in File.ReadAllLines(FilePath))
        {
            Console.WriteLine(line);
        }
    }
}