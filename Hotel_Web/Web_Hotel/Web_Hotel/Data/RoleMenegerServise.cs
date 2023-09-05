using Microsoft.AspNetCore.Identity;

namespace Web_Hotel.Data
{
    public class RoleMenegerServise
    {
        readonly RoleManager<IdentityRole> _roleManager;
        public Dictionary<string, string> MyRoles { get; set; } = new Dictionary<string, string>();

        public RoleMenegerServise(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Dictionary<string, string>> GetAllRoles()
        {
            foreach (var item in _roleManager.Roles)
            {
                MyRoles.Add(item.Id, item.Name);
            }
            return MyRoles;
        }
    }
}
