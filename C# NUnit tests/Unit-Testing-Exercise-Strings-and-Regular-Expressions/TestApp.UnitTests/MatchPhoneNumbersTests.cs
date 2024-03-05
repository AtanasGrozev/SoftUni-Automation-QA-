using NUnit.Framework;

namespace TestApp.UnitTests;

public class MatchPhoneNumbersTests
{
    // TODO: finish the test
    [Test]
    public void Test_Match_ValidPhoneNumbers_ReturnsMatchedNumbers()
    {
        // Arrange
        //@"\+359(?<seperators>[ -])2\k<seperators>[0-9]{3}\k<seperators>[0-9]{4}\b"
        string phoneNumbers = "+359-2-124-5678, +359 2 986 5432, +359-2-555-5555";
        string expected = "+359-2-124-5678, +359 2 986 5432, +359-2-555-5555";

        // Act
        string result = MatchPhoneNumbers.Match(phoneNumbers);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_Match_NoValidPhoneNumbers_ReturnsEmptyString()
    {
        // Arrange
        //@"\+359(?<seperators>[ -])2\k<seperators>[0-9]{3}\k<seperators>[0-9]{4}\b"
        string phoneNumbers = "+59-2-124-5678";
        string expected = "";


        // Act
        string result = MatchPhoneNumbers.Match(phoneNumbers);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_Match_EmptyInput_ReturnsEmptyString()
    {
        // Arrange
        //@"\+359(?<seperators>[ -])2\k<seperators>[0-9]{3}\k<seperators>[0-9]{4}\b"
        string phoneNumbers = "";
        string expected = "";


        // Act
        string result = MatchPhoneNumbers.Match(phoneNumbers);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_Match_MixedValidAndInvalidNumbers_ReturnsOnlyValidNumbers()
    {
        // Arrange
        //@"\+359(?<seperators>[ -])2\k<seperators>[0-9]{3}\k<seperators>[0-9]{4}\b"
        string phoneNumbers = "+59-2-124-5678, +359 986 5432, +359-2-555-5555, +359-2-555-55556, +359-2-555+5555, -359-2-555-5555, +359 2 555 5555,";
        string expected = "+359-2-555-5555, +359 2 555 5555";


        // Act
        string result = MatchPhoneNumbers.Match(phoneNumbers);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
