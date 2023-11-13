using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task.Web.Entites;
using Task.Web.Models;

namespace CompanyEmployees.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AccountsController(UserManager<ApplicationUser> userManager)//, IMapper mapper, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationVM model)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest();

            var ApplicationUser = new ApplicationUser() { PhoneNumber = model.Mobile, UserName = model.Name };
            var result = await _userManager.CreateAsync(ApplicationUser, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            var ApplicationUser = await _userManager.FindByNameAsync(model.Name);
            if (ApplicationUser == null || !await _userManager.CheckPasswordAsync(ApplicationUser, model.Password))
                return Unauthorized(new { ErrorMessage = "Invalid Authentication" });

            return Ok(new { IsAuthSuccessful = true, Token = GenerateJwtToken(ApplicationUser.Id,ApplicationUser.UserName,"") });// token });
        }

        public string GenerateJwtToken(string userId, string userName, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName)
        }),
                Expires = DateTime.UtcNow.AddDays(7), // Set token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
