using Microsoft.AspNetCore.Mvc;
using Moq;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.UnitTest
{
    public class UserControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private Mock<ITokenService> _mockTokenService;
        private Mock<IUserLevelService> _mockUserLevelService;

        private IUserService _userService;
        private IUserLevelService _userLevelService;
        private ITokenService _tokenService;

        public UserControllerTests()
        {
            _mockTokenService = new Mock<ITokenService>();
            _mockUserLevelService = new Mock<IUserLevelService>();
            _mockUserService = new Mock<IUserService>();

            _userService = _mockUserService.Object;
            _tokenService = _mockTokenService.Object;    
            _userLevelService = _mockUserLevelService.Object;
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
                Password = "123",
                UserID = 1,
                UserName = "Burak Güler"
            };

            _mockUserService.Setup(x => x.Login(request)).ReturnsAsync(user);
            _mockTokenService.Setup(x => x.GenerateToken(user)).Returns(tokenResponse);

            // Act
            var userResult = await _userService.Login(request);        

            var tokenResponseResult = _tokenService.GenerateToken(user);

            // Assert
            var userModel = Assert.IsType<UserViewModels>(userResult);
            var tokenModel = Assert.IsType<LoginResponse>(tokenResponseResult);


            Assert.NotNull(userModel);
            Assert.NotNull(tokenModel);
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
            var userResult = await _userService.GetAll();

            // Assert
            var userModel = Assert.IsType<List<UserViewModels>>(userResult);         

            Assert.NotNull(userModel);
            Assert.NotEmpty(userModel);
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
            var userResult = await _userService.GetAllByCompanyId(companyId);

            // Assert
            var userModel = Assert.IsType<List<UserViewModels>>(userResult);

            Assert.NotNull(userModel);
            Assert.NotEmpty(userModel);
        }


        [Fact]
        public async Task GetUser_WithValidID_ReturnsUser()
        {
            // Arrange
            int userId = 1;
            var user = new UserViewModels
            {
                UserID = 1, UserName = "user1" 

            };

            _mockUserService.Setup(x => x.GetById(userId)).ReturnsAsync(user);

            // Act      

            var userResult = await _userService.GetById(userId);
          

            // Assert
            var userModel = Assert.IsType<UserViewModels>(userResult);

            Assert.NotNull(userModel);

        }

        [Fact]
        public async Task AddUser_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;
            var user = new UserViewModels
            {
                UserID = 1,
                UserName = "user1",
                CompanyID = 1,
                CompanyName ="Name",
                LevelID = 1,

            };

            _mockUserService.Setup(x => x.Add(user,null));

            // Act      

             await _userService.Add(user);


            // Assert
            _mockUserService.Verify(x => x.Add(user,null), Times.Once);

        }



        [Fact]
        public async Task UpdateUser_WithValidModel_ReturnsOkResult()
        {
            // Arrange           

            var user = new UserViewModels()
            {
                UserID = 1,
                UserName = "BurakGuler",
                Password = "12345",
                CompanyID = 1,
                LevelID = 1,
                CreationDate = DateTime.Now,
                IsActive = true,
                LevelName = "Admin",
            };

            var userLevel = new UserLevel()
            {
                LevelID = 1,
                LevelName = "Admin"
            };

            _mockUserService.Setup(x => x.GetById(user.UserID)).ReturnsAsync(user);
            _mockUserLevelService.Setup(x=>x.GetById(user.LevelID)).ReturnsAsync(userLevel);

            _mockUserService.Setup(x => x.Update(user));

            // Act      

            var userResult = await _userService.GetById(user.UserID);          

            var levelResult = await _userLevelService.GetById(userResult.LevelID);           

            await _userService.Update(user);



            // Assert

            var userModel = Assert.IsType<UserViewModels>(userResult);
            var levelModel = Assert.IsType<UserLevel>(levelResult);

            Assert.NotNull(levelModel);
            Assert.NotNull(levelModel);
            _mockUserService.Verify(x => x.Update(user), Times.Once);

        }


        [Fact]
        public async Task RemoveUser_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;

            var user = new UserViewModels()
            {
                UserID = 1,
                UserName = "BurakGuler",
                Password = "12345",
                CompanyID = 1,
                LevelID = 1,
                CreationDate = DateTime.Now,
                IsActive = true,
                LevelName = "Admin",
            };

            var userLevel = new UserLevel()
            {
                LevelID = 1,
                LevelName = "Admin"
            };

            _mockUserService.Setup(x => x.GetById(user.UserID)).ReturnsAsync(user);
            _mockUserLevelService.Setup(x => x.GetById(user.LevelID)).ReturnsAsync(userLevel);

            _mockUserService.Setup(x => x.Remove(userId));

            // Act      

            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(userResult.LevelID);

            await _userService.Remove(userId);



            // Assert

            var userModel = Assert.IsType<UserViewModels>(userResult);
            var levelModel = Assert.IsType<UserLevel>(levelResult);

            Assert.NotNull(levelModel);
            Assert.NotNull(levelModel);
            _mockUserService.Verify(x => x.Remove(userId), Times.Once);

        }


        [Fact]
        public async Task AddUserToRole_WithValidModelId_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;
            int roleId = 1;

            var user = new UserViewModels()
            {
                UserID = 1,
                UserName = "BurakGuler",
                Password = "12345",
                CompanyID = 1,
                LevelID = 1,
                CreationDate = DateTime.Now,
                IsActive = true,
                LevelName = "Admin",
            };

            var userLevel = new UserLevel()
            {
                LevelID = 1,
                LevelName = "Admin"
            };

            _mockUserService.Setup(x => x.GetById(user.UserID)).ReturnsAsync(user);
            _mockUserLevelService.Setup(x => x.GetById(user.LevelID)).ReturnsAsync(userLevel);

            _mockUserService.Setup(x => x.AddUserToRole(userId,roleId));

            // Act      

            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(userResult.LevelID);

            await _userService.AddUserToRole(userId, roleId);



            // Assert

            var userModel = Assert.IsType<UserViewModels>(userResult);
            var levelModel = Assert.IsType<UserLevel>(levelResult);

            Assert.NotNull(levelModel);
            Assert.NotNull(levelModel);
            _mockUserService.Verify(x => x.AddUserToRole(userId, roleId), Times.Once);

        }

        [Fact]
        public async Task GetAllUserRoleByUserID_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;

            var user = new UserViewModels()
            {
                UserID = 1,
                UserName = "BurakGuler",
                Password = "12345",
                CompanyID = 1,
                LevelID = 1,
                CreationDate = DateTime.Now,
                IsActive = true,
                LevelName = "Admin",
            };

            var userLevel = new UserLevel()
            {
                LevelID = 1,
                LevelName = "Admin"
            };

            var userRoles = new List<UserRoleViewModels>
            {
                new UserRoleViewModels
                {
                    RoleID = 1,
                    UserID = 1,
                     UserRoleID = 1,
                }
            };

            _mockUserService.Setup(x => x.GetById(user.UserID)).ReturnsAsync(user);
            _mockUserLevelService.Setup(x => x.GetById(user.LevelID)).ReturnsAsync(userLevel);

            _mockUserService.Setup(x => x.GetAllUserRoleByUserID(userId)).ReturnsAsync(userRoles);

            // Act      

            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(userResult.LevelID);

            var userRoleResult = await _userService.GetAllUserRoleByUserID(userId);



            // Assert

            var userModel = Assert.IsType<UserViewModels>(userResult);
            var levelModel = Assert.IsType<UserLevel>(levelResult);
            var userRoleModel = Assert.IsType<List<UserRoleViewModels>>(userRoleResult);

            Assert.NotEmpty(userRoleModel);
            Assert.NotNull(userRoleModel);

            Assert.NotNull(levelModel);
            Assert.NotNull(levelModel);
            _mockUserService.Verify(x => x.GetAllUserRoleByUserID(userId), Times.Once);

        }


        [Fact]
        public async Task RemoveUserToRole_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int userRoleID = 1;

            var user = new UserViewModels()
            {
                UserID = 1,
                UserName = "BurakGuler",
                Password = "12345",
                CompanyID = 1,
                LevelID = 1,
                CreationDate = DateTime.Now,
                IsActive = true,
                LevelName = "Admin",
            };

            var userLevel = new UserLevel()
            {
                LevelID = 1,
                LevelName = "Admin"
            };

            var userRoles = new List<UserRoleViewModels>
            {
                new UserRoleViewModels
                {
                    RoleID = 1,
                    UserID = 1,
                     UserRoleID = 1,
                }
            };

            _mockUserService.Setup(x => x.GetById(user.UserID)).ReturnsAsync(user);
            _mockUserLevelService.Setup(x => x.GetById(user.LevelID)).ReturnsAsync(userLevel);

            _mockUserService.Setup(x => x.RemoveUserToRole(userRoleID));

            // Act      

            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(userResult.LevelID);

             await _userService.RemoveUserToRole(userRoleID);



            // Assert

            var userModel = Assert.IsType<UserViewModels>(userResult);
            var levelModel = Assert.IsType<UserLevel>(levelResult);    

            Assert.NotNull(levelModel);
            Assert.NotNull(levelModel);
            _mockUserService.Verify(x => x.RemoveUserToRole(userRoleID), Times.Once);

        }


    }
}
