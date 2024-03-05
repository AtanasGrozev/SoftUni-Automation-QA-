using NUnit.Framework;

using System;
using System.Linq;

namespace TestApp.UnitTests;

public class ReverseConcatenateTests
{
    // TODO: finish the test
    [Test]
    public void Test_ReverseAndConcatenateStrings_EmptyInput_ReturnsEmptyString()
    {
        // Arrange
        string[] input = new string[] { };

        // Act
       string result = ReverseConcatenate.ReverseAndConcatenateStrings(input);

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }

    // TODO: finish the test
    [Test]
    public void Test_ReverseAndConcatenateStrings_SingleString_ReturnsSameString()
    {
        // Arrange
        string[] input = new string[] { "prowerka" };

        // Act
        string result = ReverseConcatenate.ReverseAndConcatenateStrings(input);

        // Assert
        Assert.That(result, Is.EqualTo("prowerka"));
    }

    [Test]
    public void Test_ReverseAndConcatenateStrings_MultipleStrings_ReturnsReversedConcatenatedString()
    {
        //Arrange 
        string[] input = new string[] { "prowerka", "prowerka", "prowerka" };

        //act
        string result = ReverseConcatenate.ReverseAndConcatenateStrings(input);

        //Assert
        Assert.That(result, Is.EqualTo("prowerkaprowerkaprowerka"));
    }

    [Test]
    public void Test_ReverseAndConcatenateStrings_NullInput_ReturnsEmptyString()
    {
        //Arrange 
        string[] input = null;
        string result = ReverseConcatenate.ReverseAndConcatenateStrings(input);
        Assert.That(result, Is.Empty);

    }

    [Test]
    public void Test_ReverseAndConcatenateStrings_WhitespaceInput_ReturnsConcatenatedString()
    {
        //Arrange
        string[] strings = new string[] { " ", " " };
        //Act
        string result = ReverseConcatenate.ReverseAndConcatenateStrings(strings);
        //Assert
        Assert.That(result, Is.EqualTo("  "));
    }

    [Test]
    public void Test_ReverseAndConcatenateStrings_LargeInput_ReturnsReversedConcatenatedString()
    {
        string[] strings = new string[] { "String1", "String1", "String1", "String1", "String1", "String1", "String1", "String1", };
        string result = ReverseConcatenate.ReverseAndConcatenateStrings(strings);
        Assert.That(result,Is.EqualTo("String1String1String1String1String1String1String1String1"));
    }
}
