using Xunit;
using Services;

namespace Tests;

public class HandProbabilityTest
{
    private HandProbabilityService HandProbability = new HandProbabilityService();

    [Fact]
    public void CheckRanking()
    {
        HandProbability.Increment(0, 5, 0, 5);
        Assert.Equal(4, HandProbability.RoyalFlush);
        Assert.Equal(36, HandProbability.StraightFlush);
        Assert.Equal(624, HandProbability.FourOfAkind);
        Assert.Equal(3744, HandProbability.FullHouse);
        Assert.Equal(5108, HandProbability.Flush);
        Assert.Equal(10200, HandProbability.Straight);
        Assert.Equal(54912, HandProbability.ThreeOfAkind);
        Assert.Equal(123552, HandProbability.TwoPair);
        Assert.Equal(1098240, HandProbability.Pair);
        Assert.Equal(1302540, HandProbability.HighCard);
    }

    [Fact]
    public void GetTotal()
    {
        var total = HandProbability.GetTotal(2, 3);
        Assert.Equal(2598960, total);
    }
}