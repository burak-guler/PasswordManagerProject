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
    public class PasswordControllerTests
    {
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IUserLevelService> _mockUserLevelService;

        private readonly IPasswordService _passwordService;
        private readonly IUserLevelService _userLevelService;
        private readonly IUserService _userService;

        public PasswordControllerTests()
        {
            _mockPasswordService = new Mock<IPasswordService>();
            _mockUserLevelService = new Mock<IUserLevelService>();
            _mockUserService = new Mock<IUserService>();

            _passwordService = _mockPasswordService.Object;
            _userLevelService = _mockUserLevelService.Object;
            _userService = _mockUserService.Object;
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
            var passwordResult = await _passwordService.GetAll();

            // Assert
            var passwordModel = Assert.IsType<List<PasswordViewModels>>(passwordResult);
          

            Assert.NotNull(passwordModel);
            Assert.Equal(2, passwordModel.Count);
        }

        [Fact]
        public async Task GetAllBYCompanyIDPassword_WithValidID_ReturnsOkResult()
        {
            // Arrange
            var companyId = 1;
            var passwords = new List<PasswordViewModels>
            {
                new PasswordViewModels { CategoryID =1 , CompanyID=1, PasswordID=1, UserID=1, CompanyName="nex4biz"  },
                new PasswordViewModels { CategoryID =1 , CompanyID=1, PasswordID=1, UserID=1, CompanyName="nex4biz"  },
            };

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

            _mockPasswordService.Setup(x => x.GetAllByCompanyId(companyId)).ReturnsAsync(passwords);

            // Act   

            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(user.LevelID);

            var passwordResult = await _passwordService.GetAllByCompanyId(companyId);


            // Assert          
            var passwordModel = Assert.IsAssignableFrom<List<PasswordViewModels>>(passwordResult);
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);

            Assert.NotNull(passwordModel);
            Assert.NotEmpty(passwordModel);
        }


        [Fact]
        public async Task GetAllBYUserIDPassword_WithValidID_ReturnsOkResult()
        {
            // Arrange
            var userId = 1;
            var passwords = new List<PasswordViewModels>
            {
                new PasswordViewModels { CategoryID =1 , CompanyID=1, PasswordID=1, UserID=1, CompanyName="nex4biz"  },
                new PasswordViewModels { CategoryID =1 , CompanyID=1, PasswordID=1, UserID=1, CompanyName="nex4biz"  },          
            };
                       

            _mockPasswordService.Setup(x => x.GetAllByUserId(userId)).ReturnsAsync(passwords);

            // Act           

            var passwordResult = await _passwordService.GetAllByUserId(userId);


            // Assert          
            var passwordModel = Assert.IsAssignableFrom<List<PasswordViewModels>>(passwordResult);       

            Assert.NotNull(passwordModel);
            Assert.NotEmpty(passwordModel);
        }


        [Fact]
        public async Task GetPassword_WithValidID_ReturnsOkResult()
        {
            // Arrange
            var passwordId = 1;
            var password = new PasswordViewModels
            {
                CategoryID =1 , CompanyID=1, PasswordID=1, UserID=1, CompanyName="nex4biz"  
               
            };


            _mockPasswordService.Setup(x => x.GetById(passwordId)).ReturnsAsync(password);

            // Act           

            var passwordResult = await _passwordService.GetById(passwordId);


            // Assert          
            var passwordModel = Assert.IsAssignableFrom<PasswordViewModels>(passwordResult);

            Assert.NotNull(passwordModel);
        }

        [Fact]
        public async Task AddPassword_WithValidModel_ReturnsOkResult()
        {
            // Arrange

            var password = new PasswordViewModels
            {
                CategoryID = 1,
                CompanyID = 1,
                PasswordID = 1,
                UserID = 1,
                CompanyName = "nex4biz"

            };

            _mockPasswordService.Setup(x => x.Add(password,null));
            // Act           

            await _passwordService.Add(password, null);
            // Assert          
           _mockPasswordService.Verify(x=>x.Add(password,null), Times.Once());   
        }

        [Fact]
        public async Task UpdatePassword_WithValidModel_ReturnsOkResult()
        {
            // Arrange

            int userId = 1;

            var password = new PasswordViewModels
            {
                CategoryID = 1,
                CompanyID = 1,
                PasswordID = 1,
                UserID = 1,
                CompanyName = "nex4biz"

            };

            _mockPasswordService.Setup(x => x.Update(password, userId));
            // Act           

            await _passwordService.Update(password, userId);
            // Assert          
            _mockPasswordService.Verify(x => x.Update(password, userId), Times.Once());
        }


        [Fact]
        public async Task RemovePassword_WithValidId_ReturnsOkResult()
        {
            // Arrange

            int userId = 1;
            int passwordId = 1;

            var password = new PasswordViewModels
            {
                CategoryID = 1,
                CompanyID = 1,
                PasswordID = 1,
                UserID = 1,
                CompanyName = "nex4biz"

            };

            _mockPasswordService.Setup(x => x.Remove(passwordId, userId));
            // Act           

            await _passwordService.Remove(passwordId, userId);
            // Assert          
            _mockPasswordService.Verify(x => x.Remove(passwordId, userId), Times.Once());
        }


        [Fact]
        public async Task AddUserToPassword_WithValidID_ReturnsOkResult()
        {
            // Arrange
            var roleId = 1;
            var userId = 1;
            var PasswordId = 1;          

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

            _mockPasswordService.Setup(x => x.AddUserToPasswordAcces(PasswordId, userId, roleId));

            // Act   

            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(user.LevelID);

            await _passwordService.AddUserToPasswordAcces(PasswordId, userId, roleId);


            // Assert          
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);
            _mockPasswordService.Verify(x => x.AddUserToPasswordAcces(PasswordId, userId, roleId));
        }


        [Fact]
        public async Task PasswordAccesGetList_WithValidId_ReturnsOkResult()
        {
            // Arrange

            var roleId = 1;
            var userId = 1;

            var passwords = new List<PasswordViewModels>
            {
                new PasswordViewModels { CategoryID =1 , CompanyID=1, PasswordID=1, UserID=1, CompanyName="nex4biz"  },
                new PasswordViewModels { CategoryID =1 , CompanyID=1, PasswordID=1, UserID=1, CompanyName="nex4biz"  },
            };

            _mockPasswordService.Setup(x => x.PasswordAccesGetList(userId, roleId)).ReturnsAsync(passwords);
            // Act           

            var passwordResult = await _passwordService.PasswordAccesGetList(userId, roleId);
            // Assert          
            
            var passwordModel = Assert.IsType<List<PasswordViewModels>>(passwordResult);

            Assert.NotEmpty(passwordModel);
            Assert.NotNull(passwordResult);
        }

        [Fact]
        public async Task RemoveUserToPassword_WithValidID_ReturnsOkResult()
        {
            // Arrange
            var roleId = 1;
            var userId = 1;
            var PasswordId = 1;

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

            _mockPasswordService.Setup(x => x.RemoveUserToPasswordAcces(PasswordId, userId, roleId));

            // Act   

            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(user.LevelID);

            await _passwordService.RemoveUserToPasswordAcces(PasswordId, userId, roleId);


            // Assert          
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);
            _mockPasswordService.Verify(x => x.RemoveUserToPasswordAcces(PasswordId, userId, roleId));
        }
    }
}
