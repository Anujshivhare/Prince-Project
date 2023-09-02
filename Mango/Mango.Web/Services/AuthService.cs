using Mango.Web.Models;
using Mango.Web.Services.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                ApiUrl = SD.AuthAPIBase + "/api/auth/AssignRole",
                Data = registrationRequestDto
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                ApiUrl = SD.AuthAPIBase + "/api/auth/login",
                Data = loginRequestDto
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                ApiUrl = SD.AuthAPIBase + "/api/auth/register",
                Data = registrationRequestDto
            }, withBearer: false);
        }
    }
}
