using Moq;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;

namespace PasswordManager.UnitTest
{
    public class GroupControllerTests
    {
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly Mock<IUserLevelService> _mockUserLevelService;
        private readonly Mock<IUserService> _mockUserService;

        private IGroupService _groupService;
        private IUserService _userService;
        private IUserLevelService _userLevelService;

        public GroupControllerTests()
        {
            _mockUserLevelService = new Mock<IUserLevelService>();
            _mockUserService = new Mock<IUserService>();
            _mockGroupService = new Mock<IGroupService>();

            _groupService = _mockGroupService.Object;
            _userService = _mockUserService.Object;
            _userLevelService = _mockUserLevelService.Object;
        }

        [Fact]
        public async Task GetAllGroup_ReturnsNonEmptyGroupList()
        {
            // Arrange
            var groups = new List<GroupViewModels>
            {
                new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1  },
                new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1  }
            };

            _mockGroupService.Setup(x => x.GetAll()).ReturnsAsync(groups);

            // Act
            var values = await _groupService.GetAll();

            // Assert

            var model = Assert.IsAssignableFrom<List<GroupViewModels>>(values);

            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }




        [Fact]
        public async Task GetAllBYCompanyIDGroup_WithValidID_ReturnsNonEmptyGroupList()
        {
            // Arrange
            var companyId = 1;
            var groups = new List<GroupViewModels>
            {
                new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1, CompanyName="next"  },
                new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1, CompanyName="next"  }
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

            _mockGroupService.Setup(x => x.GetAllByCompanyId(companyId)).ReturnsAsync(groups);

            // Act   

            var userResult = await _userService.GetById(user.UserID);           

            var levelResult = await _userLevelService.GetById(user.LevelID);       

            var values = await _groupService.GetAllByCompanyId(companyId);
         

            // Assert          
            var groupModel = Assert.IsAssignableFrom<List<GroupViewModels>>(values);
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);


            Assert.NotNull(userResult);          

            Assert.NotNull(levelResult);           

            Assert.NotNull(values);
            Assert.NotEmpty(groupModel);
        }


        [Fact]
        public async Task GetGroup_WithValidID_ReturnsGroup()
        {
            // Arrange
            var groupID = 1;          
            var group = new GroupViewModels { GroupID = 1, GroupName = "Next4biz", CreationDate = DateTime.Now, GroupDescription = "Stajyer Grubu", CompanyID = 1, LangID = 1 };

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

            _mockGroupService.Setup(x => x.GetById(groupID)).ReturnsAsync(group);
            _mockUserService.Setup(x => x.GetById(user.UserID)).ReturnsAsync(user);

            // Act
            var userResult = await _userService.GetById(user.UserID);           


            var groupResult = await _groupService.GetById(groupID);


            // Assert
            var groupModel = Assert.IsAssignableFrom<GroupViewModels>(groupResult);
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);


            Assert.NotNull(userResult);
            Assert.NotNull(groupResult);
        }

        [Fact]
        public async Task AddGroup_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            var newGroup = new GroupViewModels { GroupName = "csm ekip", CreationDate = DateTime.Now, GroupDescription = "csm haberleşme grubu" };

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

            _mockGroupService.Setup(x => x.Add(newGroup, null));


            // Act
            var userResult = await _userService.GetById(user.UserID);            

            var levelResult = await _userLevelService.GetById(user.LevelID);      

            await _groupService.Add(newGroup,null);


            // Assert
            
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);

            _mockGroupService.Verify((x => x.Add(newGroup,null)), Times.Once);
        }

        [Fact]
        public async Task UpdateGroup_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            var groupToUpdate = new GroupViewModels { GroupID = 1, CreationDate = DateTime.Now, GroupName = "update name" };

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

            _mockGroupService.Setup(x => x.Update(groupToUpdate));

            // Act
            var userResult = await _userService.GetById(user.UserID);
           
            var levelResult = await _userLevelService.GetById(user.LevelID);
            
            await _groupService.Update(groupToUpdate);


            // Assert
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);

            _mockGroupService.Verify((x => x.Update(groupToUpdate)), Times.Once);
        }

        [Fact]
        public async Task RemoveUserToGroup_WithValidID_ReturnsOkResult()
        {
            //Arrange 
            var userGroupID = 1;

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

            _mockGroupService.Setup(x => x.Remove(userGroupID));

            //act 
            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(user.LevelID);


            await _groupService.Remove(userGroupID);


            //assert 
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);

            _mockGroupService.Verify((x => x.Remove(userGroupID)), Times.Once);
        }

        [Fact]
        public async Task UserGroup_BYuserID_WithValidUserId_ReturnsOkResult()
        {
            //arrange
            int userId = 1;

            var groups = new List<GroupViewModels>
            {
                new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1, CompanyName="next"  },
                new GroupViewModels { GroupID =1, GroupName="Next4biz", CreationDate=DateTime.Now, GroupDescription="Stajyer Grubu",CompanyID=1, LangID=1, CompanyName="next"  }
            };
            

            _mockGroupService.Setup(x => x.UserGroup_BYUserID(userId)).ReturnsAsync(groups);

            //act
            var groupsResult = await _groupService.UserGroup_BYUserID(userId);

            //assert

            var model = Assert.IsAssignableFrom<List<GroupViewModels>>(groupsResult);

            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task UserGroup_BYGroupID_WithValidGroupId_ReturnsOkResult()
        {
            //arrange
            int groupId = 1;

            var users = new List<UserViewModels>
            {
                 new UserViewModels
                {
                    UserID = 1,
                    UserName = "BurakGuler",
                    Password = "12345",
                    CompanyID = 1,
                    LevelID = 1,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    LevelName = "Admin",
                }
            };


            _mockGroupService.Setup(x => x.UserGroup_BYGroupID(groupId)).ReturnsAsync(users);

            //act
            var usersResult = await _groupService.UserGroup_BYGroupID(groupId);

            //assert

            var model = Assert.IsAssignableFrom<List<UserViewModels>>(usersResult);

            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }


        [Fact]
        public async Task AddRoleToGroup_WithValidModelID_ReturnsOkResult()
        {
            //Arrange 
            var groupId = 1;
            var roleId = 1;

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

            _mockGroupService.Setup(x => x.AddGroupToRole(groupId,roleId,user.UserID));

            //act 
            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(user.LevelID);


            await _groupService.AddGroupToRole(groupId, roleId, user.UserID);


            //assert 
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);

            _mockGroupService.Verify((x => x.AddGroupToRole(groupId, roleId, user.UserID)), Times.Once);
        }

        [Fact]
        public async Task RemoveGroupToRole_WithValidGroupRoleID_ReturnsOkResult()
        {
            //Arrange 
            var groupRoleId = 1;

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

            _mockGroupService.Setup(x => x.RemoveGroupToRole(groupRoleId, user.UserID));

            //act 
            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(user.LevelID);


            await _groupService.RemoveGroupToRole(groupRoleId, user.UserID);


            //assert 
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);

            _mockGroupService.Verify((x => x.RemoveGroupToRole(groupRoleId, user.UserID)), Times.Once);
        }

        [Fact]
        public async Task GetAllGroupRoleByGroupID_WithValidGroupID_ReturnsOkResult()
        {
            //Arrange 
            var groupId = 1;

            var groupRoleviewModel = new List<GroupRoleViewModel>
            {
                new GroupRoleViewModel
                {
                    GroupID = groupId,
                    GroupRoleID = 1,
                    RoleID = 1,
                }
            };

      

            _mockGroupService.Setup(x => x.GetAllGroupRoleByGrouID(groupId)).ReturnsAsync(groupRoleviewModel);

            //act       


             var groupRoleResult = await _groupService.GetAllGroupRoleByGrouID(groupId);


            //assert 
            var groupRoleResultModel = Assert.IsAssignableFrom<List<GroupRoleViewModel>>(groupRoleResult);


            Assert.NotNull(groupRoleResultModel);
            Assert.NotNull(groupRoleResultModel);
        }


        [Fact]
        public async Task AddUserToGroup_WithValidModelID_ReturnsOkResult()
        {
            //Arrange 
            var groupId = 1;
            var userId = 1;

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

            _mockGroupService.Setup(x => x.AddUserToGroup(userId,groupId, user.UserID));

            //act 
            var userResult = await _userService.GetById(user.UserID);

            var levelResult = await _userLevelService.GetById(user.LevelID);


            await _groupService.AddUserToGroup(userId, groupId, user.UserID);


            //assert 
            var userModel = Assert.IsAssignableFrom<UserViewModels>(userResult);
            var levelModel = Assert.IsAssignableFrom<UserLevel>(levelResult);


            Assert.NotNull(userResult);

            Assert.NotNull(levelResult);

            _mockGroupService.Verify((x => x.AddUserToGroup(userId, groupId, user.UserID)), Times.Once);
        }


        [Fact]
        public async Task RemoveUserToGroup_WithValidUserGroupId_ReturnsOkResult()
        {
            //arrange
            int userGroupId = 1;
            int currentUserId = 1;


            _mockGroupService.Setup(x => x.RemoveUserToGroup(userGroupId, currentUserId));

            //act
            await _groupService.RemoveUserToGroup(userGroupId, currentUserId);

            //assert

            _mockGroupService.Verify(x => x.RemoveUserToGroup(userGroupId, currentUserId));
        }

    }
}
