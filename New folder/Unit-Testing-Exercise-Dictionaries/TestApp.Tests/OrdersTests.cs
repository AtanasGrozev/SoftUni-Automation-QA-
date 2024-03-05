using NUnit.Framework;
using System;

namespace TestApp.Tests;

public class OrdersTests
{
    [Test]
    public void Test_Order_WithEmptyInput_ShouldReturnEmptyString()
    {
        string[] input = new string[0];
        string result = Orders.Order(input);
        Assert.That(result, Is.Empty);
    }


    [Test]
    public void TestOrderWithMultipleOrdersShouldReturnTotalPrice()
    {
        // Arrange
     string[] input = new string[]
{
    "apple 1.99 3.00",
    "banana 0.75 5.00",
    "orange 0.99 2.00"
};

        // Act
        string result = Orders.Order(input);

        // Assert
        Assert.That(result, Is.EqualTo($"apple -> 5.97{Environment.NewLine}banana -> 3.75{Environment.NewLine}orange -> 1.98"));
    }

    [Test]
    public void Test_Order_WithRoundedPrices_ShouldReturnTotalPrice()
    {
        // Arrange
        string[] input = new string[]
        {
            "apple 2.00 4.5",
            "banana 4.00 2.5",
            "orange 1.00 3.5"
        };

        // Act
        string result = Orders.Order(input);

        // Assert
        Assert.That(result, Is.EqualTo($"apple -> 9.00{Environment.NewLine}banana -> 10.00{Environment.NewLine}orange -> 3.50"));
    }

    [Test]
    public void Test_Order_WithDecimalQuantities_ShouldReturnTotalPrice()
    {
        // Arrange
        string[] input = new string[]
        {
            "apple 2.00 4.5",
            "banana 4.00 2.5",
            "orange 1.00 3.5"
        };

        // Act
        string result = Orders.Order(input);

        // Assert
        Assert.That(result, Is.EqualTo($"apple -> 9.00{Environment.NewLine}banana -> 10.00{Environment.NewLine}orange -> 3.50"));
    }
}
