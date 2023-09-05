using AntDesign;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace AddUserWithRols.Data
{
    public class UsersService
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly AddUserContext _context;

        public UsersService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AddUserContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> CreateNewUser(string login, string password, List<string>? roles)
        {
            var user = new IdentityUser { UserName = login, Email = login };
            var result = await _userManager.CreateAsync(user, password);
            var dbUser = _userManager.Users.FirstOrDefault(f => f.UserName == login);

            if (roles?.Count > 1 && roles.Count != null)
            {
                foreach (var item in roles)
                {
                    var _user = _context.Users.FirstOrDefault(f => f.UserName == login);
                    var dbRole = _roleManager.Roles.FirstOrDefault(w => w.Id == item).Name;
                    await _userManager.AddToRoleAsync(dbUser, dbRole);
                }
            }
            else if (roles?.Count != 0)
            {
                var dbRole = _roleManager.Roles.FirstOrDefault(f => roles.Contains(f.Id));
                await _userManager.AddToRoleAsync(dbUser, dbRole?.Name);
            }
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
