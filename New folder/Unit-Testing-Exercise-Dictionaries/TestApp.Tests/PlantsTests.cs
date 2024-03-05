using NUnit.Framework;

using System;

namespace TestApp.Tests;

public class PlantsTests
{
    [Test]
    public void Test_GetFastestGrowing_WithEmptyArray_ShouldReturnEmptyString()
    {
        string[] input = { };
        string result = Plants.GetFastestGrowing(input);
        Assert.That(result, Is.Empty);
    }

    // TODO: finish test
    [Test]
    public void Test_GetFastestGrowing_WithSinglePlant_ShouldReturnPlant()
    {
        // Arrange
        string[] plants = new string[] {"abcdf"};

        // Act
        string result = Plants.GetFastestGrowing(plants);

        // Assert
        Assert.That(result, Is.EqualTo($"Plants with 5 letters:{Environment.NewLine}abcdf"));

    }

    [Test]
    public void Test_GetFastestGrowing_WithMultiplePlants_ShouldReturnGroupedPlants()
    {
        // Arrange
        string[] plants = new string[] { "roza","hrizantema" };

        // Act
        string result = Plants.GetFastestGrowing(plants);

        // Assert
        Assert.That(result, Is.EqualTo($"Plants with 4 letters:{Environment.NewLine}roza{Environment.NewLine}Plants with 10 letters:{Environment.NewLine}hrizantema"));
    }

    [Test]
    public void Test_GetFastestGrowing_WithMixedCasePlants_ShouldBeCaseInsensitive()
    {
        // Arrange
        string[] plants = new string[] { "Roza", "Hrizantema" };

        // Act
        string result = Plants.GetFastestGrowing(plants);

        // Assert
        Assert.That(result, Is.EqualTo($"Plants with 4 letters:{Environment.NewLine}Roza{Environment.NewLine}Plants with 10 letters:{Environment.NewLine}Hrizantema"));
    }
}
