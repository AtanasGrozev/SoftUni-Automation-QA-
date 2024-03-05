using NUnit.Framework;

using System;
using System.Collections.Generic;

namespace TestApp.UnitTests;

public class ExceptionTests
{
    private Exceptions _exceptions = null!;

    [SetUp]
    public void SetUp()
    {
        this._exceptions = new();
    }

    // TODO: finish test
    [Test]
    public void Test_Reverse_ValidString_ReturnsReversedString()
    {
        // Arrange
        string input = "abc";
        string expected = "cba";

        // Act
        string result = this._exceptions.ArgumentNullReverse(input);  

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    // TODO: finish test
    [Test]
    public void Test_Reverse_NullString_ThrowsArgumentNullException()
    {
        // Arrange
        string input = null;


        // Act & Assert
        Assert.That(() => this._exceptions.ArgumentNullReverse(input), Throws.ArgumentNullException);
    }

    [Test]
    public void Test_CalculateDiscount_ValidInput_ReturnsDiscountedPrice()
    {
        //Arrange
        decimal price = 1200;
        decimal discount = 10;
        decimal expected = 1080;
        //Act
        decimal result = this._exceptions.ArgumentCalculateDiscount(price, discount);

        //Assert
        Assert.That(result, Is.EqualTo(expected));

    }

    // TODO: finish test
    [Test]
    public void Test_CalculateDiscount_NegativeDiscount_ThrowsArgumentException()
    {
        // Arrange
        decimal totalPrice = 1200;
        decimal discount = -10;

        // Act & Assert
        Assert.That(() => this._exceptions.ArgumentCalculateDiscount(totalPrice, discount), Throws.ArgumentException);
    }

    // TODO: finish test
    [Test]
    public void Test_CalculateDiscount_DiscountOver100_ThrowsArgumentException()
    {
        // Arrange
        decimal totalPrice = 100.0m;
        decimal discount = 110.0m;

        // Act & Assert
        Assert.That(() => this._exceptions.ArgumentCalculateDiscount(totalPrice, discount), Throws.ArgumentException);
    }

    [Test]
    public void Test_GetElement_ValidIndex_ReturnsElement()
    {
        //Arrange
        int[] ints = { 1, 2, 3 };
        int element = 1;
        int expected = 2;

        //Act
        int result = this._exceptions.IndexOutOfRangeGetElement(ints, element);

        //Assert
        Assert.That(result, Is.EqualTo (expected));


    }

    // TODO: finish test
    [Test]
    public void Test_GetElement_IndexLessThanZero_ThrowsIndexOutOfRangeException()
    {
        // Arrange
        int[] array = { 1, 2, 3 };
        int index = -1;

        // Act & Assert
        Assert.That(() => this._exceptions.IndexOutOfRangeGetElement(array, index), Throws.InstanceOf<IndexOutOfRangeException>());
    }

    // TODO: finish test
    [Test]
    public void Test_GetElement_IndexEqualToArrayLength_ThrowsIndexOutOfRangeException()
    {
        // Arrange
        int[] array = { 10, 20, 30, 40, 50 };
        int index = array.Length;

        // Act & Assert
        Assert.That(() => this._exceptions.IndexOutOfRangeGetElement(array,index), Throws.InstanceOf<IndexOutOfRangeException>());
    }

    [Test]
    public void Test_GetElement_IndexGreaterThanArrayLength_ThrowsIndexOutOfRangeException()
    {
        // Arrange
        int[] array = { 10, 20, 30, 40, 50 };
        int index = array.Length +1 ;

        // Act & Assert
        Assert.That(() => this._exceptions.IndexOutOfRangeGetElement(array, index), Throws.InstanceOf<IndexOutOfRangeException>());
    }

    [Test]
    public void Test_PerformSecureOperation_UserLoggedIn_ReturnsUserLoggedInMessage()
    {
        //Arrange
        bool input = true;
        string expected = "User logged in.";
        //Act

        string result = this._exceptions.InvalidOperationPerformSecureOperation(input);

        //Assert
        Assert.That(result, Is.EqualTo(expected));  

    }

    [Test]
    public void Test_PerformSecureOperation_UserNotLoggedIn_ThrowsInvalidOperationException()
    {
        //Arrange
        bool input = false;
        //Act & Assert
        Assert.That(() => this._exceptions.InvalidOperationPerformSecureOperation(input), Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void Test_ParseInt_ValidInput_ReturnsParsedInteger()
    {
        //Arrange 
        string input = "5";

       //Act
       int result = this._exceptions.FormatExceptionParseInt(input);

        //Assert
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Test_ParseInt_InvalidInput_ThrowsFormatException()
    {
        //Arrange
        string input = "a";
        Assert.That(() => this._exceptions.FormatExceptionParseInt(input), Throws.TypeOf<FormatException>());
    }

    [Test]
    public void Test_FindValueByKey_KeyExistsInDictionary_ReturnsValue()
    {
        //Arrange
        Dictionary<string, int> input = new()
        {
            ["first"] = 10,
            ["second"] = 15,
            ["third"] = 20
        };
        string key = "second";
        //Act
        int result = this._exceptions.KeyNotFoundFindValueByKey(input, key);
        //Assert
        Assert.That(result, Is.EqualTo(15));

    }

    [Test]
    public void Test_FindValueByKey_KeyDoesNotExistInDictionary_ThrowsKeyNotFoundException()
    {
        //Arrange
        Dictionary<string, int> input = new()
        {
            ["first"] = 10,
            ["second"] = 15,
            ["third"] = 20
        };
        string key = "four";
        //Act & assert
        Assert.That(() => this._exceptions.KeyNotFoundFindValueByKey(input, key), Throws.TypeOf<KeyNotFoundException>());
    }

    [Test]
    public void Test_AddNumbers_NoOverflow_ReturnsSum()
    {
        int a = 2;
        int b = 3;

        int result = this._exceptions.OverflowAddNumbers(a, b);

        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Test_AddNumbers_PositiveOverflow_ThrowsOverflowException()
    {
        int a = int.MaxValue;
        int b = 1;

        Assert.That(() => this._exceptions.OverflowAddNumbers(a, b), Throws.TypeOf<OverflowException>());
    }

    [Test]
    public void Test_AddNumbers_NegativeOverflow_ThrowsOverflowException()
    {
        int a = int.MinValue;
        int b = -1;

        Assert.That(() => this._exceptions.OverflowAddNumbers(a, b), Throws.TypeOf<OverflowException>());
    }

    [Test]
    public void Test_DivideNumbers_ValidDivision_ReturnsQuotient()
    {
        //Assert
        int a = 6;
        int b = 3;
        int expected = 2;

        //Act
        int result = this._exceptions.DivideByZeroDivideNumbers(a, b);

        //Arrange
        Assert.That(result,Is.EqualTo(expected));
    }

    [Test]
    public void Test_DivideNumbers_DivideByZero_ThrowsDivideByZeroException()
    {
        //Assert
        int a = 6;
        int b = 0;
        //Act&Assert
        Assert.That(() => this._exceptions.DivideByZeroDivideNumbers(a, b), Throws.TypeOf<DivideByZeroException>());
    }

    [Test]
    public void Test_SumCollectionElements_ValidCollectionAndIndex_ReturnsSum()
    {
        //Arrange
        int[]? collection = { 1, 2, 3 };
        int index = 2;
        //Act
        int result = this._exceptions.SumCollectionElements(collection, index);
        //Assert
        Assert.That(result, Is.EqualTo(6));


    }

    [Test]
    public void Test_SumCollectionElements_NullCollection_ThrowsArgumentNullException()
    {
        //Arrange
        int[]? collection = null;
        int index = 1;

        //Act & Assert
        Assert.That(() => this._exceptions.SumCollectionElements(collection, index), Throws.TypeOf<ArgumentNullException>());

    }

    [Test]
    public void Test_SumCollectionElements_IndexOutOfRange_ThrowsIndexOutOfRangeException()
    {
        //Arrange
        int[]? collection = {1,2,3};
        int index = -1;
        //ACt and Assert
        Assert.That(() => this._exceptions.SumCollectionElements(collection, index), Throws.TypeOf<IndexOutOfRangeException>());
    }

    [Test]
    public void Test_GetElementAsNumber_ValidKey_ReturnsParsedNumber()
    {
        //Arrange
        Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {
            ["one"] = "1",
            ["two"] = "2",
            ["three"] = "3"

        };
        string key = "one";
        int expected = 1;
        //Act
        int result = this._exceptions.GetElementAsNumber(dictionary, key);
        //Assert
        Assert.That(result, Is.EqualTo(expected));  

    }

    [Test]
    public void Test_GetElementAsNumber_KeyNotFound_ThrowsKeyNotFoundException()
    {
        //Arrange
        Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {
            ["one"] = "1",
            ["two"] = "2",
            ["three"] = "3"

        };
        string key = "five";
        //Act & Assert
        Assert.That(() => this._exceptions.GetElementAsNumber(dictionary, key), Throws.InstanceOf<KeyNotFoundException>());
    }

    [Test]
    public void Test_GetElementAsNumber_InvalidFormat_ThrowsFormatException()
    {
        //Arrange
        Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {
            ["one"] = "one",
            ["two"] = "2",
            ["three"] = "3"

        };
        string key = "one"
;        //Act & Assert
        Assert.That(() => this._exceptions.GetElementAsNumber(dictionary, key), Throws.TypeOf<FormatException>());
    }
}
