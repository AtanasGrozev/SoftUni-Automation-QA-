using System;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

using TestApp.Chat;

namespace TestApp.Tests;

[TestFixture]
public class ChatRoomTests
{
    private ChatRoom _chatRoom = null!;
    
    [SetUp]
    public void Setup()
    {
        this._chatRoom = new();
    }
    
    [Test]
    public void Test_SendMessage_MessageSentToChatRoom()
    {
        //Arrange
        string sender = "Atanas";
        string message = "hello";
       
        
        _chatRoom.SendMessage(sender, message);

        //Act
        var result = _chatRoom.DisplayChat();

        //Assert
        Assert.That(result, Does.Contain($"Chat Room Messages:{Environment.NewLine}" +
            $"Atanas: hello - "));

    }

    [Test]
    public void Test_DisplayChat_NoMessages_ReturnsEmptyString()
    {
       

        //Act
        var result = _chatRoom.DisplayChat();

        //Assert
        Assert.That(result, Is.EqualTo(string.Empty));  
    }

    [Test]
    public void Test_DisplayChat_WithMessages_ReturnsFormattedChat()
    {
        //Arrange
        string sender = "Atanas";
        string message = "hello";
        string sender2 = "Mitko";
        string message2 = "what is up";

        _chatRoom.SendMessage(sender, message);
        _chatRoom.SendMessage(sender2, message2);

        //Act
        var result = _chatRoom.DisplayChat();

        //Assert
        Assert.That(result, Does.Contain($"Chat Room Messages:{Environment.NewLine}" +
            $"Atanas: hello - "));
        Assert.That(result, Does.Contain($"Mitko: what is up - "));
    }
}
