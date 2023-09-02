using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
             _db = db;   
            _userManager = userManager; 
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(t => t.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(t => t.UserName.ToLower() == loginRequestDto.Username.ToLower());
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            if (user != null)
            {
                bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                var roles = await _userManager.GetRolesAsync(user);
                string token = _jwtTokenGenerator.GenerateToken(user, roles);
                if(isValid) {
                    UserDto userDto = new();
                    userDto.Id = user.Id;
                    userDto.Name = user.Name;
                    userDto.PhoneNumber = user.PhoneNumber;
                    userDto.Email = user.Email;

                    loginResponseDto.User = userDto;
                    loginResponseDto.Token = token;
                }
            }
            else
            {
                loginResponseDto.User = null;
                loginResponseDto.Token = "";
            }
            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.Name = registrationRequestDto.Name;
            applicationUser.Email = registrationRequestDto.Email;
            applicationUser.NormalizedEmail = registrationRequestDto.Email;
            applicationUser.PhoneNumber = registrationRequestDto.PhoneNumber;
            applicationUser.UserName = registrationRequestDto.Email;
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, registrationRequestDto.Password);
                if(result.Succeeded)
                {
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex) { }
            return "Error Encountered";

        }
    }
}
