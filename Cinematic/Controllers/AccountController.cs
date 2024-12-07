using Cinematic.Dtos.AccountDtos;
using Cinematic.Extensions;
using Cinematic.Interfaces;
using Cinematic.Mappers;
using Cinematic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinematic.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountRepository _accountRepo;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAccountRepository accountRepo, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountRepo = accountRepo;
            _tokenService = tokenService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AppUser appUser = registerDto.FormRegisterToAppUser();
                if(_userManager.Users.Any(u => u.UserName.Equals(appUser.UserName)))
                {
                    return BadRequest("Username is taken");
                }
                if (_userManager.Users.Any(u => u.Email.Equals(appUser.Email)))
                {
                    return BadRequest("Email is taken");
                }

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        string token = await _tokenService.CreateTokenAsync(appUser);
                        AppUserDto appUserDto = appUser.FromAppUserToAppUserDto(token);
                        return Ok(appUserDto);
                    }
                    return StatusCode(500, roleResult.Errors);
                }
                return StatusCode(500, createdUser.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("Username not found");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded)
            {
                string token = await _tokenService.CreateTokenAsync(user);
                return Ok(user.FromAppUserToAppUserDto(token));
            }
            return Unauthorized("Password not found");
        }
    }
}
