using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace PasswordManager.UnitTest
{
    public class UserControllerTest
    {
        private UserController _controller;
        private Mock<IUserService> _mockUserService;
        private Mock<ITokenService> _mockTokenService;
        private Mock<ILog> _mockLogger;
        private Mock<IUserLevelService> _mockUserLevelService;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IMemoryCache> _mockMemoryCache;

        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockTokenService = new Mock<ITokenService>();
            _mockLogger = new Mock<ILog>();
            _mockUserLevelService = new Mock<IUserLevelService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockMemoryCache = new Mock<IMemoryCache>();

            _controller = new UserController(
                _mockUserService.Object,
                _mockHttpContextAccessor.Object,
                _mockTokenService.Object,
                _mockLogger.Object,                              
                _mockMemoryCache.Object,
                _mockUserLevelService.Object);
        }

        [Fact]
        public async Task Login_WithNullOrEmptyCredentials_ReturnsInternalServerError()
        {
            // Arrange
            var request = new UserViewModels
            {
                UserName = "",
                Password = ""
            };

            // Act
            var actionResult = await _controller.Login(request);

            // Assert
            Assert.IsType<StatusCodeResult>(actionResult);
            var statusCodeResult = (StatusCodeResult)actionResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public async Task Login_WithNullUser_ReturnsInternalServerError()
        {
            // Arrange
            var request = new UserViewModels
            {
                UserName = "testuser",
                Password = "password123"
            };

            UserViewModels user = null;

            _mockUserService.Setup(x => x.Login(request)).ReturnsAsync(user);

            // Act
            var actionResult = await _controller.Login(request);

            // Assert
            Assert.IsType<StatusCodeResult>(actionResult);
            var statusCodeResult = (StatusCodeResult)actionResult;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var request = new UserViewModels
            {
                UserName = "Burak Güler",
                Password = "123"
            };

            var user = new UserViewModels
            {
                UserID = 1,
                UserName = "Burak Güler",
                Password = "123"
            };

            var tokenResponse = new LoginResponse
            {
                AuthToken = "test_token",
                Password="123",
                UserID = 1, 
                UserName ="Burak Güler"
            };

            _mockUserService.Setup(x => x.Login(request)).ReturnsAsync(user);
            _mockTokenService.Setup(x => x.GenerateToken(user)).Returns(tokenResponse);

            // Act
            var actionResult = await _controller.Login(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsType<LoginResponse>(okObjectResult.Value);

            Assert.Equal(tokenResponse.AuthToken, result.AuthToken);            
        }


        [Fact]
        public async Task GetAllUsers_ReturnsOkResultWithValues()
        {
            // Arrange
            var users = new List<UserViewModels>
            {
                new UserViewModels { UserID = 1, UserName = "user1" },
                new UserViewModels { UserID = 2, UserName = "user2" }
            };

            _mockUserService.Setup(x => x.GetAll()).ReturnsAsync(users);

            // Act
            var actionResult = await _controller.GetAllUsers();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsAssignableFrom<List<UserViewModels>>(okObjectResult.Value);

            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count);
        }


        [Fact]
        public async Task GetAllByCompanyIDUser_ReturnsOkResultWithValues()
        {
            // Arrange
            int companyId = 1;
            var users = new List<UserViewModels>
            {
                new UserViewModels { UserID = 1, UserName = "user1" },
                new UserViewModels { UserID = 2, UserName = "user2" }
            };

            _mockUserService.Setup(x => x.GetAllByCompanyId(companyId)).ReturnsAsync(users);

            // Act
            var actionResult = await _controller.GetAllBYCompanyIDUser(companyId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsAssignableFrom<List<UserViewModels>>(okObjectResult.Value);

            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count);
        }



    }
}
