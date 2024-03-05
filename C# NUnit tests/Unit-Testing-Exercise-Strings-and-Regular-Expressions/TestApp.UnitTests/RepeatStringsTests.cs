using NUnit.Framework;

using System;

namespace TestApp.UnitTests;

public class RepeatStringsTests
{
    [Test]
    public void Test_Repeat_EmptyInput_ReturnsEmptyString()
    {
        // Arrange
        string[] input = Array.Empty<string>();

        // Act
        string result = RepeatStrings.Repeat(input);

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Test_Repeat_SingleInputString_ReturnsRepeatedString()
    {
        //Arrange 
        string[] input = new string[] { "test" };

        //Act
        string result = RepeatStrings.Repeat(input);

        //Assert
        Assert.That(result, Is.EqualTo("testtesttesttest"));
    }

    [Test]
    public void Test_Repeat_MultipleInputStrings_ReturnsConcatenatedRepeatedStrings()
    {
        //Arrange 
        string[] input = new string[] { "test" , "otnovo" , "test" };

        //Act
        string result = RepeatStrings.Repeat(input);

        //Assert
        Assert.That(result, Is.EqualTo("testtesttesttestotnovootnovootnovootnovootnovootnovotesttesttesttest"));
    }
}
