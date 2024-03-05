using System.Collections.Generic;

using NUnit.Framework;

namespace TestApp.Tests;

[TestFixture]
public class NumberFrequencyTests
{
    [Test]
    public void Test_CountDigits_ZeroNumber_ReturnsEmptyDictionary()
    {
        //Arrange
        int input = 0;
        //Act       
        var result = NumberFrequency.CountDigits(input);
        //Assert 
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Test_CountDigits_SingleDigitNumber_ReturnsDictionaryWithSingleEntry()
    {
        //Arrange
        int input = 1;
        Dictionary<int, int> expected = new Dictionary<int, int>() { { 1, 1 } };
        //Act       
        var result = NumberFrequency.CountDigits(input);
        //Assert 
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_CountDigits_MultipleDigitNumber_ReturnsDictionaryWithDigitFrequencies()
    {
        //Arrange
        int input = 111223;
        Dictionary<int, int> expected = new Dictionary<int, int>() 
        { 
            { 1, 3 } ,
            { 2, 2 },
            {3, 1 }
        };
        //Act       
        var result = NumberFrequency.CountDigits(input);
        //Assert 
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_CountDigits_NegativeNumber_ReturnsDictionaryWithDigitFrequencies()
    {
        //Arrange
        int input = -111223;
        Dictionary<int, int> expected = new Dictionary<int, int>()
        {
            { 1, 3 } ,
            { 2, 2 },
            {3, 1 }
        };
        //Act       
        var result = NumberFrequency.CountDigits(input);
        //Assert 
        Assert.That(result, Is.EqualTo(expected));
    }
}
