using BMS.Web.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace BMS.Web.Api.Authentication
{
    public class JwtTokenWrapper
    {
		public int ApplicationUserId { get; set; }

		public JwtTokenWrapper(HttpRequest httpRequest)
		{
			KeyValuePair<string, StringValues> authorizationHeader =
				httpRequest.Headers.SingleOrDefault(x => x.Key == "Authorization");
			if (authorizationHeader.Equals(new KeyValuePair<string, StringValues>()))
			{
				throw new ArgumentException("Header 'Authorization' not found");
			}

			string token = authorizationHeader.Value.ToString().Replace("Bearer ", "");
			JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

			Claim claimApplicationUserId = jwtToken.Claims.SingleOrDefault(x => x.Type == "userId");
			if (claimApplicationUserId == null)
			{
				throw new ArgumentException("Claim 'applicationUserId' not found");
			}
			if (!int.TryParse(claimApplicationUserId.Value, out int applicationUserId))
			{
				throw new ArgumentException(
					$"Value '{claimApplicationUserId.Value}' for claim 'applicationUserId' is not a valid integer");
			}
			ApplicationUserId = applicationUserId;
		}
	}
}
