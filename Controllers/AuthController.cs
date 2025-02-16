//using FluentValidation.Results;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
//using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;
//using UTS_Project_Efengsi_Rahmanto_Zalukhu.Services;
//using UTS_Project_Efengsi_Rahmanto_Zalukhu.Validators;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {

//        private readonly UserService _userService;
//        private readonly JwtService _jwtService;
//        private ValidationResult _validationResult;

//        public AuthController(UserService userService, JwtService jwtService)
//        {
//            _userService = userService;
//            _jwtService = jwtService;
//        }

//        [HttpPost("register")]
//        public IActionResult Register([FromBody] RegisterRequestDTO request)
//        {
//            var validator = new ValidatorRegisterRequest();
//            ValidationResult validationResult = validator.Validate(request);

//            if (!validationResult.IsValid)
//            {
//                return BadRequest(new
//                {
//                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
//                });
//            }

//            if (!_userService.Register(request.Username, request.Email, request.Password, request.Role))
//                return BadRequest("Username or Email already exists.");

//            return Ok("User registered successfully.");
//        }



//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginRequestDTO request)
//        {
//            var validator = new ValidatorLoginRequest();
//            ValidationResult validationResult = validator.Validate(request);

//            if (!validationResult.IsValid)
//            {
//                return BadRequest(new
//                {
//                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
//                });
//            }

//            var user = _userService.Login(request.Username, request.Password);

//            if (user == null)
//                return Unauthorized("Invalid credentials.");

//            var claims = new[]
//            {
//        new Claim(ClaimTypes.Name, user.Username),
//        new Claim(ClaimTypes.Role, user.Role)
//    };

//            var accessToken = _jwtService.GenerateAccessToken(claims);
//            var refreshToken = _jwtService.GenerateRefreshToken();

//            // Perbarui pengguna dengan refresh token baru
//            user.RefreshToken = refreshToken;
//            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
//            _userService.UpdateUser(user); // Gunakan metode UpdateUser

//            return Ok(new
//            {
//                AccessToken = accessToken,
//                RefreshToken = refreshToken
//            });
//        }
//    }
//}




















// AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Services;
using FluentValidation;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        private readonly IValidator<LoginRequestDTO> _loginValidator;
        private readonly IValidator<RegisterRequestDTO> _registerValidator;

        public AuthController(
            UserService userService,
            JwtService jwtService,
            IValidator<LoginRequestDTO> loginValidator,
            IValidator<RegisterRequestDTO> registerValidator)
        {
            _userService = userService;
            _jwtService = jwtService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            var validationResult = _loginValidator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = _userService.Login(request.Username, request.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var accessToken = _jwtService.GenerateAccessToken(claims);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Update user's refresh token in database
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _userService.UpdateUser(user);

            return Ok(new
            {
                accessToken,
                refreshToken,
                user = new
                {
                    user.Username,
                    user.Email,
                    user.Role
                }
            });
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Refresh token is required");

            var user = _userService.GetUserByRefreshToken(request.RefreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var newAccessToken = _jwtService.GenerateAccessToken(claims);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _userService.UpdateUser(user);

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO request)
        {
            // Validasi input menggunakan FluentValidation
            var validationResult = _registerValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { message = validationResult.Errors.First().ErrorMessage });
            }

            // Proses registrasi
            var (success, message) = _userService.Register(
                request.Username,
                request.Email,
                request.Password,
                request.Role
            );

            if (!success)
            {
                return BadRequest(new { message });
            }

            return Ok(new { message });
        }


        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var username = User.Identity.Name;
            var user = _userService.GetUserByUsername(username);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = DateTime.MinValue;
                _userService.UpdateUser(user);
            }

            return Ok(new { message = "Logged out successfully" });
        }
    }
}
