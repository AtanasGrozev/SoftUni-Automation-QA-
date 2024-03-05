using NUnit.Framework;

namespace TestApp.UnitTests;

public class SubstringTests
{
    // TODO: finish the test
    [Test]
    public void Test_RemoveOccurrences_RemovesSubstringFromMiddle()
    {
        // Arrange
        string toRemove = "fox";
        string input = "The quick brown fox jumps over the lazy dog";

        // Act
        string result = Substring.RemoveOccurrences(toRemove, input);

        // Assert
        Assert.That(result, Is.EqualTo("The quick brown jumps over the lazy dog"));
    }

    [Test]
    public void Test_RemoveOccurrences_RemovesSubstringFromStart()
    {
        // Arrange
        string toRemove = "The";
        string input = "The quick brown fox jumps over the lazy dog";

        // Act
        string result = Substring.RemoveOccurrences(toRemove, input);

        // Assert
        Assert.That(result, Is.EqualTo("quick brown fox jumps over lazy dog"));
    }

    [Test]
    public void Test_RemoveOccurrences_RemovesSubstringFromEnd()
    {
        string toRemove = "dog";
        string input = "The quick brown fox jumps over the lazy dog";

        // Act
        string result = Substring.RemoveOccurrences(toRemove, input);

        // Assert
        Assert.That(result, Is.EqualTo("The quick brown fox jumps over the lazy"));
    }

    [Test]
    public void Test_RemoveOccurrences_RemovesAllOccurrences()
    {
        string toRemove = "The quick brown fox jumps over the lazy dog";
        string input = "The quick brown fox jumps over the lazy dog";

        // Act
        string result = Substring.RemoveOccurrences(toRemove, input);

        // Assert
        Assert.That(result, Is.EqualTo(""));
    }
}

