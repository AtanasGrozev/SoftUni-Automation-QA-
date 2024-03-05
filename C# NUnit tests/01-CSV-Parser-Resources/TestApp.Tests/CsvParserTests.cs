using NUnit.Framework;

namespace TestApp.Tests;

[TestFixture]
public class CsvParserTests
{
    [Test]
    public void Test_ParseCsv_EmptyInput_ReturnsEmptyArray()
    {
        //Arrange
        string input = "";
        //Act
        string[] result  = CsvParser.ParseCsv(input);
        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Test_ParseCsv_SingleField_ReturnsArrayWithOneElement()
    {
        //Arrange
        string input = "hello";
        string[] excpeted = new[] { "hello" };
        //Act
        string[] result = CsvParser.ParseCsv(input);
        //Assert
        Assert.That(result, Is.EqualTo(excpeted));
    }

    [Test]
    public void Test_ParseCsv_MultipleFields_ReturnsArrayWithMultipleElements()
    {
        string input = "hello,world,again";
        string[] excpeted = new[] {"hello","world","again"};
        //Act
        string[] result = CsvParser.ParseCsv(input);
        //Assert
        Assert.That(result, Is.EqualTo(excpeted));
    }

    [Test]
    public void Test_ParseCsv_TrimsWhiteSpace_ReturnsCleanArray()
    {
        string input = "hello,    world,     again";
        string[] excpeted = new[] { "hello","world","again" };
        //Act
        string[] result = CsvParser.ParseCsv(input);
        //Assert
        Assert.That(result, Is.EqualTo(excpeted));
    }
}
