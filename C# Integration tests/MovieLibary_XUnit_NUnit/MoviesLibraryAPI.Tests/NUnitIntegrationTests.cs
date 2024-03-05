using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MoviesLibraryAPI.Controllers;
using MoviesLibraryAPI.Controllers.Contracts;
using MoviesLibraryAPI.Data.Models;
using MoviesLibraryAPI.Services;
using MoviesLibraryAPI.Services.Contracts;
using System.ComponentModel.DataAnnotations;

namespace MoviesLibraryAPI.Tests
{
    [TestFixture]
    public class NUnitIntegrationTests
    {
        private MoviesLibraryNUnitTestDbContext _dbContext;
        private IMoviesLibraryController _controller;
        private IMoviesRepository _repository;
        IConfiguration _configuration;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [SetUp]
        public async Task Setup()
        {
            string dbName = $"MoviesLibraryTestDb_{Guid.NewGuid()}";
            _dbContext = new MoviesLibraryNUnitTestDbContext(_configuration, dbName);

            _repository = new MoviesRepository(_dbContext.Movies);
            _controller = new MoviesLibraryController(_repository);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _dbContext.ClearDatabaseAsync();
        }

        [Test]
        public async Task AddMovieAsync_WhenValidMovieProvided_ShouldAddToDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };

            // Act
            await _controller.AddAsync(movie);

