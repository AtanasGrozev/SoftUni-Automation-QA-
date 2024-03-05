using Homies.Data;
using Homies.Data.Models;
using Homies.Models.Event;
using Homies.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Homies.Tests
{
    [TestFixture]
    internal class EventServiceTests
    {
        private HomiesDbContext _dbContext;
        private EventService _eventService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HomiesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique database name to avoid conflicts
                .Options;
            _dbContext = new HomiesDbContext(options);

            _eventService = new EventService(_dbContext);
        }

        [Test]
        public async Task AddEventAsync_ShouldAddEvent_WhenValidEventModelAndUserId()
        {
            // Step 1: Arrange - Set up the initial conditions for the test
            // Create a new event model with test data
            var eventModel = new EventFormModel
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            };
            // Define a user ID for testing purposes
            string userId = "testUserId";

            // Step 2: Act - Perform the action being tested
            // Call the service method to add the event
            await _eventService.AddEventAsync(eventModel, userId);    

            // Step 3: Assert - Verify the outcome of the action
            // Retrieve the added event from the database
            var eventInTheDatabase = await _dbContext.Events.FirstOrDefaultAsync(x => x.Name == eventModel.Name);

            // Assert that the added event is not null, indicating it was successfully added
            Assert.AreEqual(eventModel.Name, eventInTheDatabase.Name);
            Assert.AreEqual(eventModel.Description, eventInTheDatabase.Description);
            Assert.NotNull(eventInTheDatabase);           
        }


        [Test]
        public async Task GetAllEventsAsync_ShouldReturnAllEvents()
        {
            // Step 1: Arrange - Set up the initial conditions for the test
            // Create two event models with test data
            var eventModel1 = new EventFormModel
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            };
            var eventModel2 = new EventFormModel
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            };

            // Define a user ID for testing purposes
            string userId = "testUserId";
       

            // Step 2: Act - Perform the action being tested
            // Add the two events to the database using the event service
            await _eventService.AddEventAsync(eventModel1, userId);
            await _eventService.AddEventAsync(eventModel2, userId);


            // Step 3: Act - Retrieve the count of events from the database
            var allEvents = await _eventService.GetAllEventsAsync();

            // Step 4: Assert - Verify the outcome of the action
            Assert.AreEqual(allEvents.Count(), 2);
            Assert.IsTrue(allEvents.Any(x => x.Name == eventModel1.Name));
            // Assert that the count of events in the database is equal to the expected count (2)
            
        }
        [Test]
        public async Task GetEventsAsync_ShouldReturnAllEventDetails()
        {
            //arrange
            var eventModel1 = new EventFormModel
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            };         

            string userId = "testUserId";
        
            await _eventService.AddEventAsync(eventModel1, userId);
            var eventExist = await _dbContext.Events.FirstAsync();

            var result = await _eventService.GetEventDetailsAsync(eventExist.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(eventModel1.Name == result.Name); 

        }
        [Test]
        public async Task GetEventForEditAsync_ShouldReturnEventWithCorrectId()
        {
            var eventModel1 = new EventFormModel
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            };
            var eventModel2 = new EventFormModel
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            };
            string userId2 = "testUserId2";
            string userId = "testUserId";
            await _eventService.AddEventAsync(eventModel1, userId);
            await _eventService.AddEventAsync(eventModel2, userId2);
            var eventExist = await _dbContext.Events.FirstAsync();
            var result = await _eventService.GetEventForEditAsync(eventExist.Id);

            Assert.AreEqual(result.Name, eventModel1.Name);

        }
        [Test]
        public async Task GetEventForEditAsync_ShouldReturnNullIfEventNotFound()
        {
            
            var result = await _eventService.GetEventForEditAsync(90);

            Assert.IsNull(result);


        }

        [Test]
        public async Task GetEventOrganizerIdAsync_ShouldReturnOrganizerIdIfExist()
        {
            var eventModel1 = new EventFormModel
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            };
          
            string userId = "testUserId";
            await _eventService.AddEventAsync(eventModel1, userId);
       
            var eventExist = await _dbContext.Events.FirstAsync();

            var result = await _eventService.GetEventOrganizerIdAsync(eventExist.Id);

            Assert.AreEqual(userId, result);
        }
        [Test]
        public async Task GetUserJoinedEventsAsync_Sh() 
        {

        }
        
        [Test]  
        public async Task JoinEventAsyn_ShouldReturnFalseIfEventDoesNotExis()
        {
            //Act
            var result = await _eventService.JoinEventAsync(99, "");
            //Assert
            Assert.False(result);


        }
        [Test]
        public async Task JoinEventAsyn_ShouldReturnFalseIfUserIsAlreadyPartOfEven()
        {
            const string userId = "userId";
            //Add an event type to DataBase
            var testType = new Data.Models.Type
            {
                Name = "TestType",
            };
            await _dbContext.Types.AddAsync(testType);
            await _dbContext.SaveChangesAsync();
            //Add an event ot the Database
            var testEvent = new Event
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                TypeId = testType.Id,
                OrganiserId = userId,
            };
            await _dbContext.Events.AddAsync(testEvent);
            await _dbContext.SaveChangesAsync();
            //Add user to the event
            await _dbContext.EventsParticipants.AddAsync(new EventParticipant()
            {
                EventId = testType.Id,  
                HelperId = userId,

            });
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _eventService.JoinEventAsync(testEvent.Id, userId);
            //Assert
            Assert.False(result);


        }
        [Test]
        public async Task JoinEventAsyn_ShouldReturnTrueIfTheUserIsAddToTheEvent()
        {
            const string userId = "userId";
            //Add an event type to DataBase
            var testType = new Data.Models.Type
            {
                Name = "TestType",
            };
            await _dbContext.Types.AddAsync(testType);
            await _dbContext.SaveChangesAsync();
            //Add an event ot the Database
            var testEvent = new Event
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                TypeId = testType.Id,
                OrganiserId = userId,
            };
            await _dbContext.Events.AddAsync(testEvent);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _eventService.JoinEventAsync(testEvent.Id, userId);

            //Assert
            Assert.True(result);


        }
        [Test]
        public async Task LeaveEventAsyncShouldReturnFalse_IfWeTryToLeaveAnEventWeAreNotPartOf()
        {
            //Arrange
            var userId = "userId";  
            //Act 
             var result = await _eventService.LeaveEventAsync(123 , userId);
            //Assert
            Assert.False(result);

        }
        [Test]
        public async Task LeaveEventAsyncShouldReturnTrue_IfWeTryToLeaveAnEventWeAreNotPart()
        {
           
            //Add an event type to DataBase
            var testType = new Data.Models.Type
            {
                Name = "TestType",
            };
            await _dbContext.Types.AddAsync(testType);
            await _dbContext.SaveChangesAsync();
            //Add an event ot the Database
            var testEvent = new Event
            {
                Name = "Test Event",
                Description = "Test Description",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                TypeId = testType.Id,
                OrganiserId = "a-simple-user",
            };
            await _dbContext.Events.AddAsync(testEvent);
            await _dbContext.SaveChangesAsync();
           string userId = "new-participant";
            await _eventService.JoinEventAsync(testEvent.Id, userId);

            //Act
            var result = await _eventService.LeaveEventAsync(testEvent.Id, userId);

            Assert.True(result);

        }
    }
}
