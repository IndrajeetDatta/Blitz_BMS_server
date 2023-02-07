using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BMS.Sql.Library.Services
{
	public class IdentityUserService
	{
		private readonly UserManager<IdentityUser> UserManager;

		public IdentityUserService(UserManager<IdentityUser> userManager)
		{
			UserManager = userManager;
		}

		public async Task<IdentityUser> CreateIdentityUser(string email, string password = "")
		{
			IdentityUser identityUser = await UserManager.FindByNameAsync(email);
			if (identityUser != null)
			{
				return null;
			}

			IdentityResult createResult;
			if (true || string.IsNullOrEmpty(password))
			{
				createResult = await UserManager.CreateAsync(new IdentityUser(email));
			}
			else
			{
				createResult = await UserManager.CreateAsync(new IdentityUser(email), password);
			}
			if (!createResult.Succeeded)
			{
				return null;
			}

			return await UserManager.FindByNameAsync(email);
		}
	}
}
