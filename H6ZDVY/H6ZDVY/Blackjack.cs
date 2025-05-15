using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace H6ZDVY;
public class Blackjack
{
    public Card[] deck {  get; set; }

    public Blackjack(Card[] gottenDeck) 
    {
        deck = new Card[gottenDeck.Length];
        for (int i = 0; i < gottenDeck.Length; i++)
        {
            deck[i] = gottenDeck[i];
        }
    }

    public Card CardDraw()
    {
        Random random = new Random();
        while (true)
        {
            int rand = random.Next(0, deck.Length);
            if (!deck[rand].Drawn)
            {
                deck[rand].Drawn = true;
                return deck[rand];
            }
        }
    }

    public bool AceChecker(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].Value == 11)
            {
                return true;
            }
        }
        return false;
    }

    public bool Round()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i].Drawn = false;
        }
        int dealerPoint = 0;
        int playerPoint = 0;
        List<Card> dealerCards = new List<Card>();
        List<Card> playerCards = new List<Card>();
        for (int i = 0; i < 2; i++)
        {
            dealerCards.Add(CardDraw());
            dealerPoint += dealerCards[i].Value;
            playerCards.Add(CardDraw());
            playerPoint += playerCards[i].Value;
        }
        if(dealerPoint > 21 && AceChecker(dealerCards))
        {
            dealerPoint -= 10;
        }
        if (playerPoint > 21 && AceChecker(playerCards))
        {
            playerPoint -= 10;
        }
        while (true)
        {
            Console.WriteLine("Dealer cards: ");
            for (int i = 0; i < dealerCards.Count; i++)
            {
                Console.Write(dealerCards[i].Rank + " of " + dealerCards[i].Suit);
                if(i+1 != dealerCards.Count)
                {
                    Console.Write(" and ");
                }
            }
            Console.WriteLine("Your cards: ");
            for (int i = 0; i < playerCards.Count; i++)
            {
                Console.Write(playerCards[i].Rank + " of " + playerCards[i].Suit);
                if (i + 1 != playerCards.Count)
                {
                    Console.Write(" and ");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Write H for draw another card or write S to stop.");
            if(Console.ReadLine() == "S")
            {
                break;
            }
            else if(Console.ReadLine() == "H")
            {
            }
            else
            {
                Console.WriteLine("Error!");
            }
        }
    }

    public void Game(int money)
    {
        Console.WriteLine("");
        Console.WriteLine("You can quit anytime by writing Q!");
        Console.WriteLine("Current money: " + money);
        Console.WriteLine("");
    }
}
