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
    public class UserControllerUnitTest
    {
        // Mock the logger as STDOUT/STDIN is *** slow.
        private Mock<ILogger<UserController>> _mockedLogger = new Mock<ILogger<UserController>>();
        // Notice that we don't care what our database implementation looks like, since we "mock" (i.e. fake) it.
        // We are testing the behaviour of our controller in this class, not of the database. 
        private Mock<IUserRepository> _mockedDatabaseUser = new Mock<IUserRepository>();

        public UserControllerUnitTest()
        {
            _mockedDatabaseUser.Reset();
            _mockedLogger.Reset();
        }

        [Fact]
        public async Task TestGetById_HappyPath()
        {
            // arrange
            // this is our happy flow: we ask for the id of an existing application
            var ourId = Guid.NewGuid();
            var ourUser = new User { UserId = ourId, Firstname = "Jurien", Lastname = "Rodi", Email = "jurien.rodi@hotmail.com", DateOfBirth = 20, Administrator = false };
            // set up the mock so that when we call the 'GetMovieById' method we return a predefined task
            // No database calls are happening here.
            _mockedDatabaseUser.Setup(x => x.GetUserById(ourId)).Returns(Task.FromResult(ourUser));

            // act
            var controller = new UserController(_mockedLogger.Object, _mockedDatabaseUser.Object);
            var actualResult = await controller.GetUserById(ourId.ToString()) as OkObjectResult;

            // assert
            Assert.Equal(200, actualResult.StatusCode);
            var viewModel = actualResult.Value as ViewUser;
            Assert.Equal(ourUser.UserId.ToString(), viewModel.UserId);
            Assert.Equal(ourUser.Firstname, viewModel.Firstname);
            Assert.Equal(ourUser.Lastname, viewModel.Lastname);
            Assert.Equal(ourUser.Email, viewModel.Email);
            Assert.Equal(ourUser.DateOfBirth, viewModel.DateOfBirth);
            Assert.Equal(ourUser.Administrator, viewModel.Administrator);

            _mockedLogger.VerifyAll();
            _mockedDatabaseUser.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_DoesntExist()
        {
            // arrange
            var ourId = Guid.NewGuid();
            var User = new User { UserId = ourId, Firstname = "Jurien", Lastname = "Rodi", Email = "jurien.rodi@hotmail.com", DateOfBirth = 20, Administrator = false };

            _mockedDatabaseUser.Setup(x => x.GetUserById(ourId)).Returns(Task.FromResult(null as User));

            // act
            var controller = new UserController(_mockedLogger.Object, _mockedDatabaseUser.Object);

            // assert
            var result = await new UserController(_mockedLogger.Object, _mockedDatabaseUser.Object).GetUserById(ourId.ToString());
            Assert.IsType<NotFoundResult>(result);

            _mockedLogger.VerifyAll();
            _mockedDatabaseUser.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_ErrorOnRetrievalAsync()
        {
            // arrange
            var ourId = Guid.NewGuid();
            var ourUser = new User { UserId = ourId, Firstname = "Jurien", Lastname = "Rodi", Email = "jurien.rodi@hotmail.com", DateOfBirth = 20, Administrator = false };

            _mockedDatabaseUser.Setup(x => x.GetUserById(ourId)).ThrowsAsync(new Exception("Jurien"));

            // act
            var result = await new UserController(_mockedLogger.Object, _mockedDatabaseUser.Object).GetUserById(ourId.ToString());

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockedLogger.VerifyAll();
            _mockedDatabaseUser.VerifyAll();
        }
    }
}