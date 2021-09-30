using Auth.API.Core;
using Auth.API.Models.Entities;
using Auth.API.Models.Requests;
using Auth.API.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signinManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtObject _jwt;
        public AuthController(SignInManager<AppUser> signinManager, UserManager<AppUser> userManager, IJwtObject jwt)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _jwt = jwt;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] RequestPostAccessToken request)
        {
            var result = await _signinManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                var userRoles = await _userManager.GetRolesAsync(user);
                var response = new ResponsePostAccessToken() { AccessToken = _jwt.Create(request.UserName, userRoles.ToList()) };
                return Ok(response);
            }
            return Unauthorized();
        }
    }
}
