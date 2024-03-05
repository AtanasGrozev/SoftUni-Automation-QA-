using NUnit.Framework;
using System;

namespace TestApp.Tests;

[TestFixture]
public class BankAccountTests
{
    [Test]
    public void Test_Constructor_InitialBalanceIsSet()
    {
        // Arrange
        double initialBalance = 10.00;
        // Act
        var account = new BankAccount(initialBalance);
        // Assert
        Assert.That(account.Balance, Is.EqualTo(initialBalance));
    }

    [Test]
    public void Test_Deposit_PositiveAmount_IncreasesBalance()
    {
        // Arrange
        double initialBalance = 100.00;
        double depositAmount = 50.00;
        double expectedBalance = initialBalance + depositAmount;

        // Act
        var account = new BankAccount(initialBalance);
        account.Deposit(depositAmount);

        // Assert
        Assert.That(account.Balance, Is.EqualTo(expectedBalance));
    }

    [Test]
    public void Test_Deposit_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        double initialBalance = 100.00;
        double depositAmount = -50.00;

        // Act
        var account = new BankAccount(initialBalance);

        // Assert
        
        Assert.That( () => account.Deposit(depositAmount), Throws.ArgumentException);
    }

    [Test]
    public void Test_Withdraw_ValidAmount_DecreasesBalance()
    {
        // Arrange
        double initialBalance = 100.00;
        double withdrawAmount = 50.00;
        double expectedBalance = initialBalance - withdrawAmount;

        // Act
        var account = new BankAccount(initialBalance);
        account.Withdraw(withdrawAmount);

        // Assert
        Assert.That(account.Balance, Is.EqualTo(expectedBalance));
    }

    [Test]
    public void Test_Withdraw_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        double initialBalance = 100.00;
        double withdrawAmount = -50.00;

        // Act
        var account = new BankAccount(initialBalance);

        // Assert

        Assert.That(() => account.Withdraw(withdrawAmount), Throws.ArgumentException);
    }

    [Test]
    public void Test_Withdraw_AmountGreaterThanBalance_ThrowsArgumentException()
    {
        // Arrange
        double initialBalance = 100.00;
        double withdrawAmount = -150.00;

        // Act
        var account = new BankAccount(initialBalance);

        // Assert

        Assert.That(() => account.Withdraw(withdrawAmount), Throws.ArgumentException);
    }
}
