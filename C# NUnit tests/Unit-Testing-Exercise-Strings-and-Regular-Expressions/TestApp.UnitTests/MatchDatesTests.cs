using NUnit.Framework;

namespace TestApp.UnitTests;

public class MatchDatesTests
{
    // TODO: finish the test
    [Test]
    public void Test_Match_ValidDate_ReturnsExpectedResult()
    {
        // Arrange
        //@"\b(?<day>\d{2})(?<seperator>[-.\/])(?<month>[A-Z][a-z]+)\k<seperator>(?<year>\d{4})"
        string input = "31-Dec-2022";
        string expected = "Day: 31, Month: Dec, Year: 2022";

      
    }

    // TODO: finish the test
    [Test]
    public void Test_Match_NoMatch_ReturnsEmptyString()
    {
        // Arrange
        //@"\b(?<day>\d{2})(?<seperator>[-.\/])(?<month>[A-Z][a-z]+)\k<seperator>(?<year>\d{4})"
        string input = "31+Dec+2022";
        string expected = string.Empty;

        // Act
        string result = MatchDates.Match(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_Match_MultipleMatches_ReturnsFirstMatch()
    {
        // Arrange
        //@"\b(?<day>\d{2})(?<seperator>[-.\/])(?<month>[A-Z][a-z]+)\k<seperator>(?<year>\d{4})"
        string input = "31+Dec+2022, 31/Dec-2022, 50-Dec-2022, 05-Dec-2022";
        string expected = "Day: 50, Month: Dec, Year: 2022"; 

        // Act
        string result = MatchDates.Match(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_Match_EmptyString_ReturnsEmptyString()
    {
        string input = string.Empty;
        string expected = string.Empty;

        // Act
        string result = MatchDates.Match(input);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_Match_NullInput_ThrowsArgumentException()
    {
        string input = null;
       // string expected = string.Empty;

       //Assert & Act

        Assert.That(() => MatchDates.Match(input), Throws.ArgumentException);   
    }
}
