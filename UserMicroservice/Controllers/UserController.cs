﻿using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserMicroservice.Models;
using UserMicroservice.Models.Data;
using UserMicroservice.Services;
using UserMicroservice.Services.Interfaces;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService? _userService;
        private readonly ISecurityService? _securityService;

        public UserController(IUserService? userService, ISecurityService? securityService)
        {
            _userService = userService;
            _securityService = securityService;
        }

        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([FromForm] LoginModel loginModel)
        {
            // get user
            var user = await _userService.GetUserAsync(loginModel.Email);

            if (user != null)
            {
                // hash password and compare with db
                var hashedPwd = _securityService.Hasher(loginModel.Password, user.PasswordSalt, Globals.HashIter);

                if (user.PasswordHash == hashedPwd) // validated user
                {
                    var result = new UserDataModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        CompanyUser = user.CompanyUser
                    };

                    return Ok(result);
                }

                return BadRequest();
            }

            return BadRequest();
        }
        
        [HttpPost("logon")]
        public async Task<IActionResult> LogOn([FromForm] LogonModel logonModel)
        {
            var isUserExist = await _userService.IsUserExistAsync(logonModel.Email);

            if (!isUserExist)
            {
                if (logonModel.Password == logonModel.PasswordConf)
                {
                    // generate salt and hash
                    var userSalt = _securityService.GenerateSalt();
                    var userHashedPwd = _securityService.Hasher(logonModel.Password, userSalt, Globals.HashIter);

                    var user = new UserDataModel()
                    {
                        Email = logonModel.Email,
                        CompanyUser = logonModel.CompanyUser,
                        PasswordSalt = userSalt,
                        PasswordHash = userHashedPwd
                    };

                    // save user
                    _userService.AddUser(user);

                    var userResult = await _userService.GetUserAsync(user.Email);

                    if (userResult != null)
                    {
                        return Ok(new UserDataModel()
                        {
                            Id = userResult.Id,
                            Email = userResult.Email,
                            CompanyUser = userResult.CompanyUser
                        });
                    }
                }
            }

            return BadRequest();
        }

        [HttpGet("get-user-by-email")]
        public async Task<IActionResult> GetUserByEmail([FromForm] string email)
        {
            var result = await _userService.GetUserAsync(email);

            if (result != null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("add-user-profile")]
        public IActionResult AddUserProfile([FromForm] UserProfileDataModel userProfile)
        {
            var result = _userService.AddUserProfile(userProfile);

            if (result != null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("get-user-profile/{userId}")]
        public async Task<IActionResult> GetUserProfileAsync(int userId)
        {
            var user = await _userService.GetUserProfileAsync(userId);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost("edit-user-profile")]
        public IActionResult EditUserProfileAsync([FromForm] UserProfileDataModel userProfile)
        {
            var result = _userService.EditUserProfile(userProfile);

            if (result != null)
            {
                return Ok();
            }
            return BadRequest();
        }

        

    }
}
