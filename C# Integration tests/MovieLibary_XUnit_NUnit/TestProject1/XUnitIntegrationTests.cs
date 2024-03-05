using DnsClient;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MoviesLibraryAPI.Controllers;
using MoviesLibraryAPI.Controllers.Contracts;
using MoviesLibraryAPI.Data.Models;
using MoviesLibraryAPI.Services;
using MoviesLibraryAPI.Services.Contracts;
using System;

//using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace MoviesLibraryAPI.XUnitTests
{
    public class XUnitIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly MoviesLibraryXUnitTestDbContext _dbContext;
        private readonly IMoviesLibraryController _controller;
        private readonly IMoviesRepository _repository;

        public XUnitIntegrationTests(DatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
            _repository = new MoviesRepository(_dbContext.Movies);
            _controller = new MoviesLibraryController(_repository);
        }

        [Fact]
        public async Task AddMovieAsync_WhenValidMovieProvided_ShouldAddToDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            // Act
            await _controller.AddAsync(movie);

            // Assert
            var resultMovie = await _dbContext.Movies.Find(m => m.Title == "Test Movie").FirstOrDefaultAsync();
           Assert.NotNull(resultMovie);
            Xunit.Assert.Equal("Test Movie", resultMovie.Title);
            Xunit.Assert.Equal("Test Director", resultMovie.Director);
            Xunit.Assert.Equal(2022, resultMovie.YearReleased);
            Xunit.Assert.Equal("Action", resultMovie.Genre);
            Xunit.Assert.Equal(120, resultMovie.Duration);
            Xunit.Assert.Equal(7.5, resultMovie.Rating);
        }

        [Fact]
        public async Task AddMovieAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Arrange
            var invalidMovie = new Movie
            {
                Title = "",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            // Act and Assert
            var result = await Assert.ThrowsAsync<ValidationException>(() => _controller.AddAsync(invalidMovie));
            
          Equals(result.Message, "Movie is not valid.");
        }

        [Fact]
        public async Task DeleteAsync_WhenValidTitleProvided_ShouldDeleteMovie()
        {
            // Arrange
            var validMovie = new Movie
            {
                Title = "Titanic",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            await _controller.AddAsync(validMovie);
            // Act
           await _controller.DeleteAsync(validMovie.Title);


            // Assert
            // The movie should no longer exist in the database
            var result = await _dbContext.Movies.Find(m => m.Title == validMovie.Title).FirstOrDefaultAsync();

            Assert.Null(result);

        }


        [Fact]
        public async Task DeleteAsync_WhenTitleIsNull_ShouldThrowArgumentException()
        {
           
            var result = Assert.ThrowsAsync<ArgumentException>(() => _controller.DeleteAsync(null));
            Assert.Equal(result.Result.Message, "Title cannot be empty.");
        }

        [Fact]
        public async Task DeleteAsync_WhenTitleIsEmpty_ShouldThrowArgumentException()
        {
            var result = Assert.ThrowsAsync<ArgumentException>(() => _controller.DeleteAsync(""));
            Assert.Equal(result.Result.Message, "Title cannot be empty.");
        }

        [Fact]
        public async Task DeleteAsync_WhenTitleDoesNotExist_ShouldThrowInvalidOperationException()
        {
            var DoesNotExistTitiel = "FastAndF";
            var result = Assert.ThrowsAsync<InvalidOperationException>(() => _controller.DeleteAsync(DoesNotExistTitiel));
            Assert.Equal(result.Result.Message, $"Movie with title '{DoesNotExistTitiel}' not found.");
        }

        [Fact]
        public async Task GetAllAsync_WhenNoMoviesExist_ShouldReturnEmptyList()
        {
            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_WhenMoviesExist_ShouldReturnAllMovies()
        {
          
            // Arrange
            var validMovie = new Movie
            {
                Title = "Titanic",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            var validMovie2 = new Movie
            {
                Title = "Titani2c",
                Director = "Test Director2",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            await _controller.AddAsync(validMovie);
            await _controller.AddAsync(validMovie2);

            // Act
            var result1 = await _controller.GetAllAsync();

            // Assert
            // Ensure that all movies are returned
            Assert.Equal(2, result1.Count());
        }

        [Fact]
        public async Task GetByTitle_WhenTitleExists_ShouldReturnMatchingMovie()
        {
            // Arrange
            var validMovie = new Movie
            {
                Title = "Titanic",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            var validMovie2 = new Movie
            {
                Title = "Titani2c",
                Director = "Test Director2",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            await _controller.AddAsync(validMovie);
            await _controller.AddAsync(validMovie2);

            // Act
            var result = await _controller.GetByTitle(validMovie.Title);

            // Assert

            Assert.Equal("Titanic", result.Title);
        }

        [Fact]
        public async Task GetByTitle_WhenTitleDoesNotExist_ShouldReturnNull()
        {
            // Act
            var resultMovie = await _controller.GetByTitle("TitleDoesNotExist");

            // Assert
            Assert.Null(resultMovie);
        }


        [Fact]
        public async Task SearchByTitleFragmentAsync_WhenTitleFragmentExists_ShouldReturnMatchingMovies()
        {
            // Arrange
            // Arrange
            var validMovie = new Movie
            {
                Title = "First Fragment",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            var validMovie2 = new Movie
            {
                Title = "Titani2c",
                Director = "Test Director2",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            await _controller.AddAsync(validMovie);
            await _controller.AddAsync(validMovie2);

            // Act
           var result = await _controller.SearchByTitleFragmentAsync("First");


            // Assert // Should return one matching movie
            Assert.Equal(1, result.Count());
            var movie = result.First();
            Assert.Equal(movie.Title, validMovie.Title);
          
        }

        [Fact]
        public async Task SearchByTitleFragmentAsync_WhenNoMatchingTitleFragment_ShouldThrowKeyNotFoundException()
        {
            var result = Assert.ThrowsAsync<KeyNotFoundException> ( () => _controller.SearchByTitleFragmentAsync("Nothing"));
            Assert.Equal(result.Result.Message, "No movies found.");
        }

        [Fact]
        public async Task UpdateAsync_WhenValidMovieProvided_ShouldUpdateMovie()
        {
            // Arrange
            var validMovie = new Movie
            {
                Title = "First Fragment",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            // Modify the movie        


            await _controller.AddAsync (validMovie);

            var movieUpdate = await _dbContext.Movies.Find(m => m.Title == validMovie.Title).FirstOrDefaultAsync();
            movieUpdate.Title = "Second Fragment";
            await _controller.UpdateAsync(movieUpdate);

            var result = await _controller.SearchByTitleFragmentAsync("Second");


            // Assert // Should return one matching movie
            Assert.Equal(1, result.Count());
            var movie = result.First();
            Assert.Equal(movie.Title, movieUpdate.Title);
        }

        [Fact]
        public async Task UpdateAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
           
            // Arrange
            var validMovie = new Movie
            {
                Title = "First Fragment",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

             


            await _controller.AddAsync(validMovie);

            var movieUpdate = await _dbContext.Movies.Find(m => m.Title == validMovie.Title).FirstOrDefaultAsync();
            movieUpdate.Title = null;
            var result = Assert.ThrowsAsync<ValidationException> (() =>  _controller.UpdateAsync(movieUpdate));

            Assert.Equal(result.Result.Message, "Movie is not valid.");
            // Act and Assert
        }
    }
}
