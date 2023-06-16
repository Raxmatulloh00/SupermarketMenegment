using AntDesign;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Plugins.DataStore.SQL;
namespace WrbApp.Data
{
	public class UsersService
	{
		readonly UserManager<IdentityUser> _userManager;
		readonly RoleManager<IdentityRole> _roleManager;
		readonly WrbAppContext _context;
		public UsersService(UserManager<IdentityUser> userManager, 
				RoleManager<IdentityRole> roleManager,
				WrbAppContext context)
        {
            _userManager = userManager;
			_roleManager = roleManager;
			_context = context;
        }
        public async Task<bool> CreateNewUser(string login, string password, List<string> roles)
        {
			var user = new IdentityUser { UserName = login, Email = login };
			var result = await _userManager.CreateAsync(user, password);
			var dbUser = _userManager.Users.FirstOrDefault(f=>f.UserName == login); 
			if (roles.Count >1)
			{
				foreach (var item in roles)
				{
					var _user = _context.Users.FirstOrDefault(f => f.UserName == login);
					//_context.UserRoles.AddRange(roles.Select(s => new IdentityUserRole<string>
					//{
					//	RoleId = item,
					//	UserId = _user.Id
					//}));
					var dbRole = _roleManager.Roles.FirstOrDefault(w => w.Id==item).Name;
					await _userManager.AddToRoleAsync(dbUser, dbRole);
				}
			}
			else
			{
				var dbRole = _roleManager.Roles.FirstOrDefault (f => roles.Contains(f.Id));
				await _userManager.AddToRoleAsync(dbUser, dbRole.Name);
			}
			//_context.SaveChanges();
			if (result.Succeeded)
			{
			
				return true;
			}
			return false;
			}
    }
}
