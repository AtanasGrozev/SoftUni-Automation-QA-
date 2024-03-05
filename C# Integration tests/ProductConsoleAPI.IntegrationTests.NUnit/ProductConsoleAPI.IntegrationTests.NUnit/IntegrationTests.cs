using Microsoft.EntityFrameworkCore;
using ProductConsoleAPI.Business;
using ProductConsoleAPI.Business.Contracts;
using ProductConsoleAPI.Data.Models;
using ProductConsoleAPI.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace ProductConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestProductsDbContext dbContext;
        private IProductsManager productsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestProductsDbContext();
            this.productsManager = new ProductsManager(new ProductsRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddProductAsync_ShouldAddNewProduct()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            var dbProduct = await this.dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode == newProduct.ProductCode);

            Assert.NotNull(dbProduct);
            Assert.AreEqual(newProduct.ProductName, dbProduct.ProductName);
            Assert.AreEqual(newProduct.Description, dbProduct.Description);
            Assert.AreEqual(newProduct.Price, dbProduct.Price);
            Assert.AreEqual(newProduct.Quantity, dbProduct.Quantity);
            Assert.AreEqual(newProduct.OriginCountry, dbProduct.OriginCountry);
            Assert.AreEqual(newProduct.ProductCode, dbProduct.ProductCode);
        }

        //Negative test
        [Test]
        public async Task AddProductAsync_TryToAddProductWithInvalidCredentials_ShouldThrowException()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = -1m,
                Quantity = 100,
                Description = "Anything for description"
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await productsManager.AddAsync(newProduct));
            var actual = await dbContext.Products.FirstOrDefaultAsync(c => c.ProductCode == newProduct.ProductCode);

            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid product!"));

        }

        [Test]
        public async Task DeleteProductAsync_WithValidProductCode_ShouldRemoveProductFromDb()
        {
            // Arrange
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 10,
                Quantity = 100,
                Description = "Anything for description"
            };
            await this.productsManager.AddAsync(newProduct);
            await this.productsManager.DeleteAsync(newProduct.ProductCode);

            // Act
            var result = this.dbContext.Products.Any(c => c.ProductName == newProduct.ProductName);
            var resultCount = await this.dbContext.Products.FirstOrDefaultAsync(x => x.ProductCode == newProduct.ProductCode);
            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(resultCount);

        }

        [Test]
        public async Task DeleteProductAsync_TryToDeleteWithNullOrWhiteSpaceProductCode_ShouldThrowException()
        {
            // Arrange
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 10,
                Quantity = 100,
                Description = "Anything for description"
            };
            var result = Assert.ThrowsAsync<ArgumentException>(() => this.productsManager.DeleteAsync(null));

            Assert.AreEqual(result.Message, "Product code cannot be empty.");


        }
        [Test]
        public async Task DeleteProductAsync_TryToDeleteWithWhiteSpaceProductCode_ShouldThrowException()
        {
            // Arrange

            var result = Assert.ThrowsAsync<ArgumentException>(() => this.productsManager.DeleteAsync(" "));

            Assert.AreEqual(result.Message, "Product code cannot be empty.");


        }

        [Test]
        public async Task GetAllAsync_WhenProductsExist_ShouldReturnAllProducts()
        {
            // Arrange
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };
            var newProduct2 = new Product()
            {
                OriginCountry = "Bulgaria2",
                ProductName = "TestProduct2",
                ProductCode = "AB12C2",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);
            await productsManager.AddAsync(newProduct2);
            // Act
            var result = await productsManager.GetAllAsync();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.NotNull(result);
            Assert.IsTrue(result.Any(x => x.ProductCode == newProduct.ProductCode));


        }

        [Test]
        public async Task GetAllAsync_WhenNoProductsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.GetAllAsync());

            // Assert
            Assert.AreEqual(result.Message, "No product found.");
        }

        [Test]
        public async Task SearchByOriginCountry_WithExistingOriginCountry_ShouldReturnMatchingProducts()
        {
            // Arrange

            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);
            // Act
            var result = await productsManager.SearchByOriginCountry(newProduct.OriginCountry);

            // Assert
            Assert.IsTrue(result.Any(x => x.OriginCountry == newProduct.OriginCountry));
        }

        [Test]
        public async Task SearchByOriginCountryAsync_WithNonExistingOriginCountry_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.SearchByOriginCountry("Kurtovo"));

            // Assert
            Assert.AreEqual(result.Message, "No product found with the given first name.");
        }
        [Test]
        public async Task SearchByOriginCountryAsync_WithNullOrWhiteSpace_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var result = Assert.ThrowsAsync<ArgumentException>(() => productsManager.SearchByOriginCountry(null));

            // Assert
            Assert.AreEqual(result.Message, "Country name cannot be empty.");
        }
        [Test]
        public async Task SearchByOriginCountryAsync_CountryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);
            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.SearchByOriginCountry("Japan"));

            // Assert
            Assert.AreEqual(result.Message, "No product found with the given first name.");
        }

        [Test]
        public async Task GetSpecificAsync_WithValidProductCode_ShouldReturnProduct()
        {
            // Arrange
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);


            // Act
            var result = await productsManager.GetSpecificAsync(newProduct.ProductCode);

            // Assert
            Assert.IsTrue(result.ProductCode.Equals(newProduct.ProductCode));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidProductCode_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string prodcutCode = "1";
            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.GetSpecificAsync(prodcutCode));

            // Assert
            Assert.AreEqual(result.Message, $"No product found with product code: {prodcutCode}");
        }
        [Test]
        public async Task GetSpecificAsync_WithNull_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string prodcutCode = null;
            // Act
            var result = Assert.ThrowsAsync<ArgumentException>(() => productsManager.GetSpecificAsync(prodcutCode));

            // Assert
            Assert.AreEqual(result.Message, $"Product code cannot be empty.");
        }

        [Test]
        public async Task UpdateAsync_WithValidProduct_ShouldUpdateProduct()
        {
            // Arrange
            var newProduct = new Product()
            {
                OriginCountry = "ZaTest",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };
            var newProductUpdated = new Product()
            {
                OriginCountry = "German",
                ProductName = "TestProduct1",
                ProductCode = "AB12C12",
                Price = 1.25m,
                Quantity = 105,
                Description = "Anything for description2"
            };

            await productsManager.AddAsync(newProduct);
            await productsManager.UpdateAsync(newProductUpdated);

            var result = await this.dbContext.Products.AnyAsync(x => x.ProductCode == "AB12C12");

            Assert.IsTrue(result);
          
          
        }

        [Test]
        public async Task UpdateAsync_WithInvalidProduct_ShouldThrowValidationException()
        {
            // Arrange
            Product product = new Product()
            {
                OriginCountry = "ZaTest",
                ProductName = "TestProduct",
                ProductCode = "AB12C",

            };

            // Act
            var result = Assert.ThrowsAsync<ValidationException>(() =>  this.productsManager.UpdateAsync(product));
            // Assert

            Assert.AreEqual(result.Message, "Invalid prduct!");
        
        }
    }
}
