using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] string roleName)
        {
            var response = await roleService.CreateRoleAsync(roleName);
            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRoleDTO updateRole)
        {
            var response = await roleService.UpdateRoleAsync(updateRole);
            return Ok(response);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRole()
        {
            var response = await roleService.GetAllRolesAsync();
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] string roleName)
        {
            var response = await roleService.DeleteRoleAsync(roleName);
            return Ok(response);
        }
        [HttpGet("get-all-user-role")]
        public async Task<ActionResult<IEnumerable<UserRoleDTO>>> GetAllUserRole()
        {
            var result = await roleService.GetAllUserRoleAsync();
            return Ok( );
        }
    }
}
