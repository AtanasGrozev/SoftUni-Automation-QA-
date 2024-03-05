using NUnit.Framework;

using System;
using System.Xml.Linq;


namespace TestApp.UnitTests;

public class PlanetTests
{
   
    // TODO: finish test
    [Test]
    public void Test_CalculateGravity_ReturnsCorrectCalculation()
    {
        // Arrange
 
    Planet earth = new("Earth", 12742, 149600000, 1);
        double mass = 1000;
        double expectedGravity = mass * 6.67430e-11 / Math.Pow(earth.Diameter / 2, 2);

        // Act
       double result  =  earth.CalculateGravity(mass);

        // Assert
        Assert.That(result, Is.EqualTo(expectedGravity));
    }

    [Test]
    public void Test_GetPlanetInfo_ReturnsCorrectInfo()
    {
        // Arrange

        Planet earth = new("Earth", 12742, 149600000, 1);
        string planetInfo = $"Planet: Earth{Environment.NewLine}Diameter: 12742 km{Environment.NewLine}Distance from the Sun: 149600000 km{Environment.NewLine}Number of Moons: 1";

        // Act       
        string result = earth.GetPlanetInfo();
        //Assert
        Assert.That(result, Is.EqualTo(planetInfo));
    }
}
