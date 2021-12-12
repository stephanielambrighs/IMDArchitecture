using System;
using Xunit;
using IMDArchitecture.API.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using IMDArchitecture.API.Ports;
using IMDArchitecture.API.Models;
using IMDArchitecture.API.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IMDArchitecture.Test.UnitTests
{
    // Only partially tests the controller; the other endpoints are more of a reader's exercise. PR's welcome.
    // Don't pull this on me with your own project ;-).
    public class EventControllerUnitTest
    {
        // Mock the logger as STDOUT/STDIN is *** slow.
        private Mock<ILogger<EventController>> _mockedLogger = new Mock<ILogger<EventController>>();
        // Notice that we don't care what our database implementation looks like, since we "mock" (i.e. fake) it.
        // We are testing the behaviour of our controller in this class, not of the database. 
        private Mock<IEventRepository> _mockedDatabaseEvent = new Mock<IEventRepository>();


        public EventControllerUnitTest()
        {
            _mockedDatabaseEvent.Reset();
            _mockedLogger.Reset();
        }

        [Fact]
        public async Task TestGetById_HappyPath()
        {
            // arrange
            // this is our happy flow: we ask for the id of an existing application
            var ourId = Guid.NewGuid();
            var ourEvent = new Event { EventId = ourId, Name = "yes", Description = "New Event", Date = 2021, ParticipantCount = 2, TargetAge = 18 };
            // set up the mock so that when we call the 'GetMovieById' method we return a predefined task
            // No database calls are happening here.
            _mockedDatabaseEvent.Setup(x => x.GetEventById(ourId)).Returns(Task.FromResult(ourEvent));
            // act
            var controller = new EventController(_mockedLogger.Object, _mockedDatabaseEvent.Object);
            var actualResult = await controller.GetEventById(ourId.ToString()) as OkObjectResult;

            // assert
            Assert.Equal(200, actualResult.StatusCode);
            var viewModel = actualResult.Value as ViewEvent;
            Assert.Equal(ourEvent.EventId.ToString(), viewModel.EventId);
            Assert.Equal(ourEvent.Name, viewModel.Name);
            Assert.Equal(ourEvent.Description, viewModel.Description);
            Assert.Equal(ourEvent.Date, viewModel.Date);
            Assert.Equal(ourEvent.ParticipantCount, viewModel.ParticipantCount);
            Assert.Equal(ourEvent.TargetAge, viewModel.TargetAge);

            _mockedLogger.VerifyAll();
            _mockedDatabaseEvent.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_DoesntExist()
        {
            // arrange
            var ourId = Guid.NewGuid();
            var Event = new Event { EventId = ourId, Name = "yes", Description = "New Event", Date = 2021, ParticipantCount = 2, TargetAge = 18 };
            _mockedDatabaseEvent.Setup(x => x.GetEventById(ourId)).Returns(Task.FromResult(null as Event));

            // act
            var controller = new EventController(_mockedLogger.Object, _mockedDatabaseEvent.Object);

            // assert
            var result = await new EventController(_mockedLogger.Object, _mockedDatabaseEvent.Object).GetEventById(ourId.ToString());
            Assert.IsType<NotFoundResult>(result);

            _mockedLogger.VerifyAll();
            _mockedDatabaseEvent.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_ErrorOnRetrievalAsync()
        {
            // arrange
            var ourId = Guid.NewGuid();
            var ourEvent = new Event { EventId = ourId, Name = "yes", Description = "New Event", Date = 2021, ParticipantCount = 2, TargetAge = 18 };

            _mockedDatabaseEvent.Setup(x => x.GetEventById(ourId)).ThrowsAsync(new Exception("Biebob"));

            // act
            var result = await new EventController(_mockedLogger.Object, _mockedDatabaseEvent.Object).GetEventById(ourId.ToString());

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockedLogger.VerifyAll();
            _mockedDatabaseEvent.VerifyAll();
        }
    }
}