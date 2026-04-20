namespace blackjack;

//Rajapinta
public interface IParticipant
{
    List<Card> Hand { get; }
    void TakeCard(Card card);
    void ClearHand();
    int GetHandValue();
    void ShowHand();
}

public class Participant : IParticipant
{
    public List<Card> Hand { get; } = new();

    public void TakeCard(Card card)
    {
        Hand.Add(card);
    }

    public void ClearHand()
    {
        Hand.Clear();
    }

    // Laskee käden arvon ja huomioi ässän arvon joko 1 tai 11.
    public int GetHandValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (var card in Hand)
        {
            total += card.Value;

            if (card.Rank == Rank.Ace)
            {
                aceCount++;
            }
        }

        while (aceCount > 0 && total + 10 <= 21)
        {
            total += 10;
            aceCount--;
        }

        return total;
    }

    protected void PrintCards()
    {
        foreach (var card in Hand)
        {
            Console.WriteLine(card);
        }
    }

    public virtual void ShowHand()
    {
        PrintCards();
        Console.WriteLine($"Arvo: {GetHandValue()}");
    }
}

public class Player : Participant
{
    public int Raha { get; set; } = 100;

    public override void ShowHand()
    {
        Console.WriteLine("Pelaajan kortit:");
        base.ShowHand();
    }
}

public class Dealer : Participant
{

    //Pidetään yksi kortti aluksi piilossa.
    public override void ShowHand()
    {
        Console.WriteLine("Jakajan kortit:");
        Console.WriteLine("[Piilotettu kortti]");

        for (int i = 1; i < Hand.Count; i++)
        {
            Console.WriteLine(Hand[i]);
        }
    }

    public void ShowFullHand()
    {
        Console.WriteLine("Jakajan kortit:");
        base.ShowHand();
    }

    public void PlayTurn(Deck deck)
    {
        while (GetHandValue() < 17)
        {
            TakeCard(deck.Draw());
        }
    }
}
