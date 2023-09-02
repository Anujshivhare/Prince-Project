using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(ApplicationUser applicationUser, IList<string> roles)
        {
            //A SecurityTokenHandler designed for creating and validating Json Web Tokens
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
            // claims is used to set the data related user like email, username
            var claimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),    
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString())
            };

            var rolesClaim = roles.Select(role => new Claim(ClaimTypes.Role, role));
            foreach(var role in rolesClaim) {
                claimsList.Add(role);
            }

            //A SecurityTokenDescriptor that contains details of contents of the token.
            //SigningCredentials is used to create a security token.
            //subject sets the output claims to be included in the issued token.
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Creates a Json Web Token(JWT).
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //It serializes a JwtSecurityToken into a JWT in Compact Serialization Format.
            return tokenHandler.WriteToken(token);
        }
    }
}
