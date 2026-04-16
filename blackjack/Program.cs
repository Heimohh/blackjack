namespace blackjack;

internal class Program
{
    static void Main(string[] args)
    {
        Player player = new Player();
        bool playing = true;

        Console.WriteLine("=== Blackjack ===");

        while (playing && player.Raha > 0)
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Rahaa: {player.Raha}");
            Console.WriteLine("---------------------------------");

            int bet = AskBet(player.Raha);

            Deck deck = new Deck();
            Dealer dealer = new Dealer();

            player.ClearHand();
            dealer.ClearHand();

            player.TakeCard(deck.Draw());
            dealer.TakeCard(deck.Draw());
            player.TakeCard(deck.Draw());
            dealer.TakeCard(deck.Draw());

            Console.WriteLine();
            dealer.ShowHand();
            Console.WriteLine();
            player.ShowHand();

            bool playerBlackjack = player.Hand.Count == 2 && player.GetHandValue() == 21;
            bool dealerBlackjack = dealer.Hand.Count == 2 && dealer.GetHandValue() == 21;

            if (playerBlackjack || dealerBlackjack)
            {
                Console.WriteLine();
                dealer.ShowFullHand();
                Console.WriteLine();

                if (playerBlackjack && dealerBlackjack)
                {
                    Console.WriteLine("Molemmilla blackjack. Tasapeli.");
                }
                else if (playerBlackjack)
                {
                    Console.WriteLine("Blackjack! Voitit kierroksen.");
                    player.Raha += bet;
                }
                else
                {
                    Console.WriteLine("Jakajalla blackjack. Hävisit kierroksen.");
                    player.Raha -= bet;
                }

                playing = AskContinue(player.Raha);
                continue;
            }

            bool playerBust = false;

            while (true)
            {
                Console.WriteLine();
                Console.Write("Valitse toiminto (h = hit, s = stand): ");
                string? choice = Console.ReadLine()?.Trim().ToLower();

                if (choice == "h")
                {
                    Card card = deck.Draw();
                    player.TakeCard(card);

                    Console.WriteLine($"Sait kortin: {card}");
                    Console.WriteLine();
                    player.ShowHand();

                    if (player.GetHandValue() > 21)
                    {
                        Console.WriteLine("Meni yli 21. Hävisit kierroksen.");
                        player.Raha -= bet;
                        playerBust = true;
                        break;
                    }
                }
                else if (choice == "s")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Virheellinen valinta.");
                }
            }

            if (playerBust)
            {
                playing = AskContinue(player.Raha);
                continue;
            }

            Console.WriteLine();
            Console.WriteLine("Jakajan vuoro:");
            dealer.ShowFullHand();
            Console.WriteLine();

            dealer.PlayTurn(deck);

            dealer.ShowFullHand();

            Console.WriteLine();
            player.ShowHand();

            int playerValue = player.GetHandValue();
            int dealerValue = dealer.GetHandValue();

            Console.WriteLine();

            if (dealerValue > 21)
            {
                Console.WriteLine("Jakaja meni yli 21. Voitit!");
                player.Raha += bet;
            }
            else if (playerValue > dealerValue)
            {
                Console.WriteLine("Voitit kierroksen!");
                player.Raha += bet;
            }
            else if (playerValue < dealerValue)
            {
                Console.WriteLine("Hävisit kierroksen.");
                player.Raha -= bet;
            }
            else
            {
                Console.WriteLine("Tasapeli.");
            }

            playing = AskContinue(player.Raha);
        }

        Console.WriteLine();

        if (player.Raha <= 0)
        {
            Console.WriteLine("Rahat loppuivat. Peli päättyi.");
        }
        else
        {
            Console.WriteLine("Kiitos pelaamisesta!");
            ScoreManager.AskToSaveScore(player);
            ScoreManager.AskToShowScores();
        }
    }

    static int AskBet(int maxMoney)
    {
        while (true)
        {
            Console.Write($"Anna panos (1-{maxMoney}): ");
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int bet) && bet >= 1 && bet <= maxMoney)
            {
                return bet;
            }

            Console.WriteLine("Virheellinen panos.");
        }
    }

    static bool AskContinue(int money)
    {
        if (money <= 0)
        {
            return false;
        }

        while (true)
        {
            Console.Write("Haluatko pelata uudestaan? (k/e): ");
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "k")
            {
                return true;
            }

            if (input == "e")
            {
                return false;
            }

            Console.WriteLine("Virheellinen valinta.");
        }
    }
}