using BMS.Sql.Library;
using BMS.Sql.Library.Models;
using Microsoft.AspNetCore.Identity;

namespace BMS.Sql.Library.Services
{
    public class ApplicationUserService : ModelServiceBase
    {
        private readonly IdentityUserService IdentityUserService;

        public ApplicationUserService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }

        public ApplicationUserService(
            BMSDbContext bmsDbContext,
            IdentityUserService identityUserService) : base(bmsDbContext)
        {
            IdentityUserService = identityUserService;
        }

        public UserRoleEnum ConvertRole(string role)
        {
            UserRoleEnum roleEnum = UserRoleEnum.Client;
            if (role == "reseller")
                roleEnum = UserRoleEnum.Manufacturer;
            else if (role == "installer")
                roleEnum = UserRoleEnum.Installer;
            else if (role == "admin")
                roleEnum = UserRoleEnum.Admin;
            return roleEnum;
        }
        public async Task<ApplicationUser?> Create(string firstName, string lastName, string email, string password, string externalId, string role, string nickName)
        {
            try
            {
                // Create Identity User
                IdentityUser identityUser = await IdentityUserService.CreateIdentityUser(email, password);
                if (identityUser == null)
                {
                    return null;
                }

                UserRoleEnum roleEnum = ConvertRole(role);
                // Create ApplicationUser
                ApplicationUser applicationUser = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Role = roleEnum,
                    IdentityUserId = identityUser.Id,
                    ExternalId = externalId,
                    Email = email,
                    NickName = nickName
                };
                applicationUser = BMSDbContext.ApplicationUsers.Add(applicationUser).Entity;
                BMSDbContext.SaveChanges();

                return applicationUser;
            } 
            catch
            {
                return null;
            }
        }

        public ApplicationUser? Get(int id)
        {
            return BMSDbContext.ApplicationUsers.SingleOrDefault(x => x.Id == id);
        }

        public ApplicationUser? Get(string? email, string externalId)
        {
            if (email != null && email.Length != 0 && externalId != null && externalId.Length != 0)
                return BMSDbContext.ApplicationUsers.SingleOrDefault(x => x.Email == email && x.ExternalId == externalId);

            return null;
        }

        public bool AddChargeController(ChargeController chargeController, ApplicationUser user)
        {
            UserRole userRole = new UserRole(chargeController, user);
            BMSDbContext.UserRoles.Add(userRole);
            BMSDbContext.SaveChanges();
            return true;
        }
        public ApplicationUser Update(ApplicationUser applicationUser)
        {
            ApplicationUser updatedApplicationUser = BMSDbContext.ApplicationUsers.Update(applicationUser).Entity;
            BMSDbContext.SaveChanges();
            return updatedApplicationUser;
        }

    }
}
