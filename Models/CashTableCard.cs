using System.Collections.Generic;

namespace Models;

public class CashTableCard
{
    public int Code { get; set; }
    public string Value { get; set; }
    public string Suit { get; set; }
    public int Rank { get; set; }
    public CashTableCard(string value, string suit)
    {
        var codes = new Dictionary<string, int> {{"C", 100}, {"S", 200}, {"H", 300}, {"D", 400}};
        var ranks = new Dictionary<string, int> {{"2", 13}, {"3", 12}, {"4", 11}, {"5", 10}, {"6", 9}, {"7", 8}, {"8", 7}, {"9", 6}, {"T", 5}, {"J", 4}, {"Q", 3}, {"K", 2}, {"A", 1}};
        Code = codes[suit] + ranks[value];
        Value = value;
        Suit  = suit;
        Rank = ranks[value];
    }
}

public class CashTableCardType : CashTableCard
{ 
    public bool PlayerCard { get; set; }

    public CashTableCardType(
        string value,
        string suit,
        bool playerCard) :
        base(value, suit)

    { PlayerCard = playerCard; }
}

public class CashTableHandRank
{ 
    public int Rank { get; set; }
    public string? Name { get; set; }
    public List<CashTableCard> FullHand { get; set; } = new List<CashTableCard>();
}