            // Assert
            var resultMovie = await _dbContext.Movies.Find(m => m.Title == "Test Movie").FirstOrDefaultAsync();
            Assert.IsNotNull(resultMovie);
        }

        [Test]
        public async Task AddMovieAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Arrange
            var invalidMovie = new Movie
            {
                Title = "",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };

            // Act and Assert
            // Expect a ValidationException because the movie is missing a required field
            var exception = Assert.ThrowsAsync<ValidationException>(() => _controller.AddAsync(invalidMovie));
            Assert.AreEqual(exception.Message, "Movie is not valid.");
        }

        [Test]
        public async Task DeleteAsync_WhenValidTitleProvided_ShouldDeleteMovie()
        {
            // Arrange            
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 86,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);
            // Act
            await _controller.DeleteAsync(movie.Title);
            var resultMovie = await _dbContext.Movies.Find(m => m.Title == "Test Movie").FirstOrDefaultAsync();

            // Assert
            // The movie should no longer exist in the database
            Assert.IsNull(resultMovie);

        }


        [Test]
        public async Task DeleteAsync_WhenTitleIsNull_ShouldThrowArgumentException()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentException>(() => _controller.DeleteAsync(null));
        }

        [Test]
        public async Task DeleteAsync_WhenTitleIsEmpty_ShouldThrowArgumentException()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentException>(() => _controller.DeleteAsync(" "));
        }

        [Test]
        public async Task DeleteAsync_WhenTitleDoesNotExist_ShouldThrowInvalidOperationException()
        {
            // Act and Assert
            string invalidTitle = "Title Does not exist";
            var result = Assert.ThrowsAsync<InvalidOperationException>(() => _controller.DeleteAsync(invalidTitle));
            Assert.AreEqual(result.Message, $"Movie with title '{invalidTitle}' not found.");
        }

        [Test]
        public async Task GetAllAsync_WhenNoMoviesExist_ShouldReturnEmptyList()
        {
            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllAsync_WhenMoviesExist_ShouldReturnAllMovies()
        {
            // Arrange
            var validMovie = new Movie
            {
                Title = "TitanicTest",
                Director = "Atanas Grozev",
                YearReleased = 2000,
                Genre = "comedy",
                Duration = 200,
                Rating = 5

            };
            var validMovie2 = new Movie
            {
                Title = "TitanicTest2",
                Director = "Atanas Grozev2",
                YearReleased = 2000,
                Genre = "comedy2",
                Duration = 200,
                Rating = 5

            };

            await _controller.AddAsync(validMovie);
            await _controller.AddAsync(validMovie2);

            // Act
            var allMovies = await _controller.GetAllAsync();

            // Assert
            // Ensure that all movies are returned
            var movie1 = allMovies.FirstOrDefault(m => m.Title == validMovie.Title);
            var movie2 = allMovies.FirstOrDefault(m => m.Title == validMovie2.Title);
            Assert.IsNotNull(movie1);
            Assert.IsNotNull(movie2);



        }

        [Test]
        public async Task GetByTitle_WhenTitleExists_ShouldReturnMatchingMovie()
        {
            // Arrange
            var validMovie = new Movie
            {
                Title = "TitanicTest",
                Director = "Atanas Grozev",
                YearReleased = 2000,
                Genre = "comedy",
                Duration = 200,
                Rating = 5

            };
            await _controller.AddAsync(validMovie);

            // Act
            var result = await _controller.SearchByTitleFragmentAsync(validMovie.Title);

            // Assert
            Assert.IsTrue(result.Any(r => r.Title == validMovie.Title));
            Assert.IsTrue(result.Any(r => r.Genre == validMovie.Genre));
            Assert.IsTrue(result.Any(r => r.Director == validMovie.Director));

        }

        [Test]
        public async Task GetByTitle_WhenTitleDoesNotExist_ShouldReturnNull()
        {
            // Act
            var resultMovie = await _controller.GetByTitle("DoesNotExist");
            // Assert

            Assert.IsNull(resultMovie);
        }


        [Test]
        public async Task SearchByTitleFragmentAsync_WhenTitleFragmentExists_ShouldReturnMatchingMovies()
        {
            // Arrange
            var validMovie = new Movie
            {
                Title = "TitanicTest",
                Director = "Atanas Grozev",
                YearReleased = 2000,
                Genre = "comedy",
                Duration = 200,
                Rating = 5

            };
            var validMovie2 = new Movie
            {
                Title = "TitanicTest",
                Director = "Atanas Grozev",
                YearReleased = 2000,
                Genre = "comedy",
                Duration = 200,
                Rating = 5

            };
            await _controller.AddAsync(validMovie);
            await _controller.AddAsync(validMovie2);

            // Act
            var result = await _controller.SearchByTitleFragmentAsync(validMovie.Title);

            // Assert // Should return one matching movie

            Assert.IsTrue(result.Count() >= 2);
            Assert.IsTrue(result.Any(r => r.Title == validMovie.Title));
        }

        [Test]
        public async Task SearchByTitleFragmentAsync_WhenNoMatchingTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var validMovie = new Movie
            {
                Title = "TitanicTest",
                Director = "Atanas Grozev",
                YearReleased = 2000,
                Genre = "comedy",
                Duration = 200,
                Rating = 5

            };
            var validMovie2 = new Movie
            {
                Title = "TitanicTest",
                Director = "Atanas Grozev",
                YearReleased = 2000,
                Genre = "comedy",
                Duration = 200,
                Rating = 5

            };
            await _controller.AddAsync(validMovie);
            await _controller.AddAsync(validMovie2);
            // Act and Assert
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => _controller.SearchByTitleFragmentAsync("DoesNotMatchTit"));
            Assert.AreEqual(result.Message, "No movies found.");
        }

        [Test]
        public async Task UpdateAsync_WhenValidMovieProvided_ShouldUpdateMovie()
        {
            // Arrange
            var validMovie = new Movie
            {
                Title = "TitanicTest",
                Director = "Atanas Grozev",
                YearReleased = 2000,
                Genre = "comedy",
                Duration = 200,
                Rating = 5
            };

            await _controller.AddAsync(validMovie);

            // Modify the movie
            validMovie.Title = "UpdateMovieTestMongoDB";

            // Act
            await _controller.UpdateAsync(validMovie);

            // Create a filter to find the updated movie
            var filter = Builders<Movie>.Filter.Eq("Title", "UpdateMovieTestMongoDB");

            // Assert
            // Use FindAsync with the filter
            var result = await _dbContext.Movies.Find(filter).FirstOrDefaultAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual("UpdateMovieTestMongoDB", result.Title);
            // You can add more asserts here to check other fields if necessary
        }


        [Test]
        public async Task UpdateAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Arrange
            // Arrange
            var validMovie = new Movie
            {
                Title = "TitanicTest",
                Director = "Atanas Grozev",
                YearReleased = 2000,
                Genre = "comedy",
                Duration = 200,
                Rating = 5
            };

            await _controller.AddAsync(validMovie);

            // Modify the movie
            validMovie.Title = null;

            // Act
          
            // Movie without required fields


            // Act and Assert
            var result = Assert.ThrowsAsync<ValidationException>( () => _controller.UpdateAsync(validMovie));
            Assert.AreEqual(result.Message, "Movie is not valid.");

        }


        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await _dbContext.ClearDatabaseAsync();
        }
    }
}
