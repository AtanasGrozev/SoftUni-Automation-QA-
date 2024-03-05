using NUnit.Framework;

namespace TestApp.Tests;

[TestFixture]
public class SubstringExtractorTests
{
    [Test]
    public void Test_ExtractSubstringBetweenMarkers_SubstringFound_ReturnsExtractedSubstring()
    {
        //Arrange
        string input = "Hello I am learing C# now!";
        string startString = "Hello";
        string endString = "now!";

       //Act
       string result = SubstringExtractor.ExtractSubstringBetweenMarkers(input, startString, endString);

       //Assert
       Assert.That(result, Is.EqualTo(" I am learing C# "));
    }

    [Test]
    public void Test_ExtractSubstringBetweenMarkers_StartMarkerNotFound_ReturnsNotFoundMessage()
    {
        //Arrange
        string input = "Hello I am learing C# now!";
       
        string startString = "dear";
        string endString = "now!";

       //Act
       string result = SubstringExtractor.ExtractSubstringBetweenMarkers(input, startString, endString);

       //Assert
       Assert.That(result, Is.EqualTo("Substring not found"));
    }

    [Test]
    public void Test_ExtractSubstringBetweenMarkers_EndMarkerNotFound_ReturnsNotFoundMessage()
    {
        //Arrange
       string input = "Hello I am learing C# now!";
        
        string startString = "Hello";
        string endString = "dear";


        //Act
        string result = SubstringExtractor.ExtractSubstringBetweenMarkers(input, startString, endString );

        //Assert
        Assert.That(result, Is.EqualTo("Substring not found"));
    }

    [Test]
    public void Test_ExtractSubstringBetweenMarkers_StartAndEndMarkersNotFound_ReturnsNotFoundMessage()
    {
        //Arrange
        string input = "Hello I am learing C# now!";

        string startString = "dear";
        string endString = "dear";

        //Act
        string result = SubstringExtractor.ExtractSubstringBetweenMarkers(input, startString, endString);

        //Assert
        Assert.That(result, Is.EqualTo("Substring not found"));
    }

    [Test]
    public void Test_ExtractSubstringBetweenMarkers_EmptyInput_ReturnsNotFoundMessage()
    {
        //Arrange
        string input = string.Empty;

        string startString = "Hello";
        string endString = "now!";

        //Act
        string result = SubstringExtractor.ExtractSubstringBetweenMarkers(input, startString, endString);

        //Assert
        Assert.That(result, Is.EqualTo("Substring not found"));
    }

    [Test]
    public void Test_ExtractSubstringBetweenMarkers_StartAndEndMarkersOverlapping_ReturnsNotFoundMessage()
    {
        //Arrange
        string input = "Hello I am learing C# now!";
        string startString = "Hello I am learing";
        string endString = "learing C# now!";

        //Act
        string result = SubstringExtractor.ExtractSubstringBetweenMarkers(input, startString, endString);

        //Assert
        Assert.That(result, Is.EqualTo("Substring not found"));
    }
}
