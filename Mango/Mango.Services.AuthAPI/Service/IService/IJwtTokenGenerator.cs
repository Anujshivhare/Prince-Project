using Mango.Services.AuthAPI.Models;
using System.Linq;

namespace Mango.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(ApplicationUser applicationUser, IList<string> roles);
    }
}
