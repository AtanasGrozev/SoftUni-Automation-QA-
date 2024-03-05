using NUnit.Framework;

namespace TestApp.Tests;

[TestFixture]
public class CamelCaseConverterTests
{
    [Test]
    public void Test_ConvertToCamelCase_EmptyString_ReturnsEmptyString()
    {
        //Arrange
        string input = "";
        //Act
        string result = CamelCaseConverter.ConvertToCamelCase(input);
        //Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Test_ConvertToCamelCase_SingleWord_ReturnsLowercaseWord()
    {
        //Arrange
        string input = "Hello";
        //Act
        string result = CamelCaseConverter.ConvertToCamelCase(input);
        //Assert
        Assert.That(result, Is.EqualTo("hello"));
    }

    [Test]
    public void Test_ConvertToCamelCase_MultipleWords_ReturnsCamelCase()
    {
        //Arrange
        string input = "dark magic";
        string input2 = "heavy weight";
        //Act
        string result = CamelCaseConverter.ConvertToCamelCase(input);
        string result2 = CamelCaseConverter.ConvertToCamelCase(input2);
        //Assert
        Assert.That(result, Is.EqualTo("darkMagic"));
        Assert.That(result2, Is.EqualTo("heavyWeight"));
    }

    [Test]
    public void Test_ConvertToCamelCase_MultipleWordsWithMixedCase_ReturnsCamelCase()
    {
        //Arrange
        string input = "Hello World";
        string input2 = "HI ALl";
        //Act
        string result = CamelCaseConverter.ConvertToCamelCase(input);
        string result2 = CamelCaseConverter.ConvertToCamelCase(input2);
        //Assert
        Assert.That(result, Is.EqualTo("helloWorld"));
        Assert.That(result2, Is.EqualTo("hiAll"));
    }
}
