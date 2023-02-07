using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BMS.Web.Api.Controllers;
using System;

namespace BMS.Web.Api.Authentication
{
    public class JwtService
    {
		public string Secret { get; set; }
		public string ExpiresIn { get; set; }

		public JwtService(IConfiguration configuration)
		{
			Secret = configuration.GetSection("JwtConfig").GetValue<string>("Secret");
			ExpiresIn = configuration.GetSection("JwtConfig").GetValue<string>("ExpiresIn");
		}

		public string GenerateToken(BMS.Sql.Library.Models.ApplicationUser applicationUser)
		{
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			byte[] key = Encoding.ASCII.GetBytes(Secret);
			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("userId", applicationUser.Id.ToString()),
					new Claim("authEmail", applicationUser.Email.ToString())
				}),
				Expires = DateTime.UtcNow.AddMinutes(double.Parse(ExpiresIn)),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
			};
			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
