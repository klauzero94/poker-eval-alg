using System.Collections.Generic;
using Data;
using Models;
using Services;
using Xunit;

namespace Tests;

public class GetActualHandTest
{
    [Theory]
    [ClassData(typeof(GetActualHandOmahaData))]
    public async void CheckOmahaConsistency(List<CashTableCard> ExposedCards, List<CashTableCard> PlayerCards, string expected, string fail)
    {
        //Arrange
        var gahService = new GetActualHandService();

        //Act
        var actual = await gahService.GetActualHand(ExposedCards, PlayerCards, 2, 5);

        //Assert
        Assert.Equal(expected, actual.Name);
        Assert.False(fail == actual.Name);
    }

    [Theory]
    [ClassData(typeof(GetActualHandTexasData))]
    public async void CheckTexasConsistency(List<CashTableCard> ExposedCards, List<CashTableCard> PlayerCards, string expected, string fail)
    {
        //Arrange
        var gahService = new GetActualHandService();

        //Act
        var actual = await gahService.GetActualHand(ExposedCards, PlayerCards, 0, 5);

        //Assert
        Assert.Equal(expected, actual.Name);
        Assert.False(fail == actual.Name);
    }

    [Theory]
    [ClassData(typeof(GetActualHandCustomData))]
    public async void CheckCustomConsistency(List<CashTableCard> ExposedCards, List<CashTableCard> PlayerCards, string expected, string fail)
    {
        //Arrange
        var gahService = new GetActualHandService();

        //Act
        var actual = await gahService.GetActualHand(ExposedCards, PlayerCards, 0, 5);

        //Assert
        Assert.Equal(expected, actual.Name);
        Assert.False(fail == actual.Name);
    }
}