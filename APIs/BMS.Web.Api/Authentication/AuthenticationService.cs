using BMS.Web.Api.Controllers;
using Microsoft.AspNetCore.Identity;
using BMS.Sql.Library.Services;

namespace BMS.Web.Api.Authentication
{
    public class AuthenticationService
    {
        private readonly ApplicationUserService ApplicationUserService;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly JwtService JwtService;
        
        public AuthenticationService(
            ApplicationUserService applicationUserService,
            UserManager<IdentityUser> userManager,
            JwtService jwtService)
        {
            ApplicationUserService = applicationUserService;
            UserManager = userManager;
            JwtService = jwtService;
        }

        public async Task<AuthenticationResponse> AuthenticateBasic(AuthenticationBasicRequest body)
        {
            BMS.Sql.Library.Models.ApplicationUser? applicationUser = ApplicationUserService.Get(body.Email, body.ExternalId);
            if (applicationUser != null)
            {
                IdentityUser identityUser = await UserManager.FindByIdAsync(applicationUser.IdentityUserId);
                applicationUser.NickName = applicationUser.NickName;
                applicationUser.Role = ApplicationUserService.ConvertRole(body.Role);
                ApplicationUserService.Update(applicationUser);
                return GenerateAuthenticationResponse(applicationUser);
            } else
            {
                //create a new one
                Sql.Library.Models.ApplicationUser? user = 
                    await ApplicationUserService.Create("", "", body.Email, body.Email + "aaaaaa", body.ExternalId, body.Role, body.Nickname);
                if (user != null)
                    return GenerateAuthenticationResponse(user);
            }

            return new AuthenticationResponse() { Success = true };
        }

        private AuthenticationResponse GenerateAuthenticationResponse(Sql.Library.Models.ApplicationUser applicationUser)
        {
            string token = JwtService.GenerateToken(applicationUser);

            return new AuthenticationResponse()
            {
                Success = true,
                Token = token
            };
        }

    }
}
