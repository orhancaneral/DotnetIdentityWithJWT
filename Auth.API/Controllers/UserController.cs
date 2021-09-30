using Auth.API.Models.Entities;
using Auth.API.Models.Requests;
using Auth.API.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{
    [Route("api/auth/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponsePostAppUser), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Post([FromBody] RequestPostAppUser request)
        {
            var result = await _userManager.CreateAsync(new() { UserName = request.UserName }, request.Password);
            if (result.Succeeded) return Ok(new ResponsePostAppUser() { Successed = true });

            return BadRequest(result.Errors);
        }

        [HttpPost("{id}/roles")]
        [ProducesResponseType(typeof(ResponsePostAddRole), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddRole([FromRoute] string id, [FromBody] RequestPostAddRole request)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (result.Succeeded) return Ok(new ResponsePostAppUser() { Successed = true });

            return BadRequest(result.Errors);
        }
    }
}
