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
        bool roundIsOver = false;
        int result = 0;
        //Player draw phase
        while (true)
        {
            Console.WriteLine(Environment.NewLine + "Dealer cards: ");
            for (int i = 0; i < dealerCards.Count; i++)
            {
                Console.Write(dealerCards[i].Rank + " of " + dealerCards[i].Suit);
                if(i+1 != dealerCards.Count)
                {
                    Console.Write(" and ");
                }
            }
            Console.Write(" (Total value: " + dealerPoint + ")" + Environment.NewLine);
            Console.WriteLine("Your cards: ");
            for (int i = 0; i < playerCards.Count; i++)
            {
                Console.Write(playerCards[i].Rank + " of " + playerCards[i].Suit);
                if (i + 1 != playerCards.Count)
                {
                    Console.Write(" and ");
                }
            }
            Console.Write(" (Total value: " + playerPoint + ")" + Environment.NewLine);
            if (roundIsOver)
            {
                return result;
            }
            Console.WriteLine("Write H to draw another card or write S to stop drawing.");
            string line = Console.ReadLine();
            if(line == "s" || line == "S")
            {
                break;
            }
            else if(line == "h" || line == "H")
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
                        Console.WriteLine(Environment.NewLine + "You lost!" + Environment.NewLine);
                        roundIsOver = true;
                        result = -1;
                        continue;
                    }
                }
            }
            else
            {
                Console.WriteLine("Wrong character!" + Environment.NewLine);
            }
        }
        //Dealer starts to draw
        while(true)
        {
            Console.WriteLine(Environment.NewLine + "Dealer cards: ");
            for (int i = 0; i < dealerCards.Count; i++)
            {
                Console.Write(dealerCards[i].Rank + " of " + dealerCards[i].Suit);
                if (i + 1 != dealerCards.Count)
                {
                    Console.Write(" and ");
                }
            }
            Console.Write(" (Total value: " + dealerPoint + ")" + Environment.NewLine);
            Console.WriteLine("Your cards: ");
            for (int i = 0; i < playerCards.Count; i++)
            {
                Console.Write(playerCards[i].Rank + " of " + playerCards[i].Suit);
                if (i + 1 != playerCards.Count)
                {
                    Console.Write(" and ");
                }
            }
            Console.Write(" (Total value: " + playerPoint + ")" + Environment.NewLine);
            if (roundIsOver)
            {
                return result;
            }
            if (dealerPoint > 21)
            {
                
                Console.WriteLine(Environment.NewLine + "You won!" + Environment.NewLine);
                roundIsOver = true;
                result = 1;
                continue;
            }
            if (dealerPoint >= 17)
            {
                if (playerPoint > dealerPoint)
                {
                    Console.WriteLine(Environment.NewLine + "You won!" + Environment.NewLine);
                    roundIsOver = true;
                    result = 1;
                    continue;
                }
                else if (playerPoint < dealerPoint)
                {
                    Console.WriteLine(Environment.NewLine + "You lost!" + Environment.NewLine);
                    roundIsOver = true;
                    result = -1;
                    continue;
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "The game was a tie!" + Environment.NewLine);
                    roundIsOver = true;
                    result = 0;
                    continue;
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

    public void Game(int chips)
    {
        while(true)
        {
            Console.WriteLine(Environment.NewLine + "Current chips: " + chips);
            Console.WriteLine(Environment.NewLine + "Write how much do you want to bet, or write Q to quit!");
            string line = Console.ReadLine();
            if (line == "q" || line == "Q")
            {
                return;
            }
            int bet;
            if(!int.TryParse(line, out bet))
            {
                Console.WriteLine("Not a Q nor a number!" + Environment.NewLine);
                continue;
            }
            if(bet > chips)
            {
                Console.WriteLine("That's a higher bet than your current chips!" + Environment.NewLine);
                continue;
            }
            int match = Round();
            if(match == 1)
            {
                chips += bet;
            }
            else if(match == -1)
            {
                chips -= bet;
                if(chips <= 0)
                {
                    Console.WriteLine("You lost everything! Quitting from game...");
                    return;
                }
            }
        }
    }
}
