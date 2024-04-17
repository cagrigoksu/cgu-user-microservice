using System.Text.Json.Serialization;
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
            var user = await _userService.GetUser(loginModel.Email);

            if (user != null)
            {
                var hashedPwd = _securityService.Hasher(loginModel.Password, user.PasswordSalt, Globals.HashIter);

                if (user.PasswordHash == hashedPwd)
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
    }
}
