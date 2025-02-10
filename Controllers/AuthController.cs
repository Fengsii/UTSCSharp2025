using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Services;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Validators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //// GET: api/<AuthController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<AuthController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<AuthController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<AuthController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AuthController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}



        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        private ValidationResult _validationResult;

        public AuthController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        //[HttpPost("register")]
        //public IActionResult Register([FromBody] RegisterRequestDTO request)
        //{
        //    var validator = new ValidatorRegisterRequest();
        //    ValidationResult validationResult = validator.Validate(request);

        //    if (!validationResult.IsValid)
        //    {
        //        return BadRequest(new
        //        {
        //            Errors = validationResult.Errors.Select(e => e.ErrorMessage)
        //        });
        //    }

        //    if (!_userService.Register(request.Username, request.Email, request.Password, request.Role))
        //        return BadRequest("Username or Email already exists.");

        //    return Ok("User registered successfully.");
        //}


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO request)
        {
            var validator = new ValidatorRegisterRequest();
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            if (!_userService.Register(request.Username, request.Email, request.Password, request.Role))
                return BadRequest("Username or Email already exists.");

            return Ok("User registered successfully.");
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            var validator = new ValidatorLoginRequest();
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            var user = _userService.Login(request.Username, request.Password);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var accessToken = _jwtService.GenerateAccessToken(claims);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Perbarui pengguna dengan refresh token baru
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _userService.UpdateUser(user); // Gunakan metode UpdateUser

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }




    }
}
