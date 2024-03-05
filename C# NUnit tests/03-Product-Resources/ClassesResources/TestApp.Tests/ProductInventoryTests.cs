using NUnit.Framework;
using System;
using TestApp.Product;

namespace TestApp.Tests;

[TestFixture]
public class ProductInventoryTests
{
    private ProductInventory _inventory = null!;
    
    [SetUp]
    public void SetUp()
    {
        this._inventory = new();
    }
    
    [Test]
    public void Test_AddProduct_ProductAddedToInventory()
    {
        //Arrange
        string productName = "Banana";
        double prodcutPrice = 100;
        int productQuantity = 10;

        string expected = $"Product Inventory:{Environment.NewLine}{productName} - Price: ${prodcutPrice:f2} - Quantity: {productQuantity}";


        //Act
        this._inventory.AddProduct(productName, prodcutPrice, productQuantity);
        string result = this._inventory.DisplayInventory();
        //Assert
        Assert.That(result, Is.EqualTo(expected));

    }

    [Test]
    public void Test_DisplayInventory_NoProducts_ReturnsEmptyString()
    {
        //Arrange
        string productName = "";
        double prodcutPrice = 0;
        int productQuantity = 0;

        string expected = "";


        //Act
        this._inventory.AddProduct(productName, prodcutPrice, productQuantity);
        string result = "";
        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_DisplayInventory_WithProducts_ReturnsFormattedInventory()
    {
        //Arrange
        string productName = "Banana";
        double prodcutPrice = 100;
        int productQuantity = 10;

        string productName2 = "Kiwi";
        double prodcutPrice2 = 50;
        int productQuantity2 = 5;

        string expected = $"Product Inventory:{Environment.NewLine}{productName} - Price: ${prodcutPrice:f2} - Quantity: {productQuantity}" +
            $"{Environment.NewLine}{productName2} - Price: ${prodcutPrice2:f2} - Quantity: {productQuantity2}";


        //Act
        this._inventory.AddProduct(productName, prodcutPrice, productQuantity);
        this._inventory.AddProduct(productName2, prodcutPrice2, productQuantity2);
        string result = this._inventory.DisplayInventory();
        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_CalculateTotalValue_NoProducts_ReturnsZero()
    {
        double result = this._inventory.CalculateTotalValue();
        Assert.That(result, Is.Zero);
    }

    [Test]
    public void Test_CalculateTotalValue_WithProducts_ReturnsTotalValue()
    {
        //Arrange
        string productName = "Banana";
        double prodcutPrice = 100;
        int productQuantity = 10;      


        //Act
        this._inventory.AddProduct(productName, prodcutPrice, productQuantity);

        double result = this._inventory.CalculateTotalValue();
        
        //Assert
        Assert.That(result, Is.EqualTo(1000));
    }
}
