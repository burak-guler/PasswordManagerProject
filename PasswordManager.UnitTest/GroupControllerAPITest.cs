using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Newtonsoft.Json.Linq;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.UnitTest.Concrete.Token;
using System.Security.Cryptography.X509Certificates;
using WebApi.Controllers;

namespace PasswordManager.UnitTest
{
    public class GroupControllerAPITest
    {
        private GroupController _controller;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly Mock<ILog> _mockLogger;
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly Mock<IUserLevelService> _mockUserLevelService;
        private readonly Mock<IUserService> _mockUserService;

      

        public GroupControllerAPITest()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockLogger = new Mock<ILog>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockUserLevelService = new Mock<IUserLevelService>();
            _mockUserService = new Mock<IUserService>();
            _mockGroupService = new Mock<IGroupService>();

         

            _controller = new GroupController(
                _mockHttpContextAccessor.Object,
                _mockMemoryCache.Object,
                _mockGroupService.Object,
                _mockLogger.Object,
                _mockUserService.Object,
                _mockUserLevelService.Object);
        }

        //[Fact]
        //public async Task GetAllGroup_ReturnsNonEmptyGroupList()
        //{
        //    // Arrange
        //    var groups = new List<GroupViewModels>
        //    {
        //        new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1  },
        //        new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1  }
        //    };

        //    _mockGroupService.Setup(x => x.GetAll()).ReturnsAsync(groups);

        //    // Act
        //    var values = await _controller.GetAllGroup();

        //    // Assert

        //    var model = Assert.IsAssignableFrom<List<GroupViewModels>>(values);
            
        //    Assert.NotNull(model);
        //    Assert.NotEmpty(model);
        //}

        //[Fact]
        //public async Task GetAllBYCompanyIDGroup_WithValidID_ReturnsNonEmptyGroupList()
        //{
        //    // Arrange
        //    var companyId = 1;
        //    var groups = new List<GroupViewModels>
        //    {
        //        new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1, CompanyName="next"  },
        //        new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1, CompanyName="next"  }
        //    };

        //    var user = new UserViewModels()
        //    {
        //        UserID = 1,
        //        UserName = "BurakGuler",
        //        Password = "12345",
        //        CompanyID=1,
        //        LevelID=1,
        //        CreationDate=DateTime.Now,
        //        IsActive=true,
        //        LevelName = "Admin",
        //    };

        //    var userLevel = new UserLevel()
        //    {
        //        LevelID = 1,
        //        LevelName = "Admin"
        //    };

        //    var userTokenResponse = new UserTokenResponse()
        //    {
        //        UserID = 1,
        //        UserName = "BurakGuler",
        //        Password = "12345"
        //    };

        //    #region
        //    //_mockHttpContextAccessor.Setup(x => x.HttpContext.Request.Headers["Authorization"]).Returns($"Bearer {TokenInfo.ADMINTOKEN}");
        //    //_mockMemoryCache.Setup(x => x.Set<UserTokenResponse>(TokenInfo.ADMINTOKEN,userTokenResponse));
        //    //_mockMemoryCache.Setup(m => m.TryGetValue<UserTokenResponse>(TokenInfo.ADMINTOKEN, out  userTokenResponse)).Returns(true);
        //    //_mockMemoryCache.Setup(x => x.CreateEntry(TokenInfo.ADMINTOKEN)).Returns(Mock.Of<ICacheEntry>);
        //    #endregion
           

        //    _mockUserService.Setup(x => x.GetById(user.UserID)).ReturnsAsync(user);

        //    _mockUserLevelService.Setup(x=>x.GetById(user.LevelID)).ReturnsAsync(userLevel);

        //    _mockGroupService.Setup(x => x.GetAllByCompanyId(companyId)).ReturnsAsync(groups);

        //    // Act   

        //    var actionResult = await _controller.GetAllBYCompanyIDGroup(companyId);

        //    // Assert
        //    var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        //    var model = Assert.IsAssignableFrom<List<GroupViewModels>>(okObjectResult.Value);


        //    Assert.NotNull(model);
        //    Assert.NotEmpty(model);
        //}

        //[Fact]
        //public async Task GetGroup_WithValidID_ReturnsGroup()
        //{
        //    // Arrange
        //    var groupID = 1;
        //    var group = new GroupViewModels { GroupID = 1, GroupName = "Next4biz", CreationDate = DateTime.Now, GroupDescription = "Stajyer Grubu", CompanyID = 1, LangID = 1 };

        //    _mockHttpContextAccessor.Setup(x => x.HttpContext.Request.Headers["Authorization"]).Returns($"Bearer {TokenInfo.ADMINTOKEN}");

        //    _mockGroupService.Setup(x => x.GetById(groupID)).ReturnsAsync(group);
             
        //    // Act
        //    var actionResult = await _controller.GetGroup(groupID);

        //    // Assert
        //    var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
        //    var model = Assert.IsType<GroupViewModels>(okObjectResult.Value);


        //    Assert.NotNull(model);
        //}

