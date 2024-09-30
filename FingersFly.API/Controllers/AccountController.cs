using FingersFly.API.DTOs;
using FingersFly.API.Extensions;
using FingersFly.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FingersFly.API.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto regis)
        {
            var user = new AppUser
            {
                FirstName = regis.FirstName,
                LastName = regis.LastName,
                Email = regis.Email,
                UserName = regis.Email
            };

            var result = await signInManager.UserManager.CreateAsync(user, regis.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Register successfully!");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false)
            {
                return NoContent();
            }

            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                address = user.Address?.ToDto()
            });
        }

        [HttpGet]
        public ActionResult GetAuthState()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false
            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<Address>> CreateOrUpdateAddress(AddressDto dto)
        {
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);
            if (user.Address == null)
            {
                user.Address = dto.ToEntity();
            }
            else
            {
                user.Address.UpdateFromDto(dto);
            }

            var result = await signInManager.UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Problem with update address");
            }

            return Ok(user.Address);
        }
    }
}
