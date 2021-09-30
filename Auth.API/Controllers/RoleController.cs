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
    [Route("api/auth/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponsePostAppRole), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Post([FromBody] RequestPostAppRole request)
        {

            var result = await _roleManager.CreateAsync(new() { Name = request.Name });
            if (result.Succeeded) return Ok(new ResponsePostAppRole() { Successed = true });

            return BadRequest(result.Errors);
        }
    }
}
