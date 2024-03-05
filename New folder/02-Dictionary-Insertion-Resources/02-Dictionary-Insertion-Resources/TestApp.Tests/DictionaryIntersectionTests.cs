using System.Collections.Generic;

using NUnit.Framework;

namespace TestApp.Tests;

[TestFixture]
public class DictionaryIntersectionTests
{
    [Test]
    public void Test_Intersect_TwoEmptyDictionaries_ReturnsEmptyDictionary()
    {
        //Arrange
        Dictionary<string, int > dict1 = new Dictionary<string, int>();
        Dictionary<string, int > dict2 = new Dictionary<string, int>();

        //Act
       Dictionary<string, int> result = DictionaryIntersection.Intersect(dict1, dict2);

        //Assert

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Test_Intersect_OneEmptyDictionaryAndOneNonEmptyDictionary_ReturnsEmptyDictionary()
    {
        //Arrange
        Dictionary<string, int> dict1 = new Dictionary<string, int>();
        Dictionary<string, int> dict2 = new Dictionary<string, int> { { "hello", 1 } };

        //Act
        Dictionary<string, int> result = DictionaryIntersection.Intersect(dict1, dict2);

        //Assert

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Test_Intersect_TwoNonEmptyDictionariesWithNoCommonKeys_ReturnsEmptyDictionary()
    {
        //Arrange
        Dictionary<string, int> dict1 = new() { { "test", 1 } };
        Dictionary<string, int> dict2 = new Dictionary<string, int> { { "hello", 1 } };

        //Act
        Dictionary<string, int> result = DictionaryIntersection.Intersect(dict1, dict2);

        //Assert

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Test_Intersect_TwoNonEmptyDictionariesWithCommonKeysAndValues_ReturnsIntersectionDictionary()
    {
        //Arrange
        Dictionary<string, int> dict1 = new() { { "hello", 1 } };
        Dictionary<string, int> dict2 = new Dictionary<string, int> { { "hello", 1 } };
        Dictionary<string, int> expected = new() { { "hello", 1 } };


        //Act
        Dictionary<string, int> result = DictionaryIntersection.Intersect(dict1, dict2);

        //Assert

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_Intersect_TwoNonEmptyDictionariesWithCommonKeysAndDifferentValues_ReturnsEmptyDictionary()
    {
        //Arrange
        Dictionary<string, int> dict1 = new() { { "hello", 1 } };
        Dictionary<string, int> dict2 = new Dictionary<string, int> { { "hello", 2 } };
        Dictionary<string, int> expected = new() { { "hello", 0} };


        //Act
        Dictionary<string, int> result = DictionaryIntersection.Intersect(dict1, dict2);

        //Assert

        Assert.That(result, Is.Empty);
    }
}
