using Identity.Domain.Entities.DTOs;
using Identity.Domain.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost("Registration")]
        public async Task<ActionResult<string>> Registration(RegistrationDTO registrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registrationDTO.Password != registrationDTO.ConfirmPassword)
                throw new Exception("Passwords do not match!");

            var user = await _userManager.FindByEmailAsync(registrationDTO.Email);

            if (user is not null)
                throw new Exception("You are already registered");

            var newUser = new AppUser
            {
                FullName = registrationDTO.FullName,
                UserName = registrationDTO.UserName,
                Email = registrationDTO.Email,
                Age = registrationDTO.Age,
                Status = registrationDTO.Status
            };

            var result = await _userManager.CreateAsync(newUser, registrationDTO.Password);
        
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            foreach(var role in registrationDTO.Roles)
            {
                await _userManager.AddToRoleAsync(newUser, role);
            }

            var signIn =
                await _signInManager.PasswordSignInAsync(newUser, registrationDTO.Password, false, false);

            if (!signIn.Succeeded)
                throw new Exception("There is an issue with signing in");

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
            {
                return Unauthorized("User not found with this email");
            }

            var test = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!test)
            {
                return Unauthorized("Password is invalid");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

            if (!result.Succeeded)
                throw new Exception("There is an issue with signing in");

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAllUsers()
        {
            var result = await _userManager.Users.ToListAsync();

            return Ok(result);
        }
    }
}
