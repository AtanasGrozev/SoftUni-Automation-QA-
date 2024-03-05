using NUnit.Framework;

using System;

namespace TestApp.UnitTests;

public class SongTests
{
    private Song _song;

    [SetUp]
    public void Setup()
    {
        this._song = new();
    }

    // TODO: finish test
    [Test]
    public void Test_AddAndListSongs_ReturnsAllSongs_WhenWantedListIsAll()
    {
        // Arrange
        string[] songs = { "Pop_Song1_3:30", "Rock_Song2_4:15", "Pop_Song3_3:00" };
        string expected = $"Song1{Environment.NewLine}Song2{Environment.NewLine}Song3";

        // Act
        string result = this._song.AddAndListSongs(songs, "all");

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_AddAndListSongs_ReturnsFilteredSongs_WhenWantedListIsSpecific()
    {
        // Arrange
        string[] songs = { "Pop_Song1_3:30", "Rock_Song2_4:15" };
        string expected = $"Song2";
        string searchFor = "Rock";
        //Act
        string result = this._song.AddAndListSongs(songs, searchFor);
        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test_AddAndListSongs_ReturnsEmptyString_WhenNoSongsMatchWantedList()
    {
        // Arrange
        string[] songs = { "Pop_Song1_3:30", "Rock_Song2_4:15" };
        string expected = string.Empty;
        string searchFor = "";
        //Act
        string result = this._song.AddAndListSongs(songs, searchFor);
        //Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
