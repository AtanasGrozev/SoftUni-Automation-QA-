using NUnit.Framework;

namespace TestApp.Tests;

[TestFixture]
public class StringRotatorTests
{
    [Test]
    public void Test_RotateRight_EmptyString_ReturnsEmptyString()
    {
        //Arrange
        string input = "";
        int position = 2;


        //Assert & Act
        string result = StringRotator.RotateRight(input, position);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Test_RotateRight_RotateByZeroPositions_ReturnsOriginalString()
    {
        //Arrange
        string input = "Hello";
        int position = 0;


        //Assert & Act
        string result = StringRotator.RotateRight(input, position);

        Assert.That(result, Is.EqualTo("Hello"));
    }

    [Test]
    public void Test_RotateRight_RotateByPositivePositions_ReturnsRotatedString()
    {
        //Arrange
        string input = "Hello";
        int position = 1;


        //Assert & Act
        string result = StringRotator.RotateRight(input, position);

        Assert.That(result, Is.EqualTo("oHell"));
    }

    [Test]
    public void Test_RotateRight_RotateByNegativePositions_ReturnsRotatedString()
    {

        //Arrange
        string input = "Hello";
        int position = -1;


        //Assert & Act
        string result = StringRotator.RotateRight(input, position);

        Assert.That(result, Is.EqualTo("oHell"));
    }

    [Test]
    public void Test_RotateRight_RotateByMorePositionsThanStringLength_ReturnsRotatedString()
    {
        //Arrange
        string input = "Hello";
        int position = 7;


        //Assert & Act
        string result = StringRotator.RotateRight(input, position);

        Assert.That(result, Is.EqualTo("loHel"));
    }
}
