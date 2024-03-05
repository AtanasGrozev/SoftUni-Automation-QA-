using NUnit.Framework;

using System;

namespace TestApp.Tests;

public class CountRealNumbersTests
{
    // TODO: finish test
    [Test]
    public void Test_Count_WithEmptyArray_ShouldReturnEmptyString()
    {
        // Arrange
        int[] input = new int[0];

        // Act
        string result = CountRealNumbers.Count(input);

        // Assert
        Assert.That(result,Is.EqualTo(""));
    }

    [Test]
    public void Test_Count_WithSingleNumber_ShouldReturnCountString()
    {
        //Arrange 
        int[] input = { 1 };
        //Act
        string result = CountRealNumbers.Count(input);  
        //Assert
        Assert.That (result, Is.EqualTo("1 -> 1"));
    }

    [Test]
    public void Test_Count_WithMultipleNumbers_ShouldReturnCountString()
    {
        //Arrange 
        int[] input = { 1, 2 , 3, 2 ,5, 1};
        //Act
        string result = CountRealNumbers.Count(input);
        //Assert
        Assert.That(result, Is.EqualTo($"1 -> 2{Environment.NewLine}2 -> 2{Environment.NewLine}3 -> 1{Environment.NewLine}5 -> 1"));
    }

    [Test]
    public void Test_Count_WithNegativeNumbers_ShouldReturnCountString()
    {
        //Arrange 
        int[] input = { -1, -2, -3, -2, -5, -1, -10 };
        //Act
        string result = CountRealNumbers.Count(input);
        //Assert
        Assert.That(result, Is.EqualTo($"-10 -> 1{Environment.NewLine}-5 -> 1{Environment.NewLine}-3 -> 1{Environment.NewLine}-2 -> 2{Environment.NewLine}-1 -> 2"));
    }

    [Test]
    public void Test_Count_WithZero_ShouldReturnCountString()
    {
        //Arrange 
        int[] input = { 0,0,0,0,0};
        //Act
        string result = CountRealNumbers.Count(input);
        //Assert
        Assert.That(result, Is.EqualTo("0 -> 5"));
    }
}
