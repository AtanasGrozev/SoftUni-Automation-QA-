using NUnit.Framework;

using System;
using System.Collections.Generic;

namespace TestApp.Tests;

public class GroupingTests
{
    // TODO: finish test
    [Test]
    public void Test_GroupNumbers_WithEmptyList_ShouldReturnEmptyString()
    {
        // Arrange
        List<int> input = new List<int>();  

        // Act
        string result = Grouping.GroupNumbers(input);

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void Test_GroupNumbers_WithEvenAndOddNumbers_ShouldReturnGroupedString()
    {
        List<int> input = new() { 1, 2, 3 ,4  };

        string result = Grouping.GroupNumbers(input);
        Assert.That(result, Is.EqualTo($"Odd numbers: 1, 3{Environment.NewLine}Even numbers: 2, 4"));

    }

    [Test]
    public void Test_GroupNumbers_WithOnlyEvenNumbers_ShouldReturnGroupedString()
    {
        List<int> input = new() {  2,4,6,8 };

        string result = Grouping.GroupNumbers(input);
        Assert.That(result, Is.EqualTo("Even numbers: 2, 4, 6, 8"));

    }

    [Test]
    public void Test_GroupNumbers_WithOnlyOddNumbers_ShouldReturnGroupedString()
    {
        List<int> input = new() { 1, 3, 7, 9 };

        string result = Grouping.GroupNumbers(input);
        Assert.That(result, Is.EqualTo("Odd numbers: 1, 3, 7, 9"));

    }

    [Test]
    public void Test_GroupNumbers_WithNegativeNumbers_ShouldReturnGroupedString()
    {
        List<int> input = new() { -1, -2, -3, -4 };

        string result = Grouping.GroupNumbers(input);
        Assert.That(result, Is.EqualTo($"Odd numbers: -1, -3{Environment.NewLine}Even numbers: -2, -4"));
    }
}
