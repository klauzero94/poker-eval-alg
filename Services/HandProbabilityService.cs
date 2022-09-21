using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services;

public class HandProbabilityService
{
    private List<string> Codes = new List<string> {{"C"}, {"S"}, {"H"}, {"D"}};
    private List<string> Ranks = new List<string> {{"2"}, {"3"}, {"4"}, {"5"}, {"6"}, {"7"}, {"8"}, {"9"}, {"T"}, {"J"}, {"Q"}, {"K"}, {"A"}};
    private List<CashTableCard> AllCards = new List<CashTableCard>();
    private GetActualHandService GetActualHand = new GetActualHandService();
    private List<CashTableCard> exposedCards = new List<CashTableCard>();
    private List<CashTableCard> playerCards = new List<CashTableCard>();
    public int RoyalFlush { get; set; }
    public int StraightFlush { get; set; }
    public int FourOfAkind { get; set; }
    public int FullHouse { get; set; }
    public int Flush { get; set; }
    public int Straight { get; set; }
    public int ThreeOfAkind { get; set; }
    public int TwoPair { get; set; }
    public int Pair { get; set; }
    public int HighCard { get; set; }
    
    public HandProbabilityService()
    {
        Codes.ForEach(x => {
            Ranks.ForEach(y => {
                AllCards.Add(new CashTableCard(y, x));
            });
        });
    }

    private IEnumerable<int[]> CombinationsRosettaWoRecursion(int m, int n)
    {
        int[] result = new int[m];
        Stack<int> stack = new Stack<int>(m);
        stack.Push(0);
        while (stack.Count > 0)
        {
            int index = stack.Count - 1;
            int value = stack.Pop();
            while (value < n)
            {
                result[index++] = value++;
                stack.Push(value);
                if (index != m) continue;
                yield return (int[])result.Clone();
                break;
            }
        }
    }

    public void Increment(int playerCardsQuantity, int exposedCardsQuantity, int requiredHandCards, int fullHandQuantity)
    {
        int count = 0;
        int period = 50000;
        int totalElements = playerCardsQuantity + exposedCardsQuantity;
        if (AllCards.Count < totalElements)
            throw new ArgumentException("List length can't be less than number of selected elements");
        if (totalElements < 1)
            throw new ArgumentException("Number of selected elements can't be less than 1");
        foreach (int[] j in CombinationsRosettaWoRecursion(totalElements, AllCards.Count))
        {
            exposedCards = new List<CashTableCard>();
            playerCards = new List<CashTableCard>();
            for (int i = 0; i < totalElements; i++)
            {
                if (i <= exposedCardsQuantity)
                    exposedCards.Add(AllCards.ElementAt(j[i]));
                else
                    playerCards.Add(AllCards.ElementAt(j[i]));
            }
            var hand = GetActualHand.GetActualHand(exposedCards, playerCards, requiredHandCards, fullHandQuantity);
            switch (hand.Result.Rank)
            {
                case 1:
                    RoyalFlush = RoyalFlush + 1;
                break;
                case 2:
                    StraightFlush = StraightFlush + 1;
                break;
                case 3:
                    FourOfAkind = FourOfAkind + 1;
                break;
                case 4:
                    FullHouse = FullHouse + 1;
                break;
                case 5:
                    Flush = Flush + 1;
                break;
                case 6:
                    Straight = Straight + 1;
                break;
                case 7:
                    ThreeOfAkind = ThreeOfAkind + 1;
                break;
                case 8:
                    TwoPair = TwoPair + 1;
                break;
                case 9:
                    Pair = Pair + 1;
                break;
                case 10:
                    HighCard = HighCard + 1;
                break;
            }
            count = count + 1;
            if (count == period)
            {
                System.Diagnostics.Debug.WriteLine(count);
                period = period + 50000;
            }
            GC.Collect();
        }
    }

    public int GetTotal(int playerCardsQuantity, int exposedCardsQuantity)
    {
        int count = 0;
        int totalElements = playerCardsQuantity + exposedCardsQuantity;
        if (AllCards.Count < totalElements)
            throw new ArgumentException("List length can't be less than number of selected elements");
        if (totalElements < 1)
            throw new ArgumentException("Number of selected elements can't be less than 1");
        foreach (int[] j in CombinationsRosettaWoRecursion(totalElements, AllCards.Count))
            count = count + 1;
        return count;
    }
}