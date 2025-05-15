using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public string teszt()
    {
        return (deck[12].Rank + " of " + deck[12].Suit);
    }
}
