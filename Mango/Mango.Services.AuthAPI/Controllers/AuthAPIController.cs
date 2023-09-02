using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage)) {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                //return BadRequest(errorMessage);
            }
            return Ok(_responseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username and password are incorrect";
            }
            _responseDto.Result = loginResponse;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(RegistrationRequestDto registrationRequestDto)
        {
            bool isAssignRole = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());
            if (!isAssignRole)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Encountered";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

    }
}
