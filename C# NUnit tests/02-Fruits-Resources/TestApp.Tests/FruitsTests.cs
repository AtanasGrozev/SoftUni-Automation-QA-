using System.Collections.Generic;

using NUnit.Framework;

namespace TestApp.Tests;

[TestFixture]
public class FruitsTests
{
    [Test]
    public void Test_GetFruitQuantity_FruitExists_ReturnsQuantity()
    {
        //Arrange
        Dictionary<string, int> input = new Dictionary<string, int> { { "kiwi", 10 } };       


        //Act
       
        int result = Fruits.GetFruitQuantity(input, "kiwi");


        //Assert
        Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    public void Test_GetFruitQuantity_FruitDoesNotExist_ReturnsZero()
    {
        //Arrange
        Dictionary<string, int> input = new Dictionary<string, int> { { "kiwi", 10 } };
       


        //Act
        
        int result = Fruits.GetFruitQuantity(input, "Mango");


        //Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Test_GetFruitQuantity_EmptyDictionary_ReturnsZero()
    {
        
        // Arrange
        Dictionary<string, int> input = new Dictionary<string, int>();

        //Act

        int result = Fruits.GetFruitQuantity(input, "Mango");


        //Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Test_GetFruitQuantity_NullDictionary_ReturnsZero()
    {
        //Arrange

        Dictionary<string, int> input = null;


        //Act
      
        int result = Fruits.GetFruitQuantity(input, "Mango");


        //Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Test_GetFruitQuantity_NullFruitName_ReturnsZero()
    {
        //Arrange
        Dictionary<string, int> input = new Dictionary<string, int> { { "kiwi", 10 } };


        //Act

        int result = Fruits.GetFruitQuantity(input, null);


        //Assert
        Assert.That(result, Is.EqualTo(0));
    }
}
