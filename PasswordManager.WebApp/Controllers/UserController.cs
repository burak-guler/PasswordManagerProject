﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.WebApp.Models;
using PasswordManager.WebApp.Services.Abstract;

namespace PasswordManager.WebApp.Controllers
{
    
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserResponse request)
        {
            try
            {
                var tokenResponse = await _userService.Login(request);
                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var values = await _userService.GetAll();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var value = await _userService.Get(id);
                if (value == null)
                {
                    return NotFound();
                }
                return Ok(value);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserResponse user)
        {
            try
            {
                await _userService.Add(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserResponse user)
        {
            try
            {
                await _userService.Update(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int id)
        {
            try
            {
                await _userService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hata: " + ex.Message);
            }
        }
    }
}
