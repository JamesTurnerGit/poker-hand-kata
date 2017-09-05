
public class Card
{
    public Suits Suit { get; private set; }
    public Values Value { get; private set; }

    public Card(string str)
    {
        Value = GetValue(str[0].ToString());
        Suit = GetSuit(str[1].ToString());
    }

    public Result CompareWith(Card othercard)
    {
        var OtherValue = othercard.Value;
        if (Value > OtherValue) { return Result.Win; }
        if (Value < OtherValue) { return Result.Loss; }
        return Result.Tie;
    }

    private Suits GetSuit(string str)
    {
        switch (str)
        {
            case "D": { return Suits.Diamonds; }
            case "H": { return Suits.Hearts; }
            case "S": { return Suits.Spades; }
            case "C": { return Suits.Clubs; }
        }
        throw new System.ArgumentException("card Suit invalid");
    }

    private Values GetValue(string str)
    {
        switch (str)
        {
            case "2": { return Values.Two; }
            case "3": { return Values.Three; }
            case "4": { return Values.Four; }
            case "5": { return Values.Five; }
            case "6": { return Values.Six; }
            case "7": { return Values.Seven; }
            case "8": { return Values.Eight; }
            case "9": { return Values.Nine; }
            case "T": { return Values.Ten; }
            case "J": { return Values.Jack; }
            case "Q": { return Values.Queen; }
            case "K": { return Values.King; }
            case "A": { return Values.Ace; }
        }
        throw new System.ArgumentException("card Value invalid");
    }
}
