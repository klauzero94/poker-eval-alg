using System.Collections;
using System.Collections.Generic;
using Models;

namespace Data;

public class GetActualHandOmahaData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //Caso 1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("8", "C"),
                new CashTableCard("A", "H"),
                new CashTableCard("9", "H"),
                new CashTableCard("2", "D"),
                new CashTableCard("9", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("7", "H"),
                new CashTableCard("T", "S"),
                new CashTableCard("K", "H"),
                new CashTableCard("8", "H")
            },
            //Expected
            "Two Pair",
            //Fail
            "High Card"
        };

        //Caso 2.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("5", "C"),
                new CashTableCard("8", "C"),
                new CashTableCard("J", "H"),
                new CashTableCard("3", "C"),
                new CashTableCard("7", "H")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("Q", "C"),
                new CashTableCard("7", "D"),
                new CashTableCard("8", "D"),
                new CashTableCard("5", "H")
            },
            //Expected
            "Two Pair",
            //Fail
            "High Card"
        };

        //Caso 2.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("5", "C"),
                new CashTableCard("8", "C"),
                new CashTableCard("J", "H"),
                new CashTableCard("3", "C"),
                new CashTableCard("7", "H")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("2", "D"),
                new CashTableCard("9", "H"),
                new CashTableCard("T", "H"),
                new CashTableCard("3", "D")
            },
            //Expected
            "Straight",
            //Fail
            "High Card"
        };

        //Caso 3.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("5", "C"),
                new CashTableCard("8", "C"),
                new CashTableCard("J", "H"),
                new CashTableCard("3", "C"),
                new CashTableCard("7", "H")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("Q", "C"),
                new CashTableCard("7", "D"),
                new CashTableCard("8", "D"),
                new CashTableCard("5", "H")
            },
            //Expected
            "Two Pair",
            //Fail
            "High Card"
        };

        //Caso 3.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("5", "C"),
                new CashTableCard("8", "C"),
                new CashTableCard("J", "H"),
                new CashTableCard("3", "C"),
                new CashTableCard("7", "H")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("2", "D"),
                new CashTableCard("9", "H"),
                new CashTableCard("T", "H"),
                new CashTableCard("3", "D")
            },
            //Expected
            "Straight",
            //Fail
            "High Card"
        };

        //Caso 4.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("8", "H"),
                new CashTableCard("J", "S"),
                new CashTableCard("9", "H"),
                new CashTableCard("5", "H"),
                new CashTableCard("6", "H")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("9", "D"),
                new CashTableCard("J", "H"),
                new CashTableCard("8", "S"),
                new CashTableCard("J", "C")
            },
            //Expected
            "Three of a kind",
            //Fail
            "High Card"
        };

        //Caso 4.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("8", "H"),
                new CashTableCard("J", "S"),
                new CashTableCard("9", "H"),
                new CashTableCard("5", "H"),
                new CashTableCard("6", "H")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("4", "D"),
                new CashTableCard("Q", "H"),
                new CashTableCard("T", "D"),
                new CashTableCard("A", "D")
            },
            //Expected
            "Straight",
            //Fail
            "High Card"
        };

        //Caso 5.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("5", "H"),
                new CashTableCard("Q", "D"),
                new CashTableCard("A", "C"),
                new CashTableCard("K", "D"),
                new CashTableCard("7", "C")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("T", "D"),
                new CashTableCard("6", "S"),
                new CashTableCard("J", "D"),
                new CashTableCard("5", "D")
            },
            //Expected
            "Straight",
            //Fail
            "High Card"
        };

        //Caso 5.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("5", "H"),
                new CashTableCard("Q", "D"),
                new CashTableCard("A", "C"),
                new CashTableCard("K", "D"),
                new CashTableCard("7", "C")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("2", "D"),
                new CashTableCard("9", "H"),
                new CashTableCard("T", "H"),
                new CashTableCard("3", "D")
            },
            //Expected
            "High Card",
            //Fail
            "Full House"
        };

        //Caso 6.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("7", "D"),
                new CashTableCard("3", "S"),
                new CashTableCard("9", "S"),
                new CashTableCard("7", "H"),
                new CashTableCard("8", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("8", "S"),
                new CashTableCard("J", "H"),
                new CashTableCard("7", "S"),
                new CashTableCard("6", "D")
            },
            //Expected
            "Full House",
            //Fail
            "High Card"
        };

        //Caso 6.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("7", "D"),
                new CashTableCard("3", "S"),
                new CashTableCard("9", "S"),
                new CashTableCard("7", "H"),
                new CashTableCard("8", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("A", "S"),
                new CashTableCard("A", "D"),
                new CashTableCard("3", "C"),
                new CashTableCard("T", "H")
            },
            //Expected
            "Two Pair",
            //Fail
            "Full House"
        };

        //Caso 7.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "D"),
                new CashTableCard("J", "C"),
                new CashTableCard("9", "D"),
                new CashTableCard("7", "H"),
                new CashTableCard("5", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "S"),
                new CashTableCard("J", "H"),
                new CashTableCard("9", "S"),
                new CashTableCard("4", "D")
            },
            //Expected
            "Four of a kind",
            //Fail
            "Three of a kind"
        };

        //Caso 7.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "D"),
                new CashTableCard("J", "C"),
                new CashTableCard("9", "D"),
                new CashTableCard("7", "H"),
                new CashTableCard("5", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("9", "S"),
                new CashTableCard("9", "C"),
                new CashTableCard("7", "C"),
                new CashTableCard("T", "H")
            },
            //Expected
            "Full House",
            //Fail
            "Royal Flush"
        };

        //Caso 8.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("Q", "C"),
                new CashTableCard("K", "H"),
                new CashTableCard("J", "C"),
                new CashTableCard("5", "C"),
                new CashTableCard("6", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("9", "D"),
                new CashTableCard("9", "S"),
                new CashTableCard("Q", "H"),
                new CashTableCard("3", "D")
            },
            //Expected
            "Pair",
            //Fail
            "Flush"
        };

        //Caso 8.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("Q", "C"),
                new CashTableCard("K", "H"),
                new CashTableCard("J", "C"),
                new CashTableCard("5", "C"),
                new CashTableCard("6", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("9", "H"),
                new CashTableCard("9", "C"),
                new CashTableCard("7", "H"),
                new CashTableCard("3", "H")
            },
            //Expected
            "Pair",
            //Fail
            "Straight"
        };

        //Caso 9.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "C"),
                new CashTableCard("T", "H"),
                new CashTableCard("9", "S"),
                new CashTableCard("7", "C"),
                new CashTableCard("5", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "D"),
                new CashTableCard("T", "S"),
                new CashTableCard("9", "D"),
                new CashTableCard("4", "D")
            },
            //Expected
            "Two Pair",
            //Fail
            "Straight"
        };

        //Caso 9.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "C"),
                new CashTableCard("T", "H"),
                new CashTableCard("9", "S"),
                new CashTableCard("7", "C"),
                new CashTableCard("5", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("9", "H"),
                new CashTableCard("9", "C"),
                new CashTableCard("7", "C"),
                new CashTableCard("3", "H")
            },
            //Expected
            "Three of a kind",
            //Fail
            "Four of a kind"
        };

        //Caso 10.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("T", "S"),
                new CashTableCard("4", "C"),
                new CashTableCard("2", "D"),
                new CashTableCard("A", "D"),
                new CashTableCard("3", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("Q", "C"),
                new CashTableCard("9", "S"),
                new CashTableCard("6", "C"),
                new CashTableCard("A", "H")
            },
            //Expected
            "Pair",
            //Fail
            "Two Pair"
        };

        //Caso 10.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("T", "S"),
                new CashTableCard("4", "C"),
                new CashTableCard("2", "D"),
                new CashTableCard("A", "D"),
                new CashTableCard("3", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("2", "C"),
                new CashTableCard("5", "H"),
                new CashTableCard("6", "S"),
                new CashTableCard("K", "D")
            },
            //Expected
            "Straight",
            //Fail
            "Pair"
        };

        //Caso 11.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("8", "C"),
                new CashTableCard("A", "H"),
                new CashTableCard("9", "H"),
                new CashTableCard("2", "D"),
                new CashTableCard("8", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "S"),
                new CashTableCard("A", "D"),
                new CashTableCard("2", "C"),
                new CashTableCard("7", "D")
            },
            //Expected
            "Two Pair",
            //Fail
            "Pair"
        };

        //Caso 11.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("8", "C"),
                new CashTableCard("A", "H"),
                new CashTableCard("9", "H"),
                new CashTableCard("2", "D"),
                new CashTableCard("8", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("7", "H"),
                new CashTableCard("T", "S"),
                new CashTableCard("K", "H"),
                new CashTableCard("8", "H")
            },
            //Expected
            "Three of a kind",
            //Fail
            "Pair"
        };

        //Caso 12.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("K", "S"),
                new CashTableCard("9", "H"),
                new CashTableCard("5", "S"),
                new CashTableCard("4", "C"),
                new CashTableCard("Q", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "D"),
                new CashTableCard("6", "S"),
                new CashTableCard("5", "C"),
                new CashTableCard("5", "H")
            },
            //Expected
            "Three of a kind",
            //Fail
            "Pair"
        };

        //Caso 12.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("K", "S"),
                new CashTableCard("9", "H"),
                new CashTableCard("5", "S"),
                new CashTableCard("4", "C"),
                new CashTableCard("Q", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("Q", "S"),
                new CashTableCard("3", "C"),
                new CashTableCard("7", "S"),
                new CashTableCard("9", "S")
            },
            //Expected
            "Two Pair",
            //Fail
            "Pair"
        };

        //Caso 13
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("4", "D"),
                new CashTableCard("5", "H"),
                new CashTableCard("9", "C"),
                new CashTableCard("6", "H"),
                new CashTableCard("K", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "H"),
                new CashTableCard("3", "D"),
                new CashTableCard("J", "D"),
                new CashTableCard("Q", "D")
            },
            //Expected
            "Pair",
            //Fail
            "Flush"
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class GetActualHandTexasData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //Caso 1.1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("4", "D"),
                new CashTableCard("5", "H"),
                new CashTableCard("T", "C"),
                new CashTableCard("Q", "H"),
                new CashTableCard("K", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("J", "H"),
                new CashTableCard("9", "D")
            },
            //Expected
            "Straight",
            //Fail
            "Flush"
        };

        //Caso 1.2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("4", "D"),
                new CashTableCard("5", "H"),
                new CashTableCard("T", "C"),
                new CashTableCard("Q", "H"),
                new CashTableCard("K", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("A", "C"),
                new CashTableCard("8", "S")
            },
            //Expected
            "High Card",
            //Fail
            "Flush"
        };

        // caso 2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("T", "D"),
                new CashTableCard("J", "D"),
                new CashTableCard("T", "C"),
                new CashTableCard("Q", "D"),
                new CashTableCard("K", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("A", "D"),
                new CashTableCard("8", "S")
            },
            //Expected
            "Royal Flush",
            //Fail
            "Flush"
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class GetActualHandCustomData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //Caso 1
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("4", "D"),
                new CashTableCard("5", "D"),
                new CashTableCard("3", "D"),
                new CashTableCard("2", "D"),
                new CashTableCard("Q", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("A", "D"),
                new CashTableCard("8", "D")
            },
            //Expected
            "Straight Flush",
            //Fail
            "Flush"
        };

        //Caso 2
        yield return new object[]
        {
            //ExposedCards
            new List<CashTableCard>
            {
                new CashTableCard("A", "H"),
                new CashTableCard("J", "D"),
                new CashTableCard("9", "D"),
                new CashTableCard("T", "D"),
                new CashTableCard("Q", "D")
            },
            //PlayerCards
            new List<CashTableCard>
            {
                new CashTableCard("A", "D"),
                new CashTableCard("K", "D")
            },
            //Expected
            "Royal Flush",
            //Fail
            "Flush"
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}