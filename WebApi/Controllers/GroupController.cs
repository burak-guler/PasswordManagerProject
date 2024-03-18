using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Concrete;
using PasswordManager.Core.Entity;

namespace WebApi.Controllers
{

    public class GroupController : BaseController
    {
        private IGroupService _groupService;
        private readonly ILog _logger;
        public GroupController(IHttpContextAccessor contextAccessor, IMemoryCache memoryCache,IGroupService groupService, ILog log ) : base(contextAccessor, memoryCache)
        {
            _logger = log;  
            _groupService = groupService;   
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
        public async Task<IActionResult> AddGroup(Group group)
        {
            try
            {
                await _groupService.Add(group);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error("HATA-AddGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup(Group group)
        {
            try
            {
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
                await _groupService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.Error("HATA-RemoveGroup:" + ex.ToString());
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        //LKP_GroupRole Add 
        [HttpPost]
        public async Task <IActionResult> AddRoleToGroup(int groupID, int roleID) 
        {
            try
            {
                if (groupID > 0 && roleID > 0 ) 
                {
                    await _groupService.AddGroupToRole(groupID, roleID);    
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

        //LKP_UserGroup Add
        [HttpPost]
        public async Task<IActionResult> AddUserToGroup(int userID, int groupID)
        {
            try
            {
                if (groupID > 0 && userID > 0)
                {
                    await _groupService.AddUserToGroup(userID, groupID);
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
    }
}
