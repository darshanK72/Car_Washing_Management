using CarWashAPI.DTO;
using CarWashAPI.Interface;
using CarWashAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CarWashAPI.Repository;

namespace CarWashAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authReposiotry,IConfiguration configuration)
        {
            _authRepository = authReposiotry;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _authRepository.RegisterUserAsync(registerDto);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(new MessageResponse()
                {
                    Message = "User registered successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{UserId}")]
        public async Task<IActionResult> GetUserById(int UserId)
        {
            var user = await _authRepository.GetUserByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("admin/{AdminId}")]
        public async Task<IActionResult> GetAdminById(int AdminId)
        {
            var user = await _authRepository.GetAdminByIdAsync(AdminId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("washer/{WasherId}")]
        public async Task<IActionResult> GetWasherById(int WasherId)
        {
            var user = await _authRepository.GetWasherByIdAsync(WasherId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser(UserDto UserDto)
        {
            try
            {
                var user = ToUserEntity(UserDto);
                var updatedUser = await _authRepository.UpdateUserAsync(user);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                return Ok(updatedUser);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _authRepository.LoginAsync(loginDto.Email, loginDto.Password);
                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private string GenerateJwtToken(dynamic user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("user", userJson)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static UserDto ToUserDto(User user)
        {
            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                Address = user.Address,
                Role = user.Role
            };
        }

        public static User ToUserEntity(UserDto userDto)
        {
            if (userDto == null) return null;

            return new User
            {
                UserId = userDto.UserId,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                PhoneNumber = userDto.PhoneNumber,
                ProfilePicture = userDto.ProfilePicture,
                Address = userDto.Address,
                Role = userDto.Role,
                IsActive = true
            };
        }
    }
}