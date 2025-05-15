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

    public int AceChecker(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].Value == 11)
            {
                return i;
            }
        }
        return -1;
    }

    public int Round() // -1 if lose, 0 if tie, 1 if win
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i].Drawn = false;
            if(deck[i].Value == 1)
            {
                deck[i].Value = 11;
            }
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
        if(dealerPoint > 21 && AceChecker(dealerCards) != -1)
        {
            dealerCards[AceChecker(dealerCards)].Value = 1;
            dealerPoint -= 10;
        }
        if (playerPoint > 21 && AceChecker(playerCards) != -1)
        {
            playerCards[AceChecker(playerCards)].Value = 1;
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
            Console.WriteLine("");
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
            string line = Console.ReadLine();
            if(line == "S" || line == "s")
            {
                break;
            }
            else if(line == "H" || line == "h")
            {
                Card card = CardDraw();
                playerCards.Add(card);
                playerPoint += card.Value;
                if(playerPoint > 21)
                {
                    if(AceChecker(playerCards) != -1)
                    {
                        playerCards[AceChecker(playerCards)].Value = 1;
                        playerPoint -= 10;
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("You lost!");
                        Console.WriteLine("");
                        return -1;
                    }
                }
            }
            else
            {
                Console.WriteLine("Wrong character!");
            }
        }
        while(true)
        {
            if(dealerPoint > 21)
            {
                Console.WriteLine("");
                Console.WriteLine("You won!");
                Console.WriteLine("");
                return 1;
            }
            if (dealerPoint >= 17)
            {
                if (playerPoint > dealerPoint)
                {
                    Console.WriteLine("");
                    Console.WriteLine("You won!");
                    Console.WriteLine("");
                    return 1;
                }
                else if (playerPoint < dealerPoint)
                {
                    Console.WriteLine("");
                    Console.WriteLine("You lost!");
                    Console.WriteLine("");
                    return -1;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("The game was a tie!");
                    Console.WriteLine("");
                    return 0;
                }
            }
            Card card = CardDraw();
            dealerCards.Add(card);
            dealerPoint += card.Value;
            if(dealerPoint > 21 && AceChecker(dealerCards) != -1)
            {
                dealerCards[AceChecker(dealerCards)].Value = 1;
                dealerPoint -= 10;
            }
        }
    }

    public void Game(int money)
    {
        while(true)
        {
            Console.WriteLine("Current money: " + money);
            Console.WriteLine("Press enter to play a round, or write Q to quit!");
            string line = Console.ReadLine();
            if (line == "Q" || line == "q")
            {
                return;
            }
            int match = Round();
            if(match == 1)
            {
                money += 500;
            }
            else if(match == -1)
            {
                money -= 500;
            }
        }
    }
}
