using GardenConsoleAPI.Business;
using GardenConsoleAPI.Business.Contracts;
using GardenConsoleAPI.Data.Models;
using GardenConsoleAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GardenConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestPlantsDbContext dbContext;
        private IPlantsManager plantsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestPlantsDbContext();
            this.plantsManager = new PlantsManager(new PlantsRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddPlantAsync_ShouldAddNewPlant()
        {
            // Arrange
            var validPlant = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F6", 
                IsEdible = true,
                Name = "Rosemary", 
                FoodType = "Herb", 
                Quantity = 200 
            };

            // Act
            await plantsManager.AddAsync(validPlant);
            var result = await dbContext.Plants.FirstOrDefaultAsync(x => x.PlantType == "Herbs");


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.PlantType, validPlant.PlantType);
            Assert.AreEqual(result.CatalogNumber, validPlant.CatalogNumber);
            Assert.AreEqual(result.Name, validPlant.Name);
            Assert.AreEqual(result.Quantity, validPlant.Quantity);


        }

        //Negative test
        [Test]
        public async Task AddPlantAsync_TryToAddPlantWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var invalidPlant = new Plant
            {
                PlantType = "Herbs", // до 30 символа
                CatalogNumber = "A1B2C3D4E5F6", // точно 12 символа, съответства на регулярния израз
                IsEdible = true,
                Name = "Rosemary", // до 70 символа
                FoodType = "12345123451234512345123451234512345", // предполага се, че е също до 30 символа
                Quantity = 200 // между 0 и 1000
            };

            // Act
            var result = Assert.ThrowsAsync<ValidationException>(() => plantsManager.AddAsync(invalidPlant));

            // Assert
            Assert.AreEqual(result.Message, "Invalid plant!");

        }

        [Test]
        public async Task DeletePlantAsync_WithValidCatalogNumber_ShouldRemovePlantFromDb()
        {
            // Arrange
            var validPlant = new Plant
            {
                PlantType = "Herbs", 
                CatalogNumber = "A1B2C3D4E5F6", 
                IsEdible = true,
                Name = "Rosemary", 
                FoodType = "Herb", 
                Quantity = 200 
            };

            // Act
            await plantsManager.AddAsync(validPlant);
            await plantsManager.DeleteAsync(validPlant.CatalogNumber);
            // Act
            var result = await dbContext.Plants.FirstOrDefaultAsync(p => p.CatalogNumber == validPlant.CatalogNumber);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task DeletePlantAsync_TryToDeleteWithNullOrWhiteSpaceCatalogNumber_ShouldThrowException()
        {
            // Arrange
            string invalidCatalogNumber = " ";
            string invalidCatalogNumber2 = null;

            // Act
            var result = Assert.ThrowsAsync<ArgumentException>(() => plantsManager.DeleteAsync(invalidCatalogNumber));
            var result2 = Assert.ThrowsAsync<ArgumentException>(() => plantsManager.DeleteAsync(invalidCatalogNumber2));

            // Assert
            Assert.AreEqual(result.Message, "Catalog number cannot be empty.");
            Assert.AreEqual(result2.Message, "Catalog number cannot be empty.");
        }

        [Test]
        public async Task GetAllAsync_WhenPlantsExist_ShouldReturnAllPlants()
        {
            // Arrange
            // Arrange
            var validPlant1 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F6",
                IsEdible = true,
                Name = "Rosemary",
                FoodType = "Herb",
                Quantity = 200
            };
            var validPlant2 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F7",
                IsEdible = true,
                Name = "Lavender",
                FoodType = "Herb",
                Quantity = 150
            };
          
            await plantsManager.AddAsync(validPlant1);
            await plantsManager.AddAsync(validPlant2 );
            // Act
            var result = await plantsManager.GetAllAsync();  

            // Assert
            Assert.AreEqual(result.Count() , 2 );   
            Assert.IsTrue(result.Any(p => p.CatalogNumber == validPlant1.CatalogNumber) );
            Assert.IsTrue(result.Any(p => p.CatalogNumber == validPlant2.CatalogNumber) );
        }

        [Test]
        public async Task GetAllAsync_WhenNoPlantsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => plantsManager.GetAllAsync());

            // Assert
            Assert.AreEqual(result.Message, "No plant found.");
        }

        [Test]
        public async Task SearchByFoodTypeAsync_WithExistingFoodType_ShouldReturnMatchingPlants()
        {
            // Arrange
            var validPlant1 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F6",
                IsEdible = true,
                Name = "Rosemary",
                FoodType = "Vegetable",
                Quantity = 200
            };
            var validPlant2 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F7",
                IsEdible = true,
                Name = "Lavender",
                FoodType = "Herb",
                Quantity = 150
            };

            await plantsManager.AddAsync(validPlant1);
            await plantsManager.AddAsync(validPlant2);
            // Act
            var result = await plantsManager.SearchByFoodTypeAsync(validPlant1.FoodType);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count(), 1);
            Assert.IsTrue(result.Any(p => p.Quantity == validPlant1.Quantity));

        }

        [Test]
        public async Task SearchByFoodTypeAsync_WithNonExistingFoodType_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var validPlant1 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F6",
                IsEdible = true,
                Name = "Rosemary",
                FoodType = "Herb",
                Quantity = 200
            };
            var validPlant2 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F7",
                IsEdible = true,
                Name = "Lavender",
                FoodType = "Herb",
                Quantity = 150
            };
            string foodTypeDoesNotExist = "Fruit";

            await plantsManager.AddAsync(validPlant1);
            await plantsManager.AddAsync(validPlant2);

            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => plantsManager
            .SearchByFoodTypeAsync(foodTypeDoesNotExist));

            // Assert
            Assert.AreEqual(result.Message, "No plant found with the given food type.");
        }
        [Test]
        public async Task SearchByFoodTypeAsync_WithNullOrWhiteSpace_ShouldThrowArgumentException()
        {
            // Arrange
            var validPlant1 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F6",
                IsEdible = true,
                Name = "Rosemary",
                FoodType = "Herb",
                Quantity = 200
            };
            var validPlant2 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F7",
                IsEdible = true,
                Name = "Lavender",
                FoodType = "Herb",
                Quantity = 150
            };
            string foodTypeDoesNotExist = null;

            await plantsManager.AddAsync(validPlant1);
            await plantsManager.AddAsync(validPlant2);

            // Act
            var result = Assert.ThrowsAsync<ArgumentException>(() => plantsManager
            .SearchByFoodTypeAsync(foodTypeDoesNotExist));

            // Assert
            Assert.AreEqual(result.Message, "Food type cannot be empty.");
        }

        [Test]
        public async Task GetSpecificAsync_WithValidCatalogNumber_ShouldReturnPlant()
        {
            // Arrange
            // Arrange
            var validPlant1 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F6",
                IsEdible = true,
                Name = "Rosemary",
                FoodType = "Herb",
                Quantity = 200
            };
            var validPlant2 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F7",
                IsEdible = true,
                Name = "Lavender",
                FoodType = "Herb",
                Quantity = 150
            };          

            await plantsManager.AddAsync(validPlant1);
            await plantsManager.AddAsync(validPlant2);

            // Act
            var result = await plantsManager.GetSpecificAsync(validPlant1.CatalogNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CatalogNumber , validPlant1 .CatalogNumber);
          
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidCatalogNumber_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidCatalogueNumber = "1";
            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => plantsManager.GetSpecificAsync(invalidCatalogueNumber));

            // Assert
            Assert.AreEqual(result.Message, $"No plant found with catalog number: {invalidCatalogueNumber}");
        }

        [Test]
        public async Task UpdateAsync_WithValidPlant_ShouldUpdatePlant()
        {
            
            // Arrange
            var validPlant = new Plant
            {
                PlantType = "Herbs", 
                CatalogNumber = "A1B2C3D4E5F6", 
                IsEdible = true,
                Name = "Rosemary", 
                FoodType = "Herb", 
                Quantity = 200 
            };
            var validPlant2 = new Plant
            {
                PlantType = "Herbs",
                CatalogNumber = "A1B2C3D4E5F7",
                IsEdible = true,
                Name = "Lavender",
                FoodType = "Herb",
                Quantity = 150
            };

            // Act
            await plantsManager.AddAsync(validPlant);

            // Act
            await plantsManager.UpdateAsync(validPlant2);
            var result = await dbContext.Plants.FirstOrDefaultAsync(p =>
    p.PlantType == validPlant2.PlantType &&
    p.CatalogNumber == validPlant2.CatalogNumber &&
    p.Name == validPlant2.Name &&
    p.FoodType == validPlant2.FoodType &&
    p.Quantity == validPlant2.Quantity &&
    p.IsEdible == validPlant2.IsEdible);



            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CatalogNumber , validPlant2.CatalogNumber);
        }

        [Test]
        public async Task UpdateAsync_WithInvalidPlant_ShouldThrowValidationException()
        {
            // Arrange
            var invalidPlant = new Plant
            {
                PlantType = "1",
                CatalogNumber = "1",
                IsEdible = true,
                Name = "Rosemary",
                FoodType = "1",
                Quantity = -3
            };

            // Act
            var result = Assert.ThrowsAsync<ValidationException>(() => plantsManager.UpdateAsync(invalidPlant));

            // Assert
            Assert.AreEqual(result.Message, "Invalid plant!");
        }
    }
}
