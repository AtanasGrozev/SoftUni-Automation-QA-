using ContactsConsoleAPI.Business;
using ContactsConsoleAPI.Business.Contracts;
using ContactsConsoleAPI.Data.Models;
using ContactsConsoleAPI.DataAccess;
using ContactsConsoleAPI.DataAccess.Contrackts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestContactDbContext dbContext;
        private IContactManager contactManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestContactDbContext();
            this.contactManager = new ContactManager(new ContactRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddContactAsync_ShouldAddNewContact()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.NotNull(dbContact);
            Assert.AreEqual(newContact.FirstName, dbContact.FirstName);
            Assert.AreEqual(newContact.LastName, dbContact.LastName);
            Assert.AreEqual(newContact.Phone, dbContact.Phone);
            Assert.AreEqual(newContact.Email, dbContact.Email);
            Assert.AreEqual(newContact.Address, dbContact.Address);
            Assert.AreEqual(newContact.Contact_ULID, dbContact.Contact_ULID);
        }

        //Negative test
        [Test]
        public async Task AddContactAsync_TryToAddContactWithInvalidCredentials_ShouldThrowException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "invalid_Mail", //invalid email
                Gender = "Male",
                Phone = "0889933779"
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await contactManager.AddAsync(newContact));
            var actual = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid contact!"));

        }

        [Test]
        public async Task DeleteContactAsync_WithValidULID_ShouldRemoveContactFromDb()
        {
            //Arrange
           var newContact = new Contact()
           {
               FirstName = "Atanas",
               LastName = "Grozev",
               Address = "Nadezhda 2",
               Contact_ULID = "1ABC23456HH",
               Email = "test@gmail.com",
               Gender = "male",
               Phone = "0889933779"
           };

            // Act

            await contactManager.AddAsync(newContact);
            
             await contactManager.DeleteAsync(newContact.Contact_ULID);         

            // Assert

            var contactInDb = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Contact_ULID == newContact.Contact_ULID);

            Assert.IsNull(contactInDb);         
            
          
        }

        [Test]
       
        public async Task DeleteContactAsync_TryToDeleteWithNullOrWhiteSpaceULID_ShouldThrowException()
        {
            // Arrange        

           var result =  Assert.ThrowsAsync<ArgumentException>(() =>  contactManager.DeleteAsync(" "));

            // Assert
            Assert.AreEqual(result.Message, "ULID cannot be empty.");
            
        }

        [Test]
        public async Task GetAllAsync_WhenContactsExist_ShouldReturnAllContacts()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            // Act
            var result = await contactManager.GetAllAsync();
            // Assert
            Assert.AreEqual(result.Count(), 1);
            Assert.IsTrue(result.Any(x => x.FirstName == newContact.FirstName));
            Assert.IsTrue(result.Any(x => x.Phone == newContact.Phone));
            Assert.IsTrue(result.Any(x => x.Contact_ULID == newContact.Contact_ULID));
         
        }

        [Test]
        public async Task GetAllAsync_WhenNoContactsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.GetAllAsync());
            // Assert
            Assert.AreEqual(result.Message, "No contact found.");


            
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithExistingFirstName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", 
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            await contactManager.AddAsync(newContact);
            // Act
            var result = await contactManager.SearchByFirstNameAsync(newContact.FirstName);

            // Assert
           Assert.AreEqual(result.Count(), 1);
            Assert.IsTrue(result.Any(x =>x.FirstName == newContact.FirstName));
            Assert.IsTrue(result.Any(x => x.LastName == newContact.LastName));
            Assert.IsTrue(result.Any(x => x.Contact_ULID == newContact.Contact_ULID)); 
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithNonExistingFirstName_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByFirstNameAsync("Tenchev"));

            // Assert
            Assert.AreEqual(result.Message, "No contact found with the given first name.");
        }

        [Test]
        public async Task SearchByLastNameAsync_WithExistingLastName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            await contactManager.AddAsync(newContact);
            // Act
            var result = await contactManager.SearchByLastNameAsync(newContact.LastName);

            // Assert
            Assert.AreEqual(result.Count(), 1);
            Assert.IsTrue(result.Any(x => x.FirstName == newContact.FirstName));
            Assert.IsTrue(result.Any(x => x.LastName == newContact.LastName));
            Assert.IsTrue(result.Any(x => x.Contact_ULID == newContact.Contact_ULID));
        }

        [Test]
        public async Task SearchByLastNameAsync_WithNonExistingLastName_ShouldThrowKeyNotFoundException()
        {

            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByLastNameAsync("Tenchev"));

            // Assert
            Assert.AreEqual(result.Message, "No contact found with the given last name.");
        }
        [Test]
        public async Task SearchByLastNameAsync_WithEmpty_ShouldThrowKeyNotFoundException()
        {

            var result = Assert.ThrowsAsync<ArgumentException>(() => contactManager.SearchByLastNameAsync(" "));

            // Assert
            Assert.AreEqual(result.Message, "Last name cannot be empty.");
        }

        [Test]
        public async Task GetSpecificAsync_WithValidULID_ShouldReturnContact()
        {
            // Arrange
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };
            await contactManager.AddAsync(newContact);
            // Act
            var result = await contactManager.GetSpecificAsync(newContact.Contact_ULID);
        
            // Assert
            Assert.AreEqual(result.LastName, newContact.LastName);
            Assert.AreEqual(result.FirstName, newContact.FirstName);


        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidULID_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string ulid = "123";
            // Act
            var result = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.GetSpecificAsync(ulid));

            // Assert
            Assert.AreEqual(result.Message, $"No contact found with ULID: {ulid}");
        }

        [Test]
        public async Task UpdateAsync_WithValidContact_ShouldUpdateContact()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"

            };
            var updatecontact = new Contact()
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"

            };
            await contactManager.AddAsync(newContact);          

            // Act
            contactManager.UpdateAsync(updatecontact);
            var result = await dbContext.Contacts.FirstOrDefaultAsync(x => x.FirstName == updatecontact.FirstName 
            && x.LastName == updatecontact.LastName);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(result.FirstName, updatecontact.FirstName);
            Assert.AreEqual(result.Contact_ULID, updatecontact.Contact_ULID);           

        }

        [Test]
        public async Task UpdateAsync_WithInvalidContact_ShouldThrowValidationException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"

            };
         
            await contactManager.AddAsync(newContact);

            var updatecontact = new Contact()
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",               
            };

            // Act
            
            var result = Assert.ThrowsAsync<ValidationException>(() => contactManager.UpdateAsync(updatecontact));

            // Assert
            Assert.AreEqual(result.Message, "Invalid contact!");
        }
    }
}
