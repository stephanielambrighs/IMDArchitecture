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
    public class UserControllerUnitTest
    {
        private Mock<ILogger<UserController>> _mockedLogger = new Mock<ILogger<UserController>>();
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
            var ourId = 1;
            var ourUser = new User { UserId = ourId, Firstname = "Jurien", Lastname = "Rodi", Email = "jurien.rodi@hotmail.com", DateOfBirth = Convert.ToDateTime("2018-11-05 11:38:56.307"), Administrator = false };

            // No database calls are happening here.
            _mockedDatabaseUser.Setup(x => x.GetUserById(ourId)).Returns(Task.FromResult(ourUser));

            // act
            var controller = new UserController(_mockedLogger.Object, _mockedDatabaseUser.Object);
            var actualResult = await controller.GetUserById(ourId) as OkObjectResult;

            // assert
            Assert.Equal(200, actualResult.StatusCode);
            var viewModel = actualResult.Value as ViewUser;
            Assert.Equal(ourUser.UserId, viewModel.UserId);
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
            var ourId = 1;
            var User = new User { UserId = ourId, Firstname = "Jurien", Lastname = "Rodi", Email = "jurien.rodi@hotmail.com", DateOfBirth = Convert.ToDateTime("2018-11-05 11:38:56.307"), Administrator = false };
            _mockedDatabaseUser.Setup(x => x.GetUserById(ourId)).Returns(Task.FromResult(null as User));

            // act
            var controller = new UserController(_mockedLogger.Object, _mockedDatabaseUser.Object);

            // assert
            var result = await new UserController(_mockedLogger.Object, _mockedDatabaseUser.Object).GetUserById(ourId);
            Assert.IsType<NotFoundResult>(result);

            _mockedLogger.VerifyAll();
            _mockedDatabaseUser.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_ErrorOnRetrievalAsync()
        {
            // arrange
            var ourId = 1;
            var ourUser = new User { UserId = ourId, Firstname = "Jurien", Lastname = "Rodi", Email = "jurien.rodi@hotmail.com", DateOfBirth = Convert.ToDateTime("2018-11-05 11:38:56.307"), Administrator = false };
            _mockedDatabaseUser.Setup(x => x.GetUserById(ourId)).ThrowsAsync(new Exception("Jurien"));

            // act
            var result = await new UserController(_mockedLogger.Object, _mockedDatabaseUser.Object).GetUserById(ourId);

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockedLogger.VerifyAll();
            _mockedDatabaseUser.VerifyAll();
        }
    }
}