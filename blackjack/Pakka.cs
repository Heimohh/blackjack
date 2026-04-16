using blackjack;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace blackjack
{
    // Kortin maa
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    // Kortin arvo
    public enum Rank
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    // Yksi kortti
    public readonly struct Card
    {
        public Rank Rank { get; }
        public Suit Suit { get; }

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        // Blackjack-arvo
        public int Value
        {
            get
            {
                return Rank switch
                {
                    Rank.Jack => 10,
                    Rank.Queen => 10,
                    Rank.King => 10,
                    Rank.Ace => 1,
                    _ => (int)Rank
                };
            }
        }

        public override string ToString()

        {
            string rankText = Rank switch
            {
                Rank.Ace => "A",
                Rank.Jack => "J",
                Rank.Queen => "Q",
                Rank.King => "K",
                _ => ((int)Rank).ToString()
            };

            string suitText = Suit switch
            {
                Suit.Clubs => "Risti",
                Suit.Diamonds => "Ruutu",
                Suit.Hearts => "Hertta",
                Suit.Spades => "Pata",
                _ => ""
            };

            return $"{suitText} {rankText}";
        }
    }
    }

    // Pakka
    public class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
            CreateDeck();
        }

        private void CreateDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(rank, suit));
                }
            }
        }

        public Card Draw()
        {
            
            int index = random.Next(cards.Count);
            Card card = cards[index];
            cards.RemoveAt(index);
            return card;
        }

        public int CardsRemaining
        {
            get { return cards.Count; }
        }
    }