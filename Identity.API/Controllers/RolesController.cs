using Identity.Domain.Entities.DTOs;
using Identity.Domain.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<ResponseDTO>> Create(RoleDTO role)
        {

            var result = await _roleManager.FindByNameAsync(role.RoleName);

            if (result == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role.RoleName));

                return Ok(new ResponseDTO
                {
                    Message = "Role Created",
                    IsSuccess = true,
                    StatusCode = 201
                });
            }

            return Ok(new ResponseDTO
            {
                Message = "Role can not be created",
                StatusCode = 403
            });
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<List<IdentityRole>>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<IdentityRole>> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return BadRequest(role);
            }

            return Ok(role);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return BadRequest("Role not found!");
            }

            await _roleManager.DeleteAsync(role);

            return Ok("Role deleted successfully!");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<string>> Update(string id, RoleDTO updatedRole)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return BadRequest("Role not found!");
            }

            role.Name = updatedRole.RoleName;
            role.NormalizedName = updatedRole.RoleName.ToUpper();

            await _roleManager.UpdateAsync(role);

            return Ok("Role updated successfully!");
        }
    }
}
