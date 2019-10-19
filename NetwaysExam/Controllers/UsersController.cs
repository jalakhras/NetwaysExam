using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Netways.Application.Dtos;
using Netways.Application.Helpers;
using Netways.Application.Service;
using Netways.ApplicationCommon.Helper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Netways.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserLoginDto userDto)
        {
            var user = _userService.Authenticate(userDto.LoginName, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "LoginName or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.LoginName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return  user info (without password) and token to store client side
            return Ok(new
            {
                user.LoginName,

                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        //POST : api/Users/Register
        public async Task<object> Register(UserDto userDto)
        {
            try
            {
                await _userService.CreateAsync(userDto);

                return Ok(new
                {
                    userDto.LoginName,
                    succeeded = true
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAll()
        {
            var userDtos = _userService.GetAll();
            return Ok(userDtos);
        }

        [AllowAnonymous]
        [HttpGet("GetCountries")]
        public IActionResult GetCountries()
        {
            var Countries = _userService.GetCountries();
            return Ok(Countries);
        }

        [AllowAnonymous]
        [HttpPost("Upload"), DisableRequestSizeLimit]
        public IActionResult Upload(string loginName)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    _userService.UpdateImagePath(loginName, dbPath);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("GetUserProfile")]
        public IActionResult GetById(string loginName)
        {
            var userDto = _userService.GetById(loginName);
            return Ok(userDto);
        }

        [Authorize]
        [HttpPut("Update")]
        public IActionResult Update(UserDto userDto)
        {
            try
            {
                _userService.Update(userDto);
                return Ok(new
                {
                    userDto.LoginName,
                    succeeded = true
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult Delete(string loginName)
        {
            try
            {
                _userService.Delete(loginName);
                return Ok(new
                {
                    succeeded = true
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}