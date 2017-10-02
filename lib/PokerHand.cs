
public class PokerHand
{
    public List<Card> cards = new List<Card>();
    public PokerHandType Type { get; private set; }

    public PokerHand(string hand)
    {
        foreach (var card in Regex.Split(hand, " "))
        {
            cards.Add(new Card(card));
        }
        TypeAndSort();

    }

    public Result CompareWith(PokerHand hand)
    {
        if (Type < hand.Type) { return Result.Win; }
        if (Type > hand.Type) { return Result.Loss; }
        return TieBreaker(hand);
    }

    private Result TieBreaker(PokerHand hand)
    {
        for (int i = 0; i < 5; i++)
        {
            Result result = cards[i].CompareWith(hand.cards[i]);
            if (result != Result.Tie) { return result; }
        }
        return Result.Tie;
    }

    private void TypeAndSort()
    {
        cards = sort(cards);
        if (RoyalFlush()) { Type = PokerHandType.RoyalFlush; return; }
        if (StraightFlush()) { Type = PokerHandType.StraightFlush; return; }
        if (FourOfAKind()) { Type = PokerHandType.FourOfAKind; return; }
        if (FullHouse()) { Type = PokerHandType.FullHouse; return; }
        if (Flush()) { Type = PokerHandType.Flush; return; }
        if (Straight()) { Type = PokerHandType.Straight; return; }
        if (ThreeOfAKind()) { Type = PokerHandType.ThreeOfAKind; return; }
        if (TwoPair()) { Type = PokerHandType.TwoPair; return; }
        if (Pair()) { Type = PokerHandType.Pair; return; }
        Type = PokerHandType.HighCard;
    }

    private List<Card> sort(List<Card> cards)
    {
        cards.Sort((card1, card2) => card2.Value.CompareTo(card1.Value));
        var sorted = cards.GroupBy(card => card.Value)
                          .OrderByDescending(group => group.Count())
                          .SelectMany(group => group.OrderByDescending(card => card.Value))
                          .ToList();
        return sorted;
    }

    private bool Straight()
    {
        for (int i = 0; i < 4; i++)
        {
            bool result = (cards[i].Value - 1) == cards[i + 1].Value;
            //if it's the first card in a hand, and that card is an ace, it should be checked as a low card instead of high
            if (i == 0 && cards[0].Value == Values.Ace && cards[1].Value == Values.Five) { result = true; } 
            if (!result) { return false; }
        }
        return true;
    }

    private bool RoyalFlush()
    {
        return (StraightFlush() && cards[0].Value == (Values.Ace));
    }

    private bool StraightFlush()
    {
        return Straight() && Flush();
    }

    private bool Flush()
    {
        return cards.GroupBy(card => card.Suit).Any(group => group.Count() == 5);
    }

    private bool FourOfAKind()
    {
        return cards.GroupBy(card => card.Value).Any(group => group.Count() == 4);
    }

    private bool ThreeOfAKind()
    {
        return cards.GroupBy(card => card.Value).Any(group => group.Count() == 3);
    }

    private bool Pair()
    {
        return cards.GroupBy(card => card.Value).Any(group => group.Count() == 2);
    }

    private bool FullHouse()
    {
        return Pair() && ThreeOfAKind();
    }

    private bool TwoPair()
    {
        return cards.GroupBy(card => card.Value).Count(group => group.Count() >= 2) == 2;
    }
}
