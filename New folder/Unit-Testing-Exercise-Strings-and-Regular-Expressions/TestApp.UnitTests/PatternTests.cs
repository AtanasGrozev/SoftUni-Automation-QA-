using NUnit.Framework;

namespace TestApp.UnitTests;

public class PatternTests
{
    // TODO: finish the test cases
    [TestCase("prowerka", 3, "pRoWeRkApRoWeRkApRoWeRkA")]
    [TestCase("prowerka", 2, "pRoWeRkApRoWeRkA")]
    [TestCase("prowerka", 1, "pRoWeRkA")]
    public void Test_GeneratePatternedString_ValidInput_ReturnsExpectedResult(string input, 
        int repetitionFactor, string expected)
    {
        // Arrange

        // Act
        string result = Pattern.GeneratePatternedString(input, repetitionFactor);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_GeneratePatternedString_EmptyInput_ThrowsArgumentException()
    {
        string input = "";
        int repetitionFactor = 0;
        Assert.That(() => Pattern.GeneratePatternedString(input,  repetitionFactor), Throws.ArgumentException);
    }

    [Test]
    public void Test_GeneratePatternedString_NegativeRepetitionFactor_ThrowsArgumentException()
    {
        string input = "prowerka";
        int repetitionFactor = -1;
        Assert.That(() => Pattern.GeneratePatternedString(input, repetitionFactor), Throws.ArgumentException);
    }

    [Test]
    public void Test_GeneratePatternedString_ZeroRepetitionFactor_ThrowsArgumentException()
    {
        string input = "prowerka";
        int repetitionFactor = 0;
        Assert.That(() => Pattern.GeneratePatternedString(input, repetitionFactor), Throws.ArgumentException);
    }
}
