using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Services;

public class GetActualHandService
{
    public virtual Task<CashTableHandRank> GetActualHand(List<CashTableCard> exposedCards, List<CashTableCard> playerCards, int requiredHandCards, int fullHandQuantity)
    {
        var cards = new List<CashTableCardType>();
        var hand = new CashTableHandRank();

        exposedCards.ForEach(c => {cards.Add(new CashTableCardType(c.Value, c.Suit, false));});
        playerCards.ForEach(c => {cards.Add(new CashTableCardType(c.Value, c.Suit, true));});
        cards = cards.OrderBy(c => c.Code).ToList();

        var acesSteelWheel = new List<CashTableCardType>();
        var cardsLowSeq = new List<CashTableCardType>();
        cards.ForEach(c =>
        {
            if (c.Rank == 1)
                acesSteelWheel.Add(new CashTableCardType(c.Value, c.Suit, c.PlayerCard));

            cardsLowSeq.Add(c);
        });
        acesSteelWheel.ForEach(a => {a.Code = a.Code + 13;});
        cardsLowSeq.AddRange(acesSteelWheel);

        #region 1.Royal flush or 2.Straight flush
        var highSeq = new List<CashTableCardType>();

        for (int i = 0; i <= (cardsLowSeq.Count - fullHandQuantity); i++)
        {
            var codes = cardsLowSeq.Skip(i).Select(c => c.Code).Take(fullHandQuantity).ToList();
            
            if (codes.Count > 0)
                if (Enumerable.Range(codes[0], fullHandQuantity).Except(codes).Count() == 0)
                {
                    foreach (var code in codes)
                    {
                        if (highSeq.Where(c => c.PlayerCard).ToList().Count >= requiredHandCards && requiredHandCards > 0)
                        {
                            if (cardsLowSeq.Where(c => !c.PlayerCard && c.Code == code).ToList().Count > 0)
                                highSeq.Add(cardsLowSeq.First(c => !c.PlayerCard && c.Code == code));
                        }
                        else
                            highSeq.Add(cardsLowSeq.First(c => c.Code == code));
                    }
                    
                    break;
                }
        }

        if (highSeq.Count == fullHandQuantity && (highSeq.Where(c => c.PlayerCard).Count() == requiredHandCards || requiredHandCards == 0))
        {
            highSeq.ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

            hand.FullHand = hand.FullHand.OrderBy(x => x.Code).ToList();

            if (highSeq[0].Value.Equals("A"))
            {
                hand.Rank = 1;
                hand.Name = "Royal Flush";
            }
            else
            {
                hand.Rank = 2;
                hand.Name = "Straight Flush";
            }
        }
        #endregion

        else
        {
            #region 3.Four of a kind or 4.Full House or 5.Flush
            var occurringCards = cards.GroupBy(x => x.Rank).Select(group => new {Rank = group.Key, Quantity = group.Count(), PlayerCardQty = group.Where(c => c.PlayerCard).Count()}).OrderByDescending(x => x.Quantity).ThenBy(x => x.Rank).ToList();
            var highOccurrence = occurringCards[0];
            var secondOccurrence = occurringCards[1];
            var highSuitOccurrence = cards.GroupBy(x => x.Suit).Select(group => new {Suit = group.Key, Quantity = group.Count(), PlayerCardQty = group.Where(c => c.PlayerCard).Count()}).OrderByDescending(x => x.Quantity).First();

            if (highOccurrence.Quantity == 4 && (highOccurrence.PlayerCardQty == (fullHandQuantity - 4) || highOccurrence.PlayerCardQty == requiredHandCards || requiredHandCards == 0))
            {
                hand.Rank = 3;
                hand.Name = "Four of a kind";

                cards.Where(x => x.Rank == highOccurrence.Rank).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                if ((requiredHandCards - highOccurrence.PlayerCardQty) > 0)
                {
                    cards.Where(x => (x.Rank != highOccurrence.Rank && x.PlayerCard)).Take(requiredHandCards - highOccurrence.PlayerCardQty).ToList().ForEach(c =>{hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                    if ((fullHandQuantity - requiredHandCards - highOccurrence.PlayerCardQty) > 0)
                        cards.Where(x => (x.Rank != highOccurrence.Rank && !x.PlayerCard)).Take(fullHandQuantity - requiredHandCards - highOccurrence.PlayerCardQty).ToList().ForEach(c =>{hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                }
                else if (requiredHandCards == 0)
                {
                    var card = cards.OrderBy(x => x.Rank).First(x => (x.Rank != highOccurrence.Rank));
                    hand.FullHand.Add(new CashTableCard(card.Value, card.Suit));
                }
                else
                {
                    var card = cards.OrderBy(x => x.Rank).First(x => (x.Rank != highOccurrence.Rank && !x.PlayerCard));
                    hand.FullHand.Add(new CashTableCard(card.Value, card.Suit));
                }
            }
            else if (highOccurrence.Quantity == 3 && secondOccurrence.Quantity >= 2 && ((highOccurrence.PlayerCardQty + secondOccurrence.PlayerCardQty) == 2 || requiredHandCards == 0))
            {
                hand.Rank = 4;
                hand.Name = "Full House";

                cards.Where(x => x.Rank == highOccurrence.Rank).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                cards.Where(x => x.Rank == secondOccurrence.Rank).Take(fullHandQuantity - highOccurrence.Quantity).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
            }
            else if ((highSuitOccurrence.Quantity == fullHandQuantity && (highSuitOccurrence.PlayerCardQty == requiredHandCards || requiredHandCards == 0)) ||
            (highSuitOccurrence.Quantity > fullHandQuantity && (highSuitOccurrence.PlayerCardQty > requiredHandCards || requiredHandCards == 0)))
            {
                hand.Rank = 5;
                hand.Name = "Flush";

                if (requiredHandCards == 0)
                    cards.Where(x => (x.Suit == highSuitOccurrence.Suit)).Take(fullHandQuantity).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                else
                {
                    cards.Where(x => x.Suit == highSuitOccurrence.Suit && !x.PlayerCard).OrderBy(x => x.Rank).Take(fullHandQuantity - requiredHandCards).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    cards.Where(x => x.Suit == highSuitOccurrence.Suit && x.PlayerCard).OrderBy(x => x.Rank).Take(requiredHandCards).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                }
            }
            #endregion

            else
            {
                #region 6.Straight or 7.Three of a kind or 8.Two Pair or 9.Pair or 10.High Card
                cards = cards.OrderBy(x => x.Rank).ToList();
                acesSteelWheel = new List<CashTableCardType>();
                cardsLowSeq = new List<CashTableCardType>();
                cards.ForEach(c =>
                {
                    if (c.Rank == 1)
                        acesSteelWheel.Add(new CashTableCardType(c.Value, c.Suit, c.PlayerCard));

                    cardsLowSeq.Add(c);
                });
                acesSteelWheel.ForEach(a => {a.Rank = a.Rank + 13;});
                cardsLowSeq.AddRange(acesSteelWheel);
                
                highSeq = new List<CashTableCardType>();
                for (int i = 0; i <= (cardsLowSeq.Count - fullHandQuantity); i++)
                {
                    var ranks = cardsLowSeq.Skip(i).Select(c => c.Rank).Distinct().Take(fullHandQuantity).ToList();
                    if (ranks.Count > 0)
                        if (Enumerable.Range(ranks[0], fullHandQuantity).Except(ranks).Count() == 0)
                        {
                            foreach (var rank in ranks)
                            {
                                if (highSeq.Where(c => c.PlayerCard).ToList().Count >= requiredHandCards && requiredHandCards > 0)
                                {
                                    if (cardsLowSeq.Where(c => !c.PlayerCard && c.Rank == rank).ToList().Count > 0)
                                        highSeq.Add(cardsLowSeq.First(c => !c.PlayerCard && c.Rank == rank));
                                }
                                else
                                    highSeq.Add(cardsLowSeq.First(c => c.Rank == rank));
                            }
                            
                            break;
                        }
                }

                if (highSeq.Count == fullHandQuantity && (highSeq.Where(c => c.PlayerCard).Count() == requiredHandCards || requiredHandCards == 0))
                {
                    hand.Rank = 6;
                    hand.Name = "Straight";

                    highSeq.OrderBy(x => x.Rank).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                }
                else if (highOccurrence.Quantity == 3 && (highOccurrence.PlayerCardQty <= requiredHandCards || requiredHandCards == 0))
                {
                    hand.Rank = 7;
                    hand.Name = "Three of a kind";

                    cards.Where(x => x.Rank == highOccurrence.Rank).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                    if ((requiredHandCards - highOccurrence.PlayerCardQty) > 0)
                    {
                        cards.Where(x => (x.Rank != highOccurrence.Rank && x.PlayerCard)).Take(requiredHandCards - highOccurrence.PlayerCardQty).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                        if ((fullHandQuantity - 3 - highOccurrence.PlayerCardQty) > 0)
                            cards.Where(x => (x.Rank != highOccurrence.Rank && !x.PlayerCard)).Take(fullHandQuantity - 3 - highOccurrence.PlayerCardQty).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                    }
                    else if (requiredHandCards == 0)
                        cards.Where(x => (x.Rank != highOccurrence.Rank)).Take(fullHandQuantity - 3).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    else
                        cards.Where(x => (x.Rank != highOccurrence.Rank && !x.PlayerCard)).Take(fullHandQuantity - 3).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                }
                else if (highOccurrence.Quantity == 2 && secondOccurrence.Quantity == 2 && ((highOccurrence.PlayerCardQty + secondOccurrence.PlayerCardQty) <= requiredHandCards || requiredHandCards == 0))
                {
                    hand.Rank = 8;
                    hand.Name = "Two Pair";

                    cards.Where(x => x.Rank == highOccurrence.Rank).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    cards.Where(x => x.Rank == secondOccurrence.Rank).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                    if (requiredHandCards == 0)
                    {
                        cards.OrderBy(x => x.Rank).Where(x => (x.Rank != highOccurrence.Rank && x.Rank != secondOccurrence.Rank)).Take(fullHandQuantity - (highOccurrence.Quantity + secondOccurrence.Quantity))
                        .ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    }
                    else if (requiredHandCards - (highOccurrence.PlayerCardQty + secondOccurrence.PlayerCardQty) > 0)
                    {
                        cards.OrderBy(x => x.Rank).Where(x => x.Rank != highOccurrence.Rank && x.Rank != secondOccurrence.Rank && x.PlayerCard)
                        .Take(requiredHandCards - (highOccurrence.PlayerCardQty + secondOccurrence.PlayerCardQty)).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                        if (fullHandQuantity - 4 - (highOccurrence.PlayerCardQty + secondOccurrence.PlayerCardQty) > 0)
                            cards.Where(x => (x.Rank != highOccurrence.Rank && !x.PlayerCard)).Take(fullHandQuantity - 4 - (highOccurrence.PlayerCardQty + secondOccurrence.PlayerCardQty))
                            .ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    }
                    else
                    {
                        cards.OrderBy(x => x.Rank).Where(x => (x.Rank != highOccurrence.Rank && x.Rank != secondOccurrence.Rank && !x.PlayerCard)).Take(fullHandQuantity - (highOccurrence.Quantity + secondOccurrence.Quantity))
                        .ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    }
                }
                else if (highOccurrence.Quantity == 2 && (highOccurrence.PlayerCardQty <= requiredHandCards || requiredHandCards == 0))
                {
                    hand.Rank = 9;
                    hand.Name = "Pair";

                    cards.Where(x => x.Rank == highOccurrence.Rank).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                    if (requiredHandCards == 0)
                        cards.Where(x => x.Rank != highOccurrence.Rank).OrderBy(x => x.Rank).Take(fullHandQuantity - 2).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    else
                    {
                        if ((requiredHandCards - highOccurrence.PlayerCardQty) > 0)
                            cards.Where(x => x.Rank != highOccurrence.Rank && x.PlayerCard).OrderBy(x => x.Rank).Take(requiredHandCards - highOccurrence.PlayerCardQty).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});

                        cards.Where(x => x.Rank != highOccurrence.Rank && !x.PlayerCard).OrderBy(x => x.Rank).Take(fullHandQuantity - 2 - (requiredHandCards - highOccurrence.PlayerCardQty)).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    }
                }
                else
                {
                    hand.Rank = 10;
                    hand.Name = "High Card";

                    if (requiredHandCards == 0)
                        cards.OrderBy(x => x.Rank).Take(fullHandQuantity).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    else
                    {
                        cards.Where(x => !x.PlayerCard).OrderBy(x => x.Rank).Take(fullHandQuantity - requiredHandCards).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                        cards.Where(x => x.PlayerCard).OrderBy(x => x.Rank).Take(requiredHandCards).ToList().ForEach(c => {hand.FullHand.Add(new CashTableCard(c.Value, c.Suit));});
                    }
                }
                #endregion
            }
        }

        return Task.FromResult(hand);
    }
}