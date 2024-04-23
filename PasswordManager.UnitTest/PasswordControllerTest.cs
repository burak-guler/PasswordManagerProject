using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using WebApi.Controllers;

namespace PasswordManager.UnitTest
{
    public class PasswordControllerTest
    {
        private readonly PasswordController _controller;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly Mock<ILog> _mockLogger;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IUserLevelService> _mockUserLevelService;

        public PasswordControllerTest()
        {
            _mockPasswordService = new Mock<IPasswordService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockLogger = new Mock<ILog>();
            _mockUserService = new Mock<IUserService>();
            _mockUserLevelService = new Mock<IUserLevelService>();

            _controller = new PasswordController(
                _mockPasswordService.Object,
                _mockHttpContextAccessor.Object,
                _mockLogger.Object,
                _mockMemoryCache.Object,
                _mockUserService.Object,
                _mockUserLevelService.Object);
        }


        [Fact]
        public async Task GetAllPassword_ReturnsOkWithValues()
        {
            // Arrange
            var passwords = new List<PasswordViewModels>
            {
                new PasswordViewModels { PasswordID = 1, UserID = 1, PasswordValue = "password1" },
                new PasswordViewModels { PasswordID = 2, UserID = 1, PasswordValue = "password2" }
            };

            _mockPasswordService.Setup(x => x.GetAll()).ReturnsAsync(passwords);

            // Act
            var actionResult = await _controller.GetAllPassword();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<PasswordViewModels>>(okObjectResult.Value);

            Assert.NotNull(model);
            Assert.Equal(2, model.Count); 
        }

        [Fact]
        public async Task GetAllBYUserIDPassword_ReturnsOkWithValues()
        {
            // Arrange
            var userID = 1;
            var passwords = new List<PasswordViewModels>
            {
                new PasswordViewModels { PasswordID = 1, UserID = userID, PasswordValue = "password1" },
                new PasswordViewModels { PasswordID = 2, UserID = userID, PasswordValue = "password2" }
            };

            _mockPasswordService.Setup(x => x.GetAllByUserId(userID)).ReturnsAsync(passwords);

            // Act
            var actionResult = await _controller.GetAllBYUserIDPassword(userID);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<PasswordViewModels>>(okObjectResult.Value);

            Assert.NotNull(model);
            Assert.Equal(2, model.Count); 
        }

        [Fact]
        public async Task GetAllBYUserIDPassword_ReturnsNotFound()
        {
            // Arrange
            var userID = 1;
            List<PasswordViewModels> passwords = null;

            _mockPasswordService.Setup(x => x.GetAllByUserId(userID)).ReturnsAsync(passwords);

            // Act
            var actionResult = await _controller.GetAllBYUserIDPassword(userID);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task AddUserToPassword_InvalidParameters_ReturnsNotFound()
        {
            // Arrange
            int passwordID = 0;
            int userID = 0;
            int roleID = 0;

            // Act
            var result = await _controller.AddUserToPassword(passwordID, userID, roleID);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        public async Task PasswordAccesGetList_ValidUserIDAndRoleID_ReturnsOkResult()
        {
            // Arrange
            int userID = 1;
            int roleID = 1;
            var expectedValues = new List<PasswordViewModels>
            {
                new PasswordViewModels { PasswordID = 1, UserID = userID, RoleID = roleID, PasswordValue = "password1" },
                new PasswordViewModels { PasswordID = 2, UserID = userID, RoleID = roleID, PasswordValue = "password2" }
            };

            _mockPasswordService.Setup(x => x.PasswordAccesGetList(userID, roleID)).ReturnsAsync(expectedValues);

            // Act
            var result = await _controller.PasswordAccesGetList(userID, roleID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<PasswordViewModels>>(okResult.Value);
            Assert.Equal(expectedValues, model);
        }

        [Fact]
        public async Task PasswordAccesGetList_InvalidUserIDAndRoleID_ReturnsNotFound()
        {
            // Arrange
            int userID = 0;
            int roleID = 0;

            _mockPasswordService.Setup(x => x.PasswordAccesGetList(userID, roleID)).ReturnsAsync((List<PasswordViewModels>)null);

            // Act
            var result = await _controller.PasswordAccesGetList(userID, roleID);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }






    }
}
