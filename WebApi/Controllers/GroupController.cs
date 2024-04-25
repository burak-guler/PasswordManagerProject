using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Models;

namespace WebApi.Controllers
{

    public class GroupController : BaseController
    {
        private IGroupService _groupService;
        private readonly ILog _logger;
        private IUserService _userService;
        private IUserLevelService _userLevelService;
        public GroupController(IHttpContextAccessor contextAccessor, IMemoryCache memoryCache, IGroupService groupService, ILog log, IUserService userService, IUserLevelService userLevelService) : base(contextAccessor, memoryCache)
        {
            _logger = log;
            _groupService = groupService;
            _userService = userService;
            _userLevelService = userLevelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroup()
        {
            try
            {

                var values = await _groupService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBYCompanyIDGroup(int companyId)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    return NotFound();
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    return NotFound();
                }


                var values = await _groupService.GetAllByCompanyId(companyId);
                return Ok(values);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetAllBYCompanyIDGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                }


                var group = await _groupService.GetById(id);
                if (group == null)
                {
                    return NotFound();
                }
                return Ok(group);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(GroupViewModels group)
        {
            try
            {
                if (CurrentUser == null) { throw new Exception("Kullanıcı bulunamadı."); }

                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                }

                if (group == null)
                {
                    throw new Exception("model tipi boş geçilemez.");
                }

                await _groupService.Add(group, CurrentUser.UserID);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup(GroupViewModels group)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                }

                await _groupService.Update(group);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-UpdateGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveGroup(int id)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                }

                await _groupService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-RemoveGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }


        [HttpGet]
        public async Task<IActionResult> UserGroup_BYuserID(int userID)
        {
            try
            {

                var group = await _groupService.UserGroup_BYUserID(userID);
                if (group == null)
                {
                    return NotFound();
                }
                return Ok(group);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }


        [HttpGet]
        public async Task<IActionResult> UserGroup_BYGroupID(int groupID)
        {
            try
            {

                var groupUser = await _groupService.UserGroup_BYGroupID(groupID);
                if (groupUser == null)
                {
                    return NotFound();
                }
                return Ok(groupUser);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        //LKP_GroupRole Add 
        [HttpPost]
        public async Task<IActionResult> AddRoleToGroup(int groupID, int roleID)
        {
            try
            {
                if (groupID > 0 && roleID > 0)
                {
                    var user = await _userService.GetById(CurrentUser.UserID);
                    if (user == null)
                    {
                        throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                    }

                    var level = await _userLevelService.GetById(user.LevelID);
                    if (level.LevelName != "Admin")
                    {
                        throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                    }

                    await _groupService.AddGroupToRole(groupID, roleID, CurrentUser.UserID);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                _logger.Error("Hata-AddRoleToGroup" + ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveGroupToRole(int groupRoleID)
        {
            try
            {
                var user = await _userService.GetById(CurrentUser.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                }

                var level = await _userLevelService.GetById(user.LevelID);
                if (level.LevelName != "Admin")
                {
                    throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                }


                await _groupService.RemoveGroupToRole(groupRoleID, CurrentUser.UserID);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-RemoveGroupToRole:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }



        [HttpGet]
        public async Task<IActionResult> GetAllGroupRoleByGroupID(int groupID)
        {
            try
            {

                var group = await _groupService.GetAllGroupRoleByGrouID(groupID);
                if (group == null)
                {
                    return NotFound();
                }
                return Ok(group);
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-GetGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }


        }

        //LKP_UserGroup Add
        [HttpPost]
        public async Task<IActionResult> AddUserToGroup(int userID, int groupID)
        {
            try
            {
                if (groupID > 0 && userID > 0)
                {
                    var user = await _userService.GetById(CurrentUser.UserID);
                    if (user == null)
                    {
                        throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
                    }

                    var level = await _userLevelService.GetById(user.LevelID);
                    if (level.LevelName != "Admin")
                    {
                        throw new UnauthorizedAccessException("Kullanıcı yetki dışı.");
                    }

                    await _groupService.AddUserToGroup(userID, groupID, CurrentUser.UserID);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                _logger.Error("Hata-AddUserToGroup" + ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUserToGroup(int userGroupID)
        {
            try
            {
                if (userGroupID==null || userGroupID >= 0)
                {
                    throw new Exception("Geçersiz veya boş userGroupID");
                }
                await _groupService.RemoveUserToGroup(userGroupID, CurrentUser.UserID);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-RemoveUserToGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }
    }
}
