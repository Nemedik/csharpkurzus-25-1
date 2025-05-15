using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6ZDVY;
public static class Mapper
{
    public static JsonCardDto ToDto(this Card card)
    {
        return new JsonCardDto
        {
            Rank = card.Rank,
            Suit = card.Suit,
            Value = card.Value,
            Drawn = card.Drawn
        };
    }

    public static Card ToDomainObject(this JsonCardDto card)
    {
        return new Card
        {
            Rank = card.Rank,
            Suit = card.Suit,
            Value = card.Value,
            Drawn = card.Drawn
        };
    }
}