        [Fact]
        public async Task AddGroup_NullToken_ReturnsAddException()
        {
            // Arrange
            var newGroup = new GroupViewModels { GroupName = "csm ekip", CreationDate = DateTime.Now, GroupDescription = "csm haberleşme grubu" };

            _mockHttpContextAccessor.Setup(x => x.HttpContext.Request.Headers["Authorization"]).Returns("Bearer ");            

            // Act
            var result = await _controller.AddGroup(newGroup); 

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("hata: Kullanıcı bulunamadı.", statusCodeResult.Value);
        }

        //[Fact]
        //public async Task UpdateGroup_WithValidModel_ReturnsOkResult()
        //{
        //    // Arrange
        //    var groupToUpdate = new GroupViewModels { GroupID = 1, CreationDate = DateTime.Now, GroupName="update name" };

        //    // Act
        //    var actionResult = await _controller.UpdateGroup(groupToUpdate);

        //    // Assert
        //    var okResult = Assert.IsType<OkResult>(actionResult);
        //    _mockGroupService.Verify(x => x.Update(It.IsAny<GroupViewModels>()), Times.Once);
        //}

        [Fact]
        public async Task RemoveUserToGroup_NullUserGroupID_ReturnsException()
        {
            //Arrange 
            var userGroupID = 0;

            //act 
            var exception = await _controller.RemoveUserToGroup(userGroupID);

            //assert 
            var statusCodeResult = Assert.IsType<ObjectResult>(exception);
            Assert.Equal(500,statusCodeResult.StatusCode);
            Assert.Equal("hata: Geçersiz veya boş userGroupID", statusCodeResult.Value);
        }

        [Fact]
        public async Task GetAllGroupRoleByGroupID_NullGroup_ReturnsNotFound()
        {
            int groupID = 1;

            List<GroupRoleViewModel> group = null;

            _mockGroupService.Setup(x => x.GetAllGroupRoleByGrouID(groupID)).ReturnsAsync(group);

            var actionResult = await _controller.GetAllGroupRoleByGroupID(groupID);

            var statusResult = Assert.IsType<NotFoundResult>(actionResult);

            Assert.Equal(StatusCodes.Status404NotFound, statusResult.StatusCode);
        }

        [Fact]
        public async Task GetAllGroupRoleByGroupID_WithValidModel_ReturnsOkResult()
        {
            int groupID = 1;
            List<GroupRoleViewModel> group = new List<GroupRoleViewModel>
            {
                new GroupRoleViewModel {  GroupID = 1, GroupRoleID = 1, RoleID = 1},
                new GroupRoleViewModel {  GroupID = 1, GroupRoleID = 1, RoleID = 1}
            };

            _mockGroupService.Setup(x => x.GetAllGroupRoleByGrouID(groupID)).ReturnsAsync(group);

            var actionResult = await _controller.GetAllGroupRoleByGroupID(groupID);

            var okResult= Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<GroupRoleViewModel>>(okResult.Value);

            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task AddUserToGroup_ZeroValue_ReturnsNotFound()
        {
            int userID = 0;
            int groupID = 0;

            var actionresult = await _controller.AddUserToGroup(userID,groupID);

            var statusResult = Assert.IsType<NotFoundResult>(actionresult);
            Assert.Equal(StatusCodes.Status404NotFound, statusResult.StatusCode);
        }

        [Fact]
        public async Task AddRoleToGroup_ZeroValue_ReturnsNotFound()
        {
            int roleID = 0;
            int groupID = 0;

            var actionResult = await _controller.AddRoleToGroup(roleID,groupID);

            var statusResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(StatusCodes.Status404NotFound, statusResult.StatusCode);
        }

        [Fact]
        public async Task UserGroup_BYGroupID_NullUserGroup_ReturnsNotFound()
        {
            int groupID = 1;
            List<UserViewModels> userGroup = null;

            _mockGroupService.Setup(x => x.UserGroup_BYGroupID(groupID)).ReturnsAsync(userGroup);

            var actionResult = await _controller.UserGroup_BYGroupID(groupID);

            var statusResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(StatusCodes.Status404NotFound,statusResult.StatusCode);
        }

        [Fact]
        public async Task UserGroup_BYuserID_NullGroup_ReturnsNotFound()
        {
            int userID = 1;
            List<GroupViewModels> groups = null;

            _mockGroupService.Setup(x => x.UserGroup_BYUserID(userID)).ReturnsAsync(groups);

            var actionResult = await _controller.UserGroup_BYuserID(userID);

            var statusResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(StatusCodes.Status404NotFound, statusResult.StatusCode);

        }

        [Fact]
        public async Task UserGroup_BYuserID_WithValidModel_ReturnsOkResult()
        {
            int userID = 1;
            List<GroupViewModels> groups = new List<GroupViewModels> 
            { 
                new GroupViewModels { GroupName = "csm ekip", CreationDate = DateTime.Now, GroupDescription = "csm haberleşme grubu" } ,
                new GroupViewModels { GroupName = "csm ekip", CreationDate = DateTime.Now, GroupDescription = "csm haberleşme grubu" } ,
                new GroupViewModels { GroupName = "csm ekip", CreationDate = DateTime.Now, GroupDescription = "csm haberleşme grubu" } 
            };

            _mockGroupService.Setup(x => x.UserGroup_BYUserID(userID)).ReturnsAsync(groups);

            var actionResult = await _controller.UserGroup_BYuserID(userID);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<GroupViewModels>>(okObjectResult.Value);
            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }


    }
}
