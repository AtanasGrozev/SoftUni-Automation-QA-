using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace TestApp.Tests;

[TestFixture]
public class GradesTests
{
    [Test]
    public void Test_GetBestStudents_ReturnsBestThreeStudents()
    {
        //Arrange
        Dictionary<string, int> studentGrades = new Dictionary<string, int>
        {
            {"Alice", 90},
            {"Bob", 85},
            {"Charlie", 92},
            {"David", 88},
            {"Eva", 95}
        };
        //Act
        string result = Grades.GetBestStudents(studentGrades);
        //Assert
        Assert.That(result, Is.EqualTo($"" +
            $"Eva with average grade 95.00{Environment.NewLine}" +
            $"Charlie with average grade 92.00{Environment.NewLine}" +
            $"Alice with average grade 90.00"));
        
    }

    [Test]
    public void Test_GetBestStudents_EmptyGrades_ReturnsEmptyString()
    {
        Dictionary<string, int> studentGrades = new();
       
        //Act
        string result = Grades.GetBestStudents(studentGrades);

        //Assert
        Assert.That(result, Is.EqualTo(string.Empty));  

    }

    [Test]
    public void Test_GetBestStudents_LessThanThreeStudents_ReturnsAllStudents()
    {
        //Arrange
        Dictionary<string, int> studentGrades = new Dictionary<string, int>
        {
            {"Alice", 90},
            {"Bob", 85},
           
        };
        //Act
        string result = Grades.GetBestStudents(studentGrades);
        //Assert
        Assert.That(result, Is.EqualTo($"" +
            $"Alice with average grade 90.00{Environment.NewLine}" +
            $"Bob with average grade 85.00"));

    }

    [Test]
    public void Test_GetBestStudents_SameGrade_ReturnsInAlphabeticalOrder()
    {
        //Arrange
        Dictionary<string, int> studentGrades = new Dictionary<string, int>
        {
            {"Alice", 90},
            {"Bob", 90},
            {"Eva",90 },
            {"Charlie", 90}

        };
        //Act
        string result = Grades.GetBestStudents(studentGrades);
        //Assert
        Assert.That(result, Is.EqualTo($"Alice with average grade 90.00{Environment.NewLine}" +
           $"Bob with average grade 90.00{Environment.NewLine}" +
           $"Charlie with average grade 90.00"));
           
        



    }
}
