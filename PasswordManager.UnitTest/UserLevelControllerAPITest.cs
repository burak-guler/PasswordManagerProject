using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace PasswordManager.UnitTest
{
    public class UserLevelControllerAPITest
    {
        private UserLevelController _controller;
        private Mock<IUserLevelService> _mockUserLevelService;
        private Mock<ILog> _mockLogger;

        public UserLevelControllerAPITest()
        {
            _mockUserLevelService = new Mock<IUserLevelService>();
            _mockLogger = new Mock<ILog>();

            _controller = new UserLevelController(
                new HttpContextAccessor(),
                new MemoryCache(new MemoryCacheOptions()),
                _mockUserLevelService.Object,
                _mockLogger.Object);
        }



        [Fact]
        public async Task GetAllUserLevel_ReturnsNotFound_WhenNoValues()
        {
            // Arrange
            List<UserLevel> emptyList = null;
            _mockUserLevelService.Setup(x => x.GetAll()).ReturnsAsync(emptyList);

            // Act
            var result = await _controller.GetAllUserLevel();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllUserLevel_ReturnsOk_WithValues()
        {
            // Arrange
            List<UserLevel> userLevels = new List<UserLevel>
            {
                new UserLevel { LevelID = 1, LevelName = "Level 1", IsActive = true, CompanyID = 1 },
                new UserLevel { LevelID = 2, LevelName = "Level 2", IsActive = true, CompanyID = 1 }
            };
            _mockUserLevelService.Setup(x => x.GetAll()).ReturnsAsync(userLevels);

            // Act
            var result = await _controller.GetAllUserLevel();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<UserLevel>>(okResult.Value);
            Assert.Equal(userLevels.Count, model.Count);
        }

        [Fact]
        public async Task GetAllBYCompanyIDUserLevel_ReturnsNotFound_WhenNoValues()
        {
            // Arrange
            int companyId = 1;
            List<UserLevel> emptyList = null;
            _mockUserLevelService.Setup(x => x.GetAllByCompanyId(companyId)).ReturnsAsync(emptyList);

            // Act
            var result = await _controller.GetAllBYCompanyIDUserLevel(companyId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllBYCompanyIDUserLevel_ReturnsOk_WithValues()
        {
            // Arrange
            int companyId = 1;
            List<UserLevel> userLevels = new List<UserLevel>
            {
                new UserLevel { LevelID = 1, LevelName = "Level 1", IsActive = true, CompanyID = companyId },
                new UserLevel { LevelID = 2, LevelName = "Level 2", IsActive = true, CompanyID = companyId }
            };
            _mockUserLevelService.Setup(x => x.GetAllByCompanyId(companyId)).ReturnsAsync(userLevels);

            // Act
            var result = await _controller.GetAllBYCompanyIDUserLevel(companyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<UserLevel>>(okResult.Value);
            Assert.Equal(userLevels.Count, model.Count);
        }


        [Fact]
        public async Task GetUserLevel_ReturnsNotFound_WhenUserLevelNotFound()
        {
            // Arrange
            int id = 1;
            UserLevel nullUserLevel = null;
            _mockUserLevelService.Setup(x => x.GetById(id)).ReturnsAsync(nullUserLevel);

            // Act
            var result = await _controller.GetUserLevel(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUserLevel_ReturnsOk_WhenUserLevelFound()
        {
            // Arrange
            int id = 1;
            UserLevel userLevel = new UserLevel { LevelID = id, LevelName = "Level 1", IsActive = true, CompanyID = 1 };
            _mockUserLevelService.Setup(x => x.GetById(id)).ReturnsAsync(userLevel);

            // Act
            var result = await _controller.GetUserLevel(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<UserLevel>(okResult.Value);
            Assert.Equal(id, model.LevelID);
        }


        [Fact]
        public async Task AddUserLevel_ReturnsOk_WhenServiceAddSucceeds()
        {
            // Arrange
            var userLevel = new UserLevel { LevelName = "Level 1", IsActive = true, CompanyID = 1 };

            // Act
            var result = await _controller.AddUserLevel(userLevel);

            // Assert
            Assert.IsType<OkResult>(result);

            // Verify that the service's Add method was called exactly once with the correct userLevel parameter
            _mockUserLevelService.Verify(x => x.Add(userLevel,null), Times.Once);
        }

        [Fact]
        public async Task AddUserLevel_ReturnsInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            var userLevel = new UserLevel { LevelName = "Level 1", IsActive = true, CompanyID = 1 };
           
            _mockUserLevelService.Setup(x => x.Add(userLevel, null));

            // Act
            var result = await _controller.AddUserLevel(userLevel);

            // Assert
            var statusCodeResult = Assert.IsType<OkResult>(result);
            
        }


    }
}
