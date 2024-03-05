using System;

using NUnit.Framework;

using TestApp.Todo;

namespace TestApp.Tests;

[TestFixture]
public class ToDoListTests
{
    private ToDoList _toDoList = null!;
    
    [SetUp]
    public void SetUp()
    {
        this._toDoList = new();
    }
    
    [Test]
    public void Test_AddTask_TaskAddedToToDoList()
    {
        // Arrange
        string inputTitle = "Zaglavie";
        DateTime inputDueDate = DateTime.Today;
        this._toDoList.AddTask(inputTitle, inputDueDate); // създаваме обект, който използва метода.

        //Act
        var result = this._toDoList.DisplayTasks(); // изпълняваме този метод за да видим какво има след добавянето за данните.


        //Assert
        Assert.That(result, Does.Contain($"To-Do List:{Environment.NewLine}[ ] {inputTitle} - Due"));
    }

    [Test]
    public void Test_CompleteTask_TaskMarkedAsCompleted()
    {
        // Arrange
        string inputTitle = "Task to complete";
        DateTime inputDueDate = DateTime.Today;
        this._toDoList.AddTask(inputTitle, inputDueDate);
        this._toDoList.CompleteTask(inputTitle);

        //Act
        var result = this._toDoList.DisplayTasks();


        //Assert
        Assert.That(result, Does.Contain($"To-Do List:{Environment.NewLine}[✓] Task to complete - Due:"));
    }

    [Test]
    public void Test_CompleteTask_TaskNotFound_ThrowsArgumentException()
    {
        // Arrange
        string inputTitle = "Task to complete";
        //Act & Assert 
        Assert.That(() => this._toDoList.CompleteTask(inputTitle), Throws.ArgumentException);
    }

    [Test]
    public void Test_DisplayTasks_NoTasks_ReturnsEmptyString()
    {
        //Arrange

        //Act
        string result = this._toDoList.DisplayTasks();
        //Assert
        Assert.That(result, Is.EqualTo("To-Do List:"));
    }

    [Test]
    public void Test_DisplayTasks_WithTasks_ReturnsFormattedToDoList()
    {
        // Arrange
        string inputTitle = "Zaglavie";
        DateTime inputDueDate = DateTime.Today;
        this._toDoList.AddTask(inputTitle, inputDueDate); 
        
        string inputTitle1 = "Task to complete";
        DateTime inputDueDate1 = DateTime.Today;
        this._toDoList.AddTask(inputTitle1, inputDueDate1);
        this._toDoList.CompleteTask(inputTitle1);
        //Act
        var result = this._toDoList.DisplayTasks(); 


        //Assert
        Assert.That(result, Does.Contain($"To-Do List:{Environment.NewLine}[ ] {inputTitle} - Due:"));
        Assert.That(result, Does.Contain($"{Environment.NewLine}[✓] {inputTitle1} - Due:"));
    }
}
