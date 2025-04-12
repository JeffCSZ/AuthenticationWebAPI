using AuthenticationWebAPI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthenticationWebAPI.Data;
using System.Diagnostics.Eventing.Reader;
using AuthenticationWebAPI.Migrations;

namespace AuthenticationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(MyDbContext _dbContext) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                // For example, return errors as JSON for an API
                return BadRequest(new { Errors = errors });
            }

            //// Simple hardcoded credentials check
            //if (model.UserName == "TC" && model.Password == "password123")
            var login = _dbContext.logins.FirstOrDefault(l => l.UserName == model.UserName && l.Password == model.Password);
            if (login != null)
            {
                var username = _dbContext.logins.FirstOrDefault(l => l.UserName == model.UserName);

                var roles = _dbContext.roles.Where(a => a.LoginId == username.Id);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, model.UserName));
                    
                    //new Claim(ClaimTypes.Role, "User"),
                    //new Claim(ClaimTypes.Role, "Admin") // Adding Admin role for demo
                    foreach (var r in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, r.Role));
                    }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    });

                return Ok(new { Message = "Login successful" });
            }
            else
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Ok(new { Message = "Logout successful" });
        }
    }
}
