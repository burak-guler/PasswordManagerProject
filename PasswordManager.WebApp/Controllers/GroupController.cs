using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Core.Entity;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{

    public class GroupController : BaseController
    {
        private IGroupClientService _groupService;
        public GroupController(IHttpContextAccessor contextAccessor, IGroupClientService groupClientService) : base(contextAccessor)
        {
            _groupService = groupClientService;
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
                return StatusCode(500, "hata: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var group = await _groupService.Get(id);
                if (group == null)
                {
                    return NotFound();
                }
                return Ok(group);
            }
            catch (Exception ex)
            {
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
                    await _groupService.AddGroupToRole(groupID, roleID);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
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
                return StatusCode(500, ex.Message);
            }
        }
    }
}
