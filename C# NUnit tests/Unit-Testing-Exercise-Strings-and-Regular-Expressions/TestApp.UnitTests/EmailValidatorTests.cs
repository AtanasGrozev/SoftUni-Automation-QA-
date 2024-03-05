using NUnit.Framework;

namespace TestApp.UnitTests;

public class EmailValidatorTests
{
    //@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    // TODO: finish the test
    [TestCase("atgrozev@gmail.co")]
    [TestCase("ck_pitbull@abv.bg")]
    //[TestCase()]
    public void Test_ValidEmails_ReturnsTrue(string email)
    {
        // Arrange

        // Act
        bool result = EmailValidator.IsValidEmail(email);

        // Assert
        Assert.That(result, Is.True);
    }

    // TODO: finish the test
    [TestCase("atrozevgmail.com")]
    [TestCase("@gmail.co")]
    [TestCase("atgrozevxgmail.co")]
    public void Test_InvalidEmails_ReturnsFalse(string email)
    {
        // Arrange

        // Act
        bool result = EmailValidator.IsValidEmail(email);
        
        // Assert
        Assert.That(result, Is.False);
    }
}